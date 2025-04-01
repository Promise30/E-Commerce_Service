using ECommerceService.API.Data.Dtos.Category;
using ECommerceService.API.Domain.Entities;
using ECommerceService.API.Helpers;
using System.Linq.Expressions;

namespace ECommerceService.API.Application.Interfaces
{
    public interface ICategoryService
    {
        Task<APIResponse<object>> GetAllCategoriesAsync(RequestParameters requestParameters, bool isPaginated, Sorting? sortingParameters, string searchParameter);
        Task<APIResponse<CategoryDto>> GetCategory(int categoryId);
        Task<APIResponse<CategoryDto>> CreateCategory(CreateCategoryDto createCategory);
        Task<APIResponse<object>> UpdateCategory(int categoryId, UpdateCategoryDto updateCategory);
        Task<APIResponse<object>> DeleteCategory(int categoryId);
    }
}
