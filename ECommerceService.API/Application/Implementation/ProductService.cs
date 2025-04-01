using AutoMapper;
using ECommerceService.API.Application.Interfaces;
using ECommerceService.API.Data.Dtos.Product;
using ECommerceService.API.Database.Interface;
using ECommerceService.API.Domain.Entities;
using ECommerceService.API.Helpers;
using k8s.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Net;
using System.Transactions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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
                var productExists = await _productRepository.GetAsync(p => p.Name == createProduct.Name);
                if (productExists != null)
                {
                    return APIResponse<ProductDto>.Create(HttpStatusCode.Conflict, "Product already exists", null);
                }

                var productToCreate = _mapper.Map<Product>(createProduct);
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

        public async Task<APIResponse<object>> DeleteProduct(int productId)
        {
            try
            {
                var product = await _productRepository.GetByIdAsync(productId);
                if (product == null)
                {
                    return APIResponse<object>.Create(HttpStatusCode.NotFound, "Product not found", new Product());
                }
                await _productRepository.DeleteAsync(productId);
                await _productRepository.SaveChangesAsync();
                return APIResponse<object>.Create(HttpStatusCode.OK, "Product deleted successfully", null);

            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred when trying to delete a product: {ex.Message}");
                return APIResponse<object>.Create(HttpStatusCode.InternalServerError, "An error occurred when trying to delete product", null!);
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

        public async Task<APIResponse<object>> GetAllProductsAsync(RequestParameters requestParameters, bool isPaginated, Sorting? sortParameter, FilterParameters? filterParameters)
        {
            try
            {   
                var products = await _productRepository.GetAllAsync(null);
                if (products == null)
                {
                    return APIResponse<object>.Create(HttpStatusCode.NotFound, "Products not found", null);
                }

                // filtering
                if (filterParameters != null)
                {
                    if (!string.IsNullOrEmpty(filterParameters.SearchTerm))
                    {
                        products = products.Where(product => product.Name.Contains(filterParameters.SearchTerm.ToLower(), StringComparison.OrdinalIgnoreCase));
                    }
                    if (filterParameters.MinAmount.HasValue)
                    {
                        products = products.Where(product => product.Price >= filterParameters.MinAmount);
                    }
                    if (filterParameters.MaxAmount.HasValue)
                    {
                        products = products.Where(product => product.Price <= filterParameters.MaxAmount);
                    }
                }

                // sorting
                switch (sortParameter.SortBy?.ToLower())
                {
                    case "name":
                        products = sortParameter.IsAscending ? products.OrderBy(product => product.Name) : products.OrderByDescending(product => product.Name);
                        break;
                    case "description":
                        products = sortParameter.IsAscending ? products.OrderBy(product => product.Description) : products.OrderByDescending(product => product.Description);
                        break;
                    case "price":
                        products = sortParameter.IsAscending ? products.OrderBy(product => product.Price) : products.OrderByDescending(product => product.Price);
                        break;
                    case "stockquantity":
                        products = sortParameter.IsAscending ? products.OrderBy(product => product.StockQuantity) : products.OrderByDescending(product => product.StockQuantity);
                        break;
                    default:
                        products = sortParameter.IsAscending ? products.OrderBy(product => product.CreatedDate) : products.OrderByDescending(product => product.CreatedDate);
                        break;
                }

                // pagination
                if (!isPaginated)
                {
                    var productsToReturn = _mapper.Map<IEnumerable<ProductDto>>(products);
                    return APIResponse<object>.Create(HttpStatusCode.OK, "Products retrieved successfully", productsToReturn);
                }
                else
                {
                    var pagedProducts = await _productRepository.GetAllPaginatedAsync(requestParameters, products.ToList());
                    var result = pagedProducts.Data.Select(product => _mapper.Map<ProductDto>(product));
                    var responseToReturn = new PagedResponse<IEnumerable<ProductDto>>()
                    {
                        Data = result,
                        NextPage = pagedProducts.MetaData.HasNext ? pagedProducts.MetaData.CurrentPage + 1 : null,
                        PreviousPage = pagedProducts.MetaData.HasPrevious ? pagedProducts.MetaData.CurrentPage - 1 : null,
                        PageSize = pagedProducts.MetaData.PageSize,
                        TotalRecords = pagedProducts.MetaData.TotalCount,
                        TotalPages = pagedProducts.MetaData.TotalPages,
                        PageNumber = pagedProducts.MetaData.CurrentPage
                    };
                    return APIResponse<object>.Create(HttpStatusCode.OK, "Products retrieved successfully", responseToReturn);

                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred when trying to retrieve products: {ex.Message}");
                return APIResponse<object>.Create(HttpStatusCode.InternalServerError, "An error occurred when trying to retrieve products", null);
            }
        }

        public async Task<APIResponse<ProductDto>> GetProductAsync(int productId)
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

        public async Task<APIResponse<object>> UpdateProduct(int productId, UpdateProductDto updateProduct)
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
