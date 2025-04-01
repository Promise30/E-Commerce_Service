using ECommerceService.API.Data;
using ECommerceService.API.Database.Interface;
using ECommerceService.API.Domain.Entities;

namespace ECommerceService.API.Database.Implementation
{
    public class OrderRepository : BaseRepository<Order, int>, IOrderRepository
    {
        public OrderRepository(ECommerceDbContext context, ILogger<BaseRepository<Order, int>> logger) : base(context, logger)
        {
        }
    }
    
}
