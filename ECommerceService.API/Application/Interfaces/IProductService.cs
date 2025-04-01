using ECommerceService.API.Data.Dtos.Product;
using ECommerceService.API.Domain.Entities;
using ECommerceService.API.Helpers;
using System.Linq.Expressions;

namespace ECommerceService.API.Application.Interfaces
{
    public interface IProductService
    {
        Task<APIResponse<object>> GetAllProductsAsync(RequestParameters requestParameters, bool isPaginated, Sorting? sortParameter, FilterParameters? filterParameters);
        Task<APIResponse<ProductDto>> GetProductAsync(int productId);
        Task<APIResponse<ProductDto>> CreateProduct(CreateProductDto createProduct);
        Task<APIResponse<object>> UpdateProduct(int productId, UpdateProductDto updateProduct);
        Task<APIResponse<object>> DeleteProduct(int productId);

    }
}
