using ECommerceService.API.Application.Interfaces;
using ECommerceService.API.Data.Dtos.Category;
using ECommerceService.API.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAllCategoriesAsync([FromQuery] RequestParameters requestParameters, bool isPaginated, [FromQuery]Sorting? sortingParameters, [FromQuery]string? searchParameter)
        {
            var response = await _categoryService.GetAllCategoriesAsync(requestParameters, isPaginated, sortingParameters, searchParameter);
            return StatusCode((int)response.StatusCode, response);
        }
        [AllowAnonymous]
        [HttpGet("{categoryId}")]
        public async Task<IActionResult> GetCategory(int categoryId)
        {
            var response = await _categoryService.GetCategory(categoryId);
            return StatusCode((int)response.StatusCode, response);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryDto createCategory)
        {
            if(!ModelState.IsValid)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Invalid data provided");
            }
            var response = await _categoryService.CreateCategory(createCategory);
            return StatusCode((int)response.StatusCode, response);
        }
        [Authorize(Roles = "Admin")]
        [HttpPut("{categoryId}")]
        public async Task<IActionResult> UpdateCategory(int categoryId, [FromBody] UpdateCategoryDto updateCategory)
        {
            if(!ModelState.IsValid)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Invalid data provided");
            }
            var response = await _categoryService.UpdateCategory(categoryId, updateCategory);
            return StatusCode((int)response.StatusCode, response);
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("{categoryId}")]
        public async Task<IActionResult> DeleteCategory(int categoryId)
        {
            var response = await _categoryService.DeleteCategory(categoryId);
            return StatusCode((int)response.StatusCode, response);
        }
    }
}
