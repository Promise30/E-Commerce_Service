using ECommerceService.API.Data.Dtos.CartItem;
using ECommerceService.API.Helpers;

namespace ECommerceService.API.Application.Interfaces
{
    public interface ICartService
    {
        Task<APIResponse<IEnumerable<CartDto>>> GetCarts();
        Task<APIResponse<object>> ClearCart(string userId);
        Task<APIResponse<object>> ClearCarts();
        Task<APIResponse<CartDto>> GetUserCart(string userId);
        Task<APIResponse<CartDto>> CreateUserCart(string userId);
        Task<APIResponse<CartDto>> AddItemToCart(string userId, AddCartItemDto cartItemDto);
        Task<APIResponse<object>> RemoveItemFromCart(string userId, int cartItemId);
        Task<APIResponse<object>> UpdateCartItem(string userId, UpdateCartItemDto cartItemDto);
        Task<APIResponse<object>> UpdateCart(string userId, CartDto cartDto);
        Task<APIResponse<object>> DeleteCart(string userId);
        Task<APIResponse<object>> DeleteCarts();

    }
}
