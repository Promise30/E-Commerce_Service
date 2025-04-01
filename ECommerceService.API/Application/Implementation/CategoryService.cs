using AutoMapper;
using ECommerceService.API.Application.Interfaces;
using ECommerceService.API.Data.Dtos.Category;
using ECommerceService.API.Data.Dtos.Product;
using ECommerceService.API.Database.Implementation;
using ECommerceService.API.Database.Interface;
using ECommerceService.API.Domain.Entities;
using ECommerceService.API.Helpers;
using System.Linq.Expressions;
using System.Net;

namespace ECommerceService.API.Application.Implementation
{
    public class CategoryService : ICategoryService
    {
        private readonly IMapper _mapper;
        private readonly ILogger<CategoryService> _logger;
        private readonly ICategoryRepository _categoryRepository;
        public CategoryService(IMapper mapper, ILogger<CategoryService> logger, ICategoryRepository categoryRepository)
        {
            _mapper = mapper;
            _logger = logger;
            _categoryRepository = categoryRepository;
        }
        public async Task<APIResponse<CategoryDto>> CreateCategory(CreateCategoryDto createCategory)
        {
            try
            {
                var categoryExists = await _categoryRepository.GetAsync(c => c.Name == createCategory.Name);
                if (categoryExists != null)
                {
                    return APIResponse<CategoryDto>.Create(HttpStatusCode.BadRequest, "Category already exists", null);
                }

                Category categoryToCreate = _mapper.Map<Category>(createCategory);
                await _categoryRepository.CreateAsync(categoryToCreate);
                await _categoryRepository.SaveChangesAsync();
                var categoryToReturn = _mapper.Map<CategoryDto>(categoryToCreate);
                return APIResponse<CategoryDto>.Create(HttpStatusCode.Created, "Category created successfully", categoryToReturn);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred when trying to create a new category: {ex.Message}");
                return APIResponse<CategoryDto>.Create(HttpStatusCode.InternalServerError, "An error occurred when trying to create a new category", null);
            }
        }

        public async Task<APIResponse<object>> DeleteCategory(int categoryId)
        {
            try
            {
                var category = await _categoryRepository.GetByIdAsync(categoryId);
                if (category == null)
                {
                    return APIResponse<object>.Create(HttpStatusCode.NotFound, "Category not found", null);
                }
                await _categoryRepository.DeleteAsync(categoryId);
                await _categoryRepository.SaveChangesAsync();
                return APIResponse<object>.Create(HttpStatusCode.OK, "Category deleted successfully", null);

            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred when trying to delete a category: {ex.Message}");
                return APIResponse<object>.Create(HttpStatusCode.InternalServerError, "An error occurred when trying to delete category", null);

            }
        }
        public async Task<APIResponse<IEnumerable<Category>>> GetAllCategoriesAsync(Expression<Func<Category, bool>> filter = null)
        {
            try
            {
                var categories = await _categoryRepository.GetAllAsync(filter);
                return APIResponse<IEnumerable<Category>>.Create(HttpStatusCode.OK, "Categories retrieved successfully", categories);

            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred when trying to retrieve categories: {ex.Message}");
                return APIResponse<IEnumerable<Category>>.Create(HttpStatusCode.InternalServerError, "An error occurred when trying to retrieve categories", null);

            }
        }
        public async Task<APIResponse<object>> GetAllCategoriesAsync(RequestParameters requestParameters, bool isPaginated, Sorting? sortingParameters, string searchParameter)
        {
            try
            {
                var categories = await _categoryRepository.GetAllAsync(null, includeProperty: c=> c.Products);
                if (categories == null)
                {
                    return APIResponse<object>.Create(HttpStatusCode.NotFound, "Categories not found", null);
                }

                //searching
                if (!string.IsNullOrEmpty(searchParameter))
                {
                    categories = categories.Where(c => c.Name.ToLower().Contains(searchParameter.ToLower()) 
                                                 || c.Description.ToLower().Contains(searchParameter.ToLower()));
                }

                //sorting
                switch (sortingParameters.SortBy?.ToLower())
                {
                    case "name":
                        categories = sortingParameters.IsAscending == true ? categories.OrderBy(c => c.Name) : categories.OrderByDescending(c => c.Name);
                        break;
                    case "description":
                        categories = sortingParameters.IsAscending == true ? categories.OrderBy(c => c.Description) : categories.OrderByDescending(c => c.Description);
                        break;
                    default:
                        categories = sortingParameters.IsAscending == true ? categories.OrderBy(c => c.CreatedDate) : categories.OrderByDescending(c => c.CreatedDate);
                        break;
                }

                // pagination
                if (!isPaginated)
                {
                    var categoriesToReturn = _mapper.Map<IEnumerable<CategoryDto>>(categories);
                    return APIResponse<object>.Create(HttpStatusCode.OK, "Categories retrieved successfully", categoriesToReturn);
                }
                else
                {
                    var pagedCategories = await _categoryRepository.GetAllPaginatedAsync(requestParameters, categories.ToList());
                    var result = pagedCategories.Data.Select(category => _mapper.Map<CategoryDto>(category));
                    var responseToReturn = new PagedResponse<IEnumerable<CategoryDto>>()
                    {
                        Data = result,
                        NextPage = pagedCategories.MetaData.HasNext ? pagedCategories.MetaData.CurrentPage + 1 : null,
                        PreviousPage = pagedCategories.MetaData.HasPrevious ? pagedCategories.MetaData.CurrentPage - 1 : null,
                        PageSize = pagedCategories.MetaData.PageSize,
                        TotalRecords = pagedCategories.MetaData.TotalCount,
                        TotalPages = pagedCategories.MetaData.TotalPages,
                        PageNumber = pagedCategories.MetaData.CurrentPage
                    };
                    return APIResponse<object>.Create(HttpStatusCode.OK, "Categories retrieved successfully", responseToReturn);

                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred when trying to retrieve categories: {ex.Message}");
                return APIResponse<object>.Create(HttpStatusCode.InternalServerError, "An error occurred when trying to retrieve categories", null);
            }
        }

        public async Task<APIResponse<CategoryDto>> GetCategory(int categoryId)
        {
            try
            {
                var category = await _categoryRepository.GetByIdAsync(categoryId);
                if (category == null)
                {
                    return APIResponse<CategoryDto>.Create(HttpStatusCode.NotFound, "Category not found", null);
                }
                var categoryToReturn = _mapper.Map<CategoryDto>(category);
                return APIResponse<CategoryDto>.Create(HttpStatusCode.OK, "Category retrieved successfully", categoryToReturn);

            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred when trying to create a new category: {ex.Message}");
                return APIResponse<CategoryDto>.Create(HttpStatusCode.InternalServerError, "An error occurred when trying to retrieve category", null);
            }
        }

        public async Task<APIResponse<object>> UpdateCategory(int categoryId, UpdateCategoryDto updateCategory)
        {
            try
            {
                var category = await _categoryRepository.GetByIdAsync(categoryId);
                if (category == null)
                {
                    return APIResponse<object>.Create(HttpStatusCode.NotFound, "Category not found", null);
                }
                _mapper.Map(updateCategory, category);
                await _categoryRepository.UpdateAsync(category);
                await _categoryRepository.SaveChangesAsync();
                return APIResponse<object>.Create(HttpStatusCode.OK, "Category updated successfully", null);

            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred when trying to update a category: {ex.Message}");
                return APIResponse<object>.Create(HttpStatusCode.InternalServerError, "An error occurred when trying to update category", null);

            }
        }
    }
}
