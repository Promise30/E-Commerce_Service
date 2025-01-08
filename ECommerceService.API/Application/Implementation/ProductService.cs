using AutoMapper;
using ECommerceService.API.Application.Interfaces;
using ECommerceService.API.Data.Dtos;
using ECommerceService.API.Database.Interface;
using ECommerceService.API.Domain.Entities;
using ECommerceService.API.Helpers;
using System.Linq.Expressions;
using System.Net;

namespace ECommerceService.API.Application.Implementation
{
    public class ProductService : IProductService
    {
        private readonly IMapper _mapper;
        private readonly ILogger<ProductService> _logger;
        private readonly IProductRepository _productRepository;
        public ProductService(IMapper mapper, ILogger<ProductService> logger, IProductRepository productRepository)
        {
            _mapper = mapper;
            _logger = logger;
            _productRepository = productRepository;
        }
        public async Task<APIResponse<ProductDto>> CreateProduct(CreateProductDto createProduct)
        {
            try
            {
                Product productToCreate = _mapper.Map<Product>(createProduct);
                await _productRepository.CreateAsync(productToCreate);
                await _productRepository.SaveChangesAsync();
                var productToReturn = _mapper.Map<ProductDto>(productToCreate);
                return APIResponse<ProductDto>.Create(HttpStatusCode.Created, "Product created successfully", productToReturn);

            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred when trying to create a new category: {ex.Message}");
                return APIResponse<ProductDto>.Create(HttpStatusCode.InternalServerError, "An error occurred when trying to create a new category", null);
            }
        }

        public async Task<APIResponse<object>> DeleteProduct(Guid productId)
        {
            try
            {
                var product = await _productRepository.GetByIdAsync(productId);
                if (product == null)
                {
                    return APIResponse<object>.Create(HttpStatusCode.NotFound, "Product not found", null);
                }
                await _productRepository.DeleteAsync(productId);
                await _productRepository.SaveChangesAsync();
                return APIResponse<object>.Create(HttpStatusCode.OK, "Product deleted successfully", null);

            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred when trying to delete a product: {ex.Message}");
                return APIResponse<object>.Create(HttpStatusCode.InternalServerError, "An error occurred when trying to delete product", null);
            }
        }

        public async Task<APIResponse<IEnumerable<ProductDto>>> GetAllProductsAsync(Expression<Func<Product, bool>> filter = null)
        {
            try
            {
                var products = await _productRepository.GetAllAsync(filter);
                var productsToReturn = _mapper.Map<IEnumerable<ProductDto>>(products);
                return APIResponse<IEnumerable<ProductDto>>.Create(HttpStatusCode.OK, "Products retrieved successfully", productsToReturn);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred when trying to retrieve products: {ex.Message}");
                return APIResponse<IEnumerable<ProductDto>>.Create(HttpStatusCode.InternalServerError, "An error occurred when trying to retrieve products", null);
            }
        }

        public async Task<APIResponse<PagedList<ProductDto>>> GetAllProductsPaginatedAsync(RequestParameters requestParameters, Expression<Func<Product, bool>> filter = null)
        {
            try
            {
                var products = await _productRepository.GetAllPaginatedAsync(requestParameters, filter);
                var productsToReturn = _mapper.Map<PagedList<ProductDto>>(products);
                return APIResponse<PagedList<ProductDto>>.Create(HttpStatusCode.OK, "Products retrieved successfully", productsToReturn);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred when trying to retrieve products: {ex.Message}");
                return APIResponse<PagedList<ProductDto>>.Create(HttpStatusCode.InternalServerError, "An error occurred when trying to retrieve products", null);
            }
        }

        public async Task<APIResponse<ProductDto>> GetProductAsync(Guid productId)
        {
            try
            {
                var product = await _productRepository.GetByIdAsync(productId);
                if (product == null)
                {
                    return APIResponse<ProductDto>.Create(HttpStatusCode.NotFound, "Product not found", null);
                }
                var productToReturn = _mapper.Map<ProductDto>(product);
                return APIResponse<ProductDto>.Create(HttpStatusCode.OK, "Product retrieved successfully", productToReturn);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred when trying to retrieve a product: {ex.Message}");
                return APIResponse<ProductDto>.Create(HttpStatusCode.InternalServerError, "An error occurred when trying to retrieve product", null);
            }
        }

        public async Task<APIResponse<object>> UpdateProduct(Guid productId, UpdateProductDto updateProduct)
        {
            try
            {
                var product = await _productRepository.GetByIdAsync(productId);
                if (product == null)
                {
                    return APIResponse<object>.Create(HttpStatusCode.NotFound, "Product not found", null);
                }
                _mapper.Map(updateProduct, product);
                await _productRepository.UpdateAsync(product);
                await _productRepository.SaveChangesAsync();
                return APIResponse<object>.Create(HttpStatusCode.OK, "Product updated successfully", null);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred when trying to update a product: {ex.Message}");
                return APIResponse<object>.Create(HttpStatusCode.InternalServerError, "An error occurred when trying to update product", null);
            }
        }
    }
}
