using ECommerceService.API.Application.Interfaces;
using ECommerceService.API.Data.Dtos;
using ECommerceService.API.Helpers;
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
        [HttpGet]
        public async Task<IActionResult> GetAllProductsAsync()
        {
            var response = await _productService.GetAllProductsAsync();
            return StatusCode((int)response.StatusCode, response);
        }
        [HttpGet("{productId}")]
        public async Task<IActionResult> GetProduct(Guid productId)
        {
            var response = await _productService.GetProductAsync(productId);
            return StatusCode((int)response.StatusCode, response);
        }
        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductDto createProduct)
        {
            var response = await _productService.CreateProduct(createProduct);
            return StatusCode((int)response.StatusCode, response);
        }
        [HttpPut("{productId}")]
        public async Task<IActionResult> UpdateProduct(Guid productId, [FromBody] UpdateProductDto updateProduct)
        {
            var response = await _productService.UpdateProduct(productId, updateProduct);
            return StatusCode((int)response.StatusCode, response);
        }
        [HttpDelete("{productId}")]
        public async Task<IActionResult> DeleteProduct(Guid productId)
        {
            var response = await _productService.DeleteProduct(productId);
            return StatusCode((int)response.StatusCode, response);
        }
    }
}
