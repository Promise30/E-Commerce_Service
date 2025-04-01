using ECommerceService.API.Data;
using ECommerceService.API.Database.Interface;
using ECommerceService.API.Domain.Entities;

namespace ECommerceService.API.Database.Implementation
{
    public class CartItemRepository : BaseRepository<CartItem, int>, ICartItemRepository
    {
        public CartItemRepository(ECommerceDbContext context, ILogger<BaseRepository<CartItem, int>> logger) : base(context, logger)
        {
        }

        
    }
}
