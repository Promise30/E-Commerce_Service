using ECommerceService.API.Application.Interfaces;
using ECommerceService.API.Data.Dtos.Product;
using ECommerceService.API.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }
        [AllowAnonymous]
        [HttpGet()]
        public async Task<IActionResult> GetAllPaginatedProduct([FromQuery]RequestParameters requestParameters, bool isPaginated, [FromQuery] Sorting? sortParameters, [FromQuery] FilterParameters? filterParameters)
        {
            var response = await _productService.GetAllProductsAsync(requestParameters, isPaginated, sortParameters, filterParameters);
            return StatusCode((int)response.StatusCode, response);
        }
        [AllowAnonymous]
        [HttpGet("{productId:int}")]
        public async Task<IActionResult> GetProduct(int productId)
        {
            var response = await _productService.GetProductAsync(productId);
            return StatusCode((int)response.StatusCode, response);
        }
        [Authorize(Roles = "Admin")]    
        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductDto createProduct)
        {
            var response = await _productService.CreateProduct(createProduct);
            return StatusCode((int)response.StatusCode, response);
        }
        [Authorize(Roles = "Admin")]
        [HttpPut("{productId}")]
        public async Task<IActionResult> UpdateProduct(int productId, [FromBody] UpdateProductDto updateProduct)
        {
            var response = await _productService.UpdateProduct(productId, updateProduct);
            return StatusCode((int)response.StatusCode, response);
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("{productId}")]
        public async Task<IActionResult> DeleteProduct(int productId)
        {
            var response = await _productService.DeleteProduct(productId);
            return StatusCode((int)response.StatusCode, response);
        }
    }
}
