namespace ECommerceService.API.Data.Dtos.Order
{
    public class OrderHistorySummary
    {
        public int TotalOrders { get; set; }
        public int TotalPaidOrders { get; set; }
        public int TotalCancelledOrders { get; set; }
        public int TotalPendingOrders { get; set; }

    }
}
