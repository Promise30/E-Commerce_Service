using ECommerceService.API.Data;
using ECommerceService.API.Database.Interface;
using ECommerceService.API.Domain.Entities;

namespace ECommerceService.API.Database.Implementation
{
    public class OrderItemRepository : BaseRepository<OrderItem, int>, IOrderItemRepository
    {
        public OrderItemRepository(ECommerceDbContext context, ILogger<BaseRepository<OrderItem, int>> logger) : base(context, logger)
        {
        }
    }
}
