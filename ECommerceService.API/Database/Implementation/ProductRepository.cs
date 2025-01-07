using ECommerceService.API.Data;
using ECommerceService.API.Database.Interface;
using ECommerceService.API.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ECommerceService.API.Database.Implementation
{
    public class ProductRepository : BaseRepository<Product, Guid>, IProductRepository
    {
        public ProductRepository(ECommerceDbContext context, ILogger<BaseRepository<Product, Guid>> logger)
            : base(context, logger)
        {
        }
    }
}
