using ECommerceService.API.Data;
using ECommerceService.API.Database.Interface;
using ECommerceService.API.Domain.Entities;
using ECommerceService.API.Helpers;

namespace ECommerceService.API.Database.Implementation
{
    public class CategoryRepository : BaseRepository<Category, int>, ICategoryRepository
    {
        public CategoryRepository(ECommerceDbContext context, ILogger<BaseRepository<Category, int>> logger) : base(context, logger)
        {
        }
    }
}
