using ECommerceService.API.Data.Dtos.Order;
using ECommerceService.API.Domain.Enums;
using ECommerceService.API.Helpers;

namespace ECommerceService.API.Application.Interfaces
{
    public interface IOrderService
    {
        Task<APIResponse<object>> GetOrderAsync(int orderId, string userId);
        Task<APIResponse<object>> CreateOrderFromCartAsync(string userId, CheckoutDto checkoutDto);
        Task<APIResponse<object>> GetOrderHistory(string userId, RequestParameters requestParameters, Sorting? sortParameters, bool isPaginated, OrderFilterParameters orderFilterParameters);
        Task<APIResponse<object>> UpdateOrderStatusAsync(int orderId, OrderStatus orderStatus);
        Task<APIResponse<object>> CancelOrderAsync(int orderId);
        Task<APIResponse<object>> VerifyAndFinalizeOrderAsync(string reference);
        Task<APIResponse<object>> GetAllOrderHistoryByAdminAsync(RequestParameters requestParameters, Sorting? sortParameters, bool isPaginated, OrderFilterParameters orderFilterParameters);
        Task<APIResponse<OrderHistorySummary>> GetOrderHistorySummaryByAdminAsync();
    }
}
