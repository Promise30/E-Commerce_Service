using ECommerceService.API.Data;
using ECommerceService.API.Database.Interface;
using ECommerceService.API.Domain.Entities;
using ECommerceService.API.Helpers;
using Microsoft.EntityFrameworkCore;

namespace ECommerceService.API.Database.Implementation
{
    public class ProductRepository : BaseRepository<Product, int>, IProductRepository
    {
        public ProductRepository(ECommerceDbContext context, ILogger<BaseRepository<Product, int>> logger)
            : base(context, logger)
        {
        }
    }
}
