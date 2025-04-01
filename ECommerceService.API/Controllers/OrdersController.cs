using ECommerceService.API.Application.Interfaces;
using ECommerceService.API.Data.Dtos.Order;
using ECommerceService.API.Domain.Enums;
using ECommerceService.API.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;

namespace ECommerceService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public OrderController(IOrderService orderService, IHttpContextAccessor httpContextAccessor)
        {
            _orderService = orderService;
            _httpContextAccessor = httpContextAccessor;
        }
        [Authorize]
        [HttpPost("checkout")]
        public async Task<IActionResult> CheckOut(CheckoutDto checkoutDto)
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            var result = await _orderService.CreateOrderFromCartAsync(userId, checkoutDto);
            return result.StatusCode == HttpStatusCode.OK ? Ok(result) : StatusCode((int)result.StatusCode, result);
        }
        [HttpGet("verify")]
        public async Task<IActionResult> VerifyPayment(string reference)
        {
            var result = await _orderService.VerifyAndFinalizeOrderAsync(reference);
            return result.StatusCode == HttpStatusCode.OK ? Ok(result) : StatusCode((int)result.StatusCode, result);
        }
        [Authorize]
        [HttpGet("history")]
        public async Task<IActionResult> GetOrderHistory([FromQuery]RequestParameters requestParameters, [FromQuery]Sorting? sortParameters, bool isPaginated, [FromQuery]OrderFilterParameters orderFilterParameters)
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            var result = await _orderService.GetOrderHistory(userId, requestParameters, sortParameters, isPaginated, orderFilterParameters);
            return result.StatusCode == HttpStatusCode.OK ? Ok(result) : StatusCode((int)result.StatusCode, result);
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("admin-order-histories")]
        public async Task<IActionResult> GetOrderHistoryByAdmin([FromQuery] RequestParameters requestParameters, [FromQuery] Sorting? sortParameters, bool isPaginated, [FromQuery] OrderFilterParameters orderFilterParameters)
        {
            var result = await _orderService.GetAllOrderHistoryByAdminAsync(requestParameters, sortParameters, isPaginated, orderFilterParameters);
            return result.StatusCode == HttpStatusCode.OK ? Ok(result) : StatusCode((int)result.StatusCode, result);
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("order-history-summary")]
        public async Task<IActionResult> GetAllOrderHistorySummaryByAdmin()
        {
            var result = await _orderService.GetOrderHistorySummaryByAdminAsync();
            return result.StatusCode == HttpStatusCode.OK ? Ok(result) : StatusCode((int)result.StatusCode, result);
        }
        [Authorize]
        [HttpGet("{orderId}")]
        public async Task<IActionResult> GetOrder(int orderId)
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            var result = await _orderService.GetOrderAsync(orderId, userId);
            return result.StatusCode == HttpStatusCode.OK ? Ok(result) : StatusCode((int)result.StatusCode, result);
        }
        [Authorize]
        [HttpPut("update-status/{orderId}")]
        public async Task<IActionResult> UpdateOrderStatus(int orderId, OrderStatus orderStatus)
        {
            var result = await _orderService.UpdateOrderStatusAsync(orderId, orderStatus);
            return result.StatusCode == HttpStatusCode.OK ? Ok(result) : StatusCode((int)result.StatusCode, result);
        }
        [Authorize]
        [HttpDelete("cancel/{orderId}")]
        public async Task<IActionResult> CancelOrder(int orderId)
        {
            var result = await _orderService.CancelOrderAsync(orderId);
            return result.StatusCode == HttpStatusCode.OK ? Ok(result) : StatusCode((int)result.StatusCode, result);
        }
    }
}
