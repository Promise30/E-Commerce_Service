using AutoMapper;
using ECommerceService.API.Application.Interfaces;
using ECommerceService.API.Data.Dtos;
using ECommerceService.API.Data.Dtos.CartItem;
using ECommerceService.API.Database.Interface;
using ECommerceService.API.Domain.Entities;
using ECommerceService.API.Helpers;
using System.Net;
using System.Runtime.CompilerServices;

namespace ECommerceService.API.Application.Implementation
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;
        private readonly IProductRepository _productRepository;
        private readonly ICartItemRepository _cartItemRepository;
        private readonly ILogger<CartService> _logger;
        private readonly IMapper _mapper;
        public CartService(ICartRepository cartRepository, ILogger<CartService> logger, IMapper mapper,
                           ICartItemRepository cartItemRepository, IProductRepository productRepository)
        {
            _cartRepository = cartRepository;
            _logger = logger;
            _mapper = mapper;
            _cartItemRepository = cartItemRepository;
            _productRepository = productRepository;
        }

        public async Task<APIResponse<CartDto>> AddItemToCart(string userId, AddCartItemDto cartItemDto)
        {
            try
            {
                var cart = await _cartRepository.GetAsync(x=> x.UserId == userId);
                if (cart == null)
                    return APIResponse<CartDto>.Create(HttpStatusCode.BadRequest, "Cart not found", null);
                var product = await _productRepository.GetByIdAsync(cartItemDto.ProductId);
                if (product == null)
                    return APIResponse<CartDto>.Create(HttpStatusCode.BadRequest, "Product not found", null);

                // check if product already exists in cart and also if quantity to be added is not more than available quantity
                var existingCartItem = await _cartItemRepository.GetAsync(x => x.ProductId == cartItemDto.ProductId && x.CartId == cart.Id);
                if (existingCartItem == null)
                {
                    if (cartItemDto.Quantity > product.StockQuantity)
                        return APIResponse<CartDto>.Create(HttpStatusCode.BadRequest, "Quantity to be added is more than available quantity", null);
                    var newCartItem = new CartItem
                    {
                        CartId = cart.Id,
                        ProductId = cartItemDto.ProductId,
                        Quantity = cartItemDto.Quantity,
                        UnitPrice = product.Price,
                        CreatedDate = DateTime.UtcNow,
                        LastModifiedDate = DateTime.UtcNow,
                    };
                    await _cartItemRepository.CreateAsync(newCartItem);
                }
                else
                {
                    if (existingCartItem.Quantity + cartItemDto.Quantity > product.StockQuantity)
                        return APIResponse<CartDto>.Create(HttpStatusCode.BadRequest, "Quantity to be added is more than available quantity", null);
                    existingCartItem.Quantity += cartItemDto.Quantity;
                    existingCartItem.LastModifiedDate = DateTime.UtcNow;
                }

                await _cartItemRepository.SaveChangesAsync();

                var cartDto = _mapper.Map<CartDto>(cart);

                return APIResponse<CartDto>.Create(HttpStatusCode.OK, "Item added to cart successfully", cartDto, null);

            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred when trying to add item to cart: {ex.Message}");
                return APIResponse<CartDto>.Create(HttpStatusCode.InternalServerError, "An error occurred when trying to add item to cart.", null);
            }
        }

        public async Task<APIResponse<object>> ClearCart(string userId)
        {
            try
            {
                var cart = await _cartRepository.GetAsync(x => x.UserId == userId);
                if (cart == null)
                    return APIResponse<object>.Create(HttpStatusCode.BadRequest, "Cart not found", null);
                cart.CartItems.Clear();
                cart.LastModifiedDate = DateTime.UtcNow;
                await _cartRepository.SaveChangesAsync();
                return APIResponse<object>.Create(HttpStatusCode.OK, "Cart cleared successfully", null);

            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred when trying to clear cart: {ex.Message}");
                return APIResponse<object>.Create(HttpStatusCode.InternalServerError, "An error occurred when trying to clear cart.", null);
            }
        }

        public async Task<APIResponse<object>> ClearCarts()
        {
            try
            {
                var carts = await _cartRepository.GetAllAsync(null);
                foreach (var cart in carts)
                {
                    cart.CartItems.Clear();
                    cart.LastModifiedDate = DateTime.UtcNow;
                }
                await _cartRepository.SaveChangesAsync();
                return APIResponse<object>.Create(HttpStatusCode.OK, "Carts cleared successfully", null);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred when trying to clear carts: {ex.Message}");
                return APIResponse<object>.Create(HttpStatusCode.InternalServerError, "An error occurred when trying to clear carts.", null);
            }
        }

        public async Task<APIResponse<CartDto>> CreateUserCart(string userId)
        {
            try
            {
                // check if user already has a cart created
                var cart = await _cartRepository.GetAsync(x => x.UserId == userId);
                if (cart != null)
                    return APIResponse<CartDto>.Create(HttpStatusCode.BadRequest, "User already has a cart", null);
                var newCart = new Cart
                {
                    UserId = userId,
                    CreatedDate = DateTime.UtcNow,
                    LastModifiedDate = DateTime.UtcNow,
                };
                await _cartRepository.CreateAsync(newCart);
                await _cartRepository.SaveChangesAsync();

                var cartToReturn = _mapper.Map<CartDto>(cart);  


                return APIResponse<CartDto>.Create(HttpStatusCode.OK, "Cart created successfully", cartToReturn);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred when trying to create user cart: {ex.Message}");
                return APIResponse<CartDto>.Create(HttpStatusCode.InternalServerError, "An error occurred when trying to create user cart.", null);
            }
        }

        public async Task<APIResponse<object>> DeleteCart(string userId)
        {
            try
            {
                var cart = await _cartRepository.GetAsync(x => x.UserId == userId);
                if (cart == null)
                    return APIResponse<object>.Create(HttpStatusCode.BadRequest, "Cart not found", null);
                await _cartRepository.DeleteAsync(cart.Id);
                await _cartRepository.SaveChangesAsync();
                return APIResponse<object>.Create(HttpStatusCode.OK, "Cart deleted successfully", null);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred when trying to delete cart: {ex.Message}");
                return APIResponse<object>.Create(HttpStatusCode.InternalServerError, "An error occurred when trying to delete cart.", null);
            }
        }

        public async Task<APIResponse<object>> DeleteCarts()
        {
            try
            {
                var carts = await _cartRepository.GetAllAsync(null);
                foreach (var cart in carts)
                {
                    await _cartRepository.DeleteAsync(cart.Id);
                }
                await _cartRepository.SaveChangesAsync();
                return APIResponse<object>.Create(HttpStatusCode.OK, "Carts deleted successfully", null);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred when trying to delete carts: {ex.Message}");
                return APIResponse<object>.Create(HttpStatusCode.InternalServerError, "An error occurred when trying to delete carts.", null);
            }
        }

        public async Task<APIResponse<IEnumerable<CartDto>>> GetCarts()
        {
            try
            {
                var carts = await _cartRepository.GetAllAsync(null);
                var cartsDto = _mapper.Map<IEnumerable<CartDto>>(carts);
                return APIResponse<IEnumerable<CartDto>>.Create(HttpStatusCode.OK, "Carts retrieved successfully", cartsDto, null);

            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred when trying to get carts: {ex.Message}");
                return APIResponse<IEnumerable<CartDto>>.Create(HttpStatusCode.InternalServerError, "An error occurred when trying to get carts.", null);
            }
        }

        public async Task<APIResponse<CartDto>> GetUserCart(string userId)
        {
            try
            {
                var cart = await _cartRepository.GetAsync(x=> x.UserId == userId);
                if (cart == null)
                {
                    // create the cart
                    var newCart = new Cart
                    {
                        UserId = userId,
                        CreatedDate = DateTime.UtcNow,
                        LastModifiedDate = DateTime.UtcNow,
                    };
                    await _cartRepository.CreateAsync(newCart);
                    await _cartRepository.SaveChangesAsync();

                    var cartToReturn = _mapper.Map<CartDto>(cart);


                    return APIResponse<CartDto>.Create(HttpStatusCode.OK, "Cart created and retrieved successfully", cartToReturn, null);
                }
                var cartDto = _mapper.Map<CartDto>(cart);
                return APIResponse<CartDto>.Create(HttpStatusCode.OK, "Cart retrieved successfully", cartDto, null);

            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred when trying to get user cart: {ex.Message}");
                return APIResponse<CartDto>.Create(HttpStatusCode.InternalServerError, "An error occurred when trying to get user cart.", null);
            }
        }

        public async Task<APIResponse<object>> RemoveItemFromCart(string userId, int cartItemId)
        {
            try
            {
                var cart = await _cartRepository.GetAsync(x => x.UserId == userId);
                if (cart == null)
                    return APIResponse<object>.Create(HttpStatusCode.BadRequest, "Cart not found", null);
                var cartItem = await _cartItemRepository.GetAsync(x => x.Id == cartItemId);
                if (cartItem == null)
                    return APIResponse<object>.Create(HttpStatusCode.BadRequest, "Cart item not found", null);
                await _cartItemRepository.DeleteAsync(cartItemId);
                await _cartItemRepository.SaveChangesAsync();
                return APIResponse<object>.Create(HttpStatusCode.OK, "Item removed from cart successfully", null);

            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred when trying to remove item from cart: {ex.Message}");
                return APIResponse<object>.Create(HttpStatusCode.InternalServerError, "An error occurred when trying to remove item from cart.", null);
            }
        }

        public async Task<APIResponse<object>> UpdateCart(string userId, CartDto cartDto)
        {
            throw new NotImplementedException();
        }

        public async Task<APIResponse<object>> UpdateCartItem(string userId, UpdateCartItemDto cartItemDto)
        {
            try
            {
                var cart = await _cartRepository.GetAsync(x => x.UserId == userId);
                if (cart == null)
                    return APIResponse<object>.Create(HttpStatusCode.BadRequest, "Cart not found", null);
                var product = await _productRepository.GetByIdAsync(cartItemDto.ProductId);
                if (product == null)
                    return APIResponse<object>.Create(HttpStatusCode.BadRequest, "Product not found", null);

                var cartItem = await _cartItemRepository.GetAsync(x => x.Id == cartItemDto.CartItemId);
                if (cartItem == null)
                    return APIResponse<object>.Create(HttpStatusCode.BadRequest, "Cart item not found", null);

                cartItem.UnitPrice = product.Price;
                cartItem.ProductId = cartItemDto.ProductId;
                cartItem.Quantity = cartItemDto.Quantity;
                cartItem.LastModifiedDate = DateTime.UtcNow;
                await _cartItemRepository.SaveChangesAsync();
                return APIResponse<object>.Create(HttpStatusCode.OK, "Cart item updated successfully", null);

            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred when trying to update cart item: {ex.Message}");
                return APIResponse<object>.Create(HttpStatusCode.InternalServerError, "An error occurred when trying to update cart item.", null);
            }
        }
    }
}
