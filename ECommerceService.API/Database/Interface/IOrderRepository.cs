using ECommerceService.API.Domain.Entities;

namespace ECommerceService.API.Database.Interface
{
    public interface IOrderRepository : IBaseRepository<Order, int>
    {
    }
}
