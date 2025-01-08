using ECommerceService.API.Data.Dtos;
using ECommerceService.API.Domain.Entities;
using ECommerceService.API.Helpers;
using System.Linq.Expressions;

namespace ECommerceService.API.Application.Interfaces
{
    public interface ICategoryService
    {
        Task<APIResponse<IEnumerable<Category>>> GetAllCategoriesAsync(Expression<Func<Category, bool>> filter = null);
        Task<APIResponse<PagedList<CategoryDto>>> GetCategoriesPaginatedAsync(RequestParameters requestParameters, Expression<Func<Category, bool>> filter= null);
        Task<APIResponse<CategoryDto>> GetCategory(int categoryId);
        Task<APIResponse<CategoryDto>> CreateCategory(CreateCategoryDto createCategory);
        Task<APIResponse<object>> UpdateCategory(int categoryId, UpdateCategoryDto updateCategory);
        Task<APIResponse<object>> DeleteCategory(int categoryId);
    }
}
