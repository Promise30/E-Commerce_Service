using ECommerceService.API.Domain.Entities;
using ECommerceService.API.Helpers;

namespace ECommerceService.API.Database.Interface
{
    public interface IProductRepository : IBaseRepository<Product, int>
    { 
    }
}
