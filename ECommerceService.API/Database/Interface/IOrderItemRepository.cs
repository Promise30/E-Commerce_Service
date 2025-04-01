using ECommerceService.API.Domain.Entities;

namespace ECommerceService.API.Database.Interface
{
    public interface IOrderItemRepository : IBaseRepository<OrderItem, int>
    {
    }
}
