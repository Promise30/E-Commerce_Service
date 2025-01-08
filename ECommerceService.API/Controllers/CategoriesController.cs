using ECommerceService.API.Application.Interfaces;
using ECommerceService.API.Data.Dtos;
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
        [HttpGet]
        public async Task<IActionResult> GetAllCategoriesAsync()
        {
            var response = await _categoryService.GetAllCategoriesAsync();
            return StatusCode((int)response.StatusCode, response);
        }
        [HttpGet("{categoryId}")]
        public async Task<IActionResult> GetCategory(int categoryId)
        {
            var response = await _categoryService.GetCategory(categoryId);
            return StatusCode((int)response.StatusCode, response);
        }
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
        [HttpDelete("{categoryId}")]
        public async Task<IActionResult> DeleteCategory(int categoryId)
        {
            var response = await _categoryService.DeleteCategory(categoryId);
            return StatusCode((int)response.StatusCode, response);
        }
    }
}
