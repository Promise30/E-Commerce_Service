using ECommerceService.API.Domain.Entities;

namespace ECommerceService.API.Database.Interface
{
    public interface IProductRepository : IBaseRepository<Product, Guid>
    {
    }
}
