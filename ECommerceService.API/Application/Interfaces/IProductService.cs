using ECommerceService.API.Data.Dtos;
using ECommerceService.API.Domain.Entities;
using ECommerceService.API.Helpers;
using System.Linq.Expressions;

namespace ECommerceService.API.Application.Interfaces
{
    public interface IProductService
    {
        Task<APIResponse<IEnumerable<ProductDto>>> GetAllProductsAsync(Expression<Func<Product, bool>> filter = null);
        Task<APIResponse<PagedList<ProductDto>>> GetAllProductsPaginatedAsync(RequestParameters requestParameters, Expression<Func<Product, bool>> filter = null);
        Task<APIResponse<ProductDto>> GetProductAsync(Guid productId);
        Task<APIResponse<ProductDto>> CreateProduct(CreateProductDto createProduct);
        Task<APIResponse<object>> UpdateProduct(Guid productId, UpdateProductDto updateProduct);
        Task<APIResponse<object>> DeleteProduct(Guid productId);

    }
}
