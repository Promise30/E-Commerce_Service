using ECommerceService.API.Data;
using ECommerceService.API.Database.Interface;
using ECommerceService.API.Domain.Entities;

namespace ECommerceService.API.Database.Implementation
{
    public class CartRepository : BaseRepository<Cart, int>, ICartRepository
    {
        public CartRepository(ECommerceDbContext context, ILogger<BaseRepository<Cart, int>> logger) : base(context, logger)
        {
        }
    }
}
