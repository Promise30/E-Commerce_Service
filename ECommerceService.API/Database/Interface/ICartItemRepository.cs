using ECommerceService.API.Domain.Entities;

namespace ECommerceService.API.Database.Interface
{
    public interface ICartItemRepository : IBaseRepository<CartItem, int>
    {
       // Task ClearCartAsync(int cartId);
    }
}
