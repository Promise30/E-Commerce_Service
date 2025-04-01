using ECommerceService.API.Application.Interfaces;
using ECommerceService.API.Data.Dtos.CartItem;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;

namespace ECommerceService.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CartsController : ControllerBase
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly ICartService _cartService;


        public CartsController(IHttpContextAccessor contextAccessor, ICartService cartService)
        {
            _contextAccessor = contextAccessor;
            _cartService = cartService;
        }

        [Authorize]
        [HttpGet("get-cart")]
        public async Task<IActionResult> GetUserCart()
        {
            var userId = _contextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            var result = await _cartService.GetUserCart(userId);
            return result.StatusCode == HttpStatusCode.OK ? Ok(result) : StatusCode((int)result.StatusCode, result);
        }
        [Authorize]
        [HttpPost("create-cart")]
        public async Task<IActionResult> CreateUserCart()
        {
            var userId = _contextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            var result = await _cartService.CreateUserCart(userId);
            return result.StatusCode == HttpStatusCode.OK ? Ok(result) : StatusCode((int)result.StatusCode, result);
        }
        [Authorize]
        [HttpPost("add-item")]
        public async Task<IActionResult> AddItemToCart(AddCartItemDto cartItemDto)
        {
            var userId = _contextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            var result = await _cartService.AddItemToCart(userId, cartItemDto);
            return result.StatusCode == HttpStatusCode.OK ? Ok(result) : StatusCode((int)result.StatusCode, result);
        }
        [Authorize]
        [HttpPut("update-item")]
        public async Task<IActionResult> UpdateCartItem(UpdateCartItemDto cartItemDto)
        {
            var userId = _contextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            var result = await _cartService.UpdateCartItem(userId, cartItemDto);
            return result.StatusCode == HttpStatusCode.OK ? Ok(result) : StatusCode((int)result.StatusCode, result);
        }
        [Authorize]
        [HttpDelete("remove-item")]
        public async Task<IActionResult> RemoveCartItem(int cartItemId)
        {
            var userId = _contextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            var result = await _cartService.RemoveItemFromCart(userId, cartItemId);
            return result.StatusCode == HttpStatusCode.OK? Ok(result) : StatusCode((int)result.StatusCode, result);
        }
        [Authorize]
        [HttpDelete("delete-cart")]
        public async Task<IActionResult> DeleteCart()
        {
            var userId = _contextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            var result = await _cartService.DeleteCart(userId);
            return result.StatusCode == HttpStatusCode.OK ? Ok(result) : StatusCode((int)result.StatusCode, result);
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("delete-carts")]
        public async Task<IActionResult> DeleteCarts()
        {
            var result = await _cartService.DeleteCarts();
            return result.StatusCode == HttpStatusCode.OK ? Ok(result) : StatusCode((int)result.StatusCode, result);
        }
        [Authorize]
        [HttpDelete("clear-cart")]
        public async Task<IActionResult> ClearCart()
        {
            var userId = _contextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            var result = await _cartService.ClearCart(userId);
            return result.StatusCode == HttpStatusCode.OK ? Ok(result) : StatusCode((int)result.StatusCode, result);
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("clear-carts")]
        public async Task<IActionResult> ClearCarts()
        {
            var result = await _cartService.ClearCarts();
            return result.StatusCode == HttpStatusCode.OK ? Ok(result) : StatusCode((int)result.StatusCode, result);
        }
    }
}
