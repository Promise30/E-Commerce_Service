using AutoMapper;
using ECommerceService.API.Application.Interfaces;
using ECommerceService.API.Data;
using ECommerceService.API.Data.Dtos;
using ECommerceService.API.Data.Dtos.Order;
using ECommerceService.API.Database.Interface;
using ECommerceService.API.Domain.Entities;
using ECommerceService.API.Domain.Enums;
using ECommerceService.API.Helpers;
using PayStack.Net;
using System.Net;
using System.Text;
using System.Text.Json;

namespace ECommerceService.API.Application.Implementation
{
    public partial class OrderService : IOrderService
    {
        private readonly ILogger<OrderService> _logger;
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderItemRepository _orderItemRepository;
        private readonly ICartRepository _cartRepository;
        private readonly ICartItemRepository _cartItemRepository;
        private readonly IMapper _mapper;
        private readonly ECommerceDbContext _dbContext;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly PayStackApi _payStackApi;
        private readonly IConfiguration _configuration;

        public OrderService(
            ILogger<OrderService> logger,
            IOrderRepository orderRepository,
            IOrderItemRepository orderItemRepository,
            ICartRepository cartRepository,
            ICartItemRepository cartItemRepository,
            IMapper mapper,
            IHttpClientFactory httpClientFactory,
            ECommerceDbContext dbContext,
            PayStackApi payStackApi,
            IConfiguration configuration)
        {
            _logger = logger;
            _orderRepository = orderRepository;
            _orderItemRepository = orderItemRepository;
            _cartRepository = cartRepository;
            _cartItemRepository = cartItemRepository;
            _mapper = mapper;
            _dbContext = dbContext;
            _payStackApi = payStackApi;
            _configuration = configuration;
        }
        public async Task<APIResponse<object>> CreateOrderFromCartAsync(string userId, CheckoutDto checkoutDto)
        {
            try
            {
                var cart = await _cartRepository.GetAsync(x => x.UserId == userId);
                if (cart == null || !cart.CartItems.Any())
                {
                    return APIResponse<object>.Create(HttpStatusCode.OK, "Cart is empty", null);
                }

                // calculate total costs
                decimal subTotals = cart.CartItems.Sum(x => x.Quantity * x.UnitPrice);
                decimal taxAmount = CalculateTax(subTotals);
                decimal totalAmount = subTotals + taxAmount;

                var discountAmount = CalculateDiscount(totalAmount);

                var order = new Order
                {
                    UserId = userId,
                    OrderStatus = OrderStatus.Pending,
                    PaymentStatus = PaymentStatus.Pending,
                    PaymentMethod = checkoutDto.PaymentMethod,
                    TotalAmount = totalAmount,
                    TaxAmount = taxAmount,
                    CreatedDate = DateTime.Now,
                    LastModifiedDate = DateTime.Now,
                    DiscountApplied = discountAmount
                };

                // Initiate payment
                var transactionRequest = new TransactionInitializeRequest
                {
                    Email = checkoutDto.Email,
                    Reference = Guid.NewGuid().ToString(),
                    CallbackUrl = string.Concat(_configuration["PayStack:CallbackUrl"], "/api/order/verify"),
                    AmountInKobo = (int)(totalAmount * 100),
                    Currency = "NGN",
                    Channels = new[] { checkoutDto.PaymentMethod },
                };

                var transactionResponse = _payStackApi.Transactions.Initialize(transactionRequest);

                if (!transactionResponse.Status)
                {
                    return APIResponse<object>.Create(HttpStatusCode.InternalServerError, "An error occurred while trying to initiate payment", null);
                }

                order.PaymentReference = transactionResponse.Data.Reference;
                await _orderRepository.CreateAsync(order);
                await _orderRepository.SaveChangesAsync();

                return APIResponse<object>.Create(HttpStatusCode.OK, "Successful", new PaymentInitializationResponse
                {
                    AuthorizationUrl = transactionResponse.Data.AuthorizationUrl,
                    PaymentReference = transactionResponse.Data.Reference
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while trying to create an order : {ex.Message}");
                return APIResponse<object>.Create(HttpStatusCode.InternalServerError, "Error occurred while trying to create order", null);
            }
        }
        public async Task<APIResponse<object>> VerifyAndFinalizeOrderAsync(string reference)
        {
            try
            {
                var order = await _orderRepository.GetAsync(x => x.PaymentReference == reference);

                if (order == null)
                {
                    return APIResponse<object>.Create(HttpStatusCode.NotFound, "Order not found", null);
                }

                // Verify payment with PayStack
                var verificationResponse = _payStackApi.Transactions.Verify(reference);
                if (!verificationResponse.Status || verificationResponse.Data.Status != "success")
                {
                    order.PaymentStatus = PaymentStatus.Failed;
                    await _orderRepository.SaveChangesAsync();
                    return APIResponse<object>.Create(HttpStatusCode.BadRequest, "Payment verification failed", null);
                }

                // Update order status
                order.PaymentStatus = PaymentStatus.Paid;
                order.OrderStatus = OrderStatus.Processing;
                order.LastModifiedDate = DateTimeOffset.UtcNow;

                // populate order item and update stock
                var cart = await _cartRepository.GetAsync(x => x.UserId == order.UserId);
                foreach (var cartItem in cart.CartItems)
                {
                    var product = cartItem.Product;
                    var orderItem = new OrderItem
                    {
                        ProductId = cartItem.ProductId,
                        Quantity = cartItem.Quantity,
                        SubTotal = cartItem.Quantity * product.Price,
                        UnitPrice = product.Price,
                        OrderId = order.Id,
                        CreatedDate = DateTimeOffset.UtcNow,
                        LastModifiedDate = DateTimeOffset.UtcNow
                    };
                    order.OrderItems.Add(orderItem);
                    product.StockQuantity -= cartItem.Quantity;
                };
                await _orderRepository.UpdateAsync(order);
                await _dbContext.SaveChangesAsync();

                // clear the cart
                cart.CartItems.Clear();
                cart.LastModifiedDate = DateTimeOffset.UtcNow;
                await _cartRepository.SaveChangesAsync();

                var orderDto = _mapper.Map<OrderDto>(order);
                return APIResponse<object>.Create(HttpStatusCode.OK, "Order verified and finalized successfully", orderDto);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred when trying to verify and finalize order: {ex.Message}");
                return APIResponse<object>.Create(HttpStatusCode.InternalServerError, "An error occurred when trying to verify and finalize order", null);
            }
        }

        public async Task<APIResponse<object>> GetOrderAsync(int orderId, string userId)
        {
            try
            {
                var order = await _orderRepository.GetAsync(x => x.Id == orderId && x.UserId == userId);
                if (order == null)
                {
                    return APIResponse<object>.Create(HttpStatusCode.NotFound, "Order not found", null);
                }
                var orderDto = _mapper.Map<OrderDto>(order);
                return APIResponse<object>.Create(HttpStatusCode.OK, "Order retrieved successfully", orderDto);

            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred when trying to get an order item: {ex.Message}");
                return APIResponse<object>.Create(HttpStatusCode.InternalServerError, "An error occurred when trying to get an order item", null);
            }
        }
        public async Task<APIResponse<object>> GetAllOrderHistoryByAdminAsync(RequestParameters requestParameters, Sorting? sortParameters, bool isPaginated, OrderFilterParameters orderFilterParameters)
        {
            try
            {
                var orders = await _orderRepository.GetAllAsync(null);
                if (orders == null || !orders.Any())
                {
                    return APIResponse<object>.Create(HttpStatusCode.NotFound, "No order records found", null);
                }

                // filtering
                if (orderFilterParameters != null)
                {
                    if (!string.IsNullOrEmpty(orderFilterParameters.PaymentStatus))
                    {
                        orders = orders.Where(o => o.PaymentStatus.GetEnumDescription().ToLower() == orderFilterParameters.PaymentStatus.ToLower());
                    }
                    if (!string.IsNullOrEmpty(orderFilterParameters.OrderStatus))
                    {
                        orders = orders.Where(o => o.OrderStatus.GetEnumDescription().ToLower() == orderFilterParameters.OrderStatus.ToLower());
                    }
                    if (orderFilterParameters.MinAmount.HasValue)
                    {
                        orders = orders.Where(o => o.TotalAmount >= orderFilterParameters.MinAmount);
                    }
                    if (orderFilterParameters.MaxAmount.HasValue)
                    {
                        orders = orders.Where(o => o.TotalAmount <= orderFilterParameters.MaxAmount);
                    }
                    if (orderFilterParameters.Duration.HasValue)
                    {
                        switch (orderFilterParameters.Duration.Value.ToString())
                        {
                            case "7":
                                orders = orders.Where(o => o.CreatedDate >= DateTimeOffset.Now.AddDays(-7));
                                break;
                            case "30":
                                orders = orders.Where(o => o.CreatedDate >= DateTimeOffset.Now.AddDays(-30));
                                break;
                            case "90":
                                orders = orders.Where(o => o.CreatedDate >= DateTimeOffset.Now.AddDays(-90));
                                break;
                            case "365":
                                orders = orders.Where(o => o.CreatedDate >= DateTimeOffset.Now.AddDays(-365));
                                break;
                            default:
                                break;
                        }
                    }
                }

                // sorting
                switch (sortParameters.SortBy?.ToLower())
                {
                    case "totalamount":
                        orders = sortParameters.IsAscending == true ? orders.OrderBy(o => o.TotalAmount) : orders.OrderByDescending(o => o.TotalAmount);
                        break;
                    default:
                        orders = sortParameters.IsAscending == true ? orders.OrderBy(o => o.CreatedDate) : orders.OrderByDescending(o => o.CreatedDate);
                        break;
                }
                if (!isPaginated)
                {
                    var orderDtos = _mapper.Map<IEnumerable<OrderDto>>(orders);
                    return APIResponse<object>.Create(HttpStatusCode.OK, "Order history retrieved successfully", orderDtos);
                }
                else
                {
                    var pagedOrders = await _orderRepository.GetAllPaginatedAsync(requestParameters, orders.ToList());
                    var result = pagedOrders.Data.Select(order => _mapper.Map<OrderDto>(order));
                    var responseToReturn = new PagedResponse<IEnumerable<OrderDto>>()
                    {
                        Data = result,
                        NextPage = pagedOrders.MetaData.HasNext ? pagedOrders.MetaData.CurrentPage + 1 : null,
                        PreviousPage = pagedOrders.MetaData.HasPrevious ? pagedOrders.MetaData.CurrentPage - 1 : null,
                        PageSize = pagedOrders.MetaData.PageSize,
                        TotalRecords = pagedOrders.MetaData.TotalCount,
                        TotalPages = pagedOrders.MetaData.TotalPages,
                        PageNumber = pagedOrders.MetaData.CurrentPage
                    };
                    return APIResponse<object>.Create(HttpStatusCode.OK, "Orders history retrieved successfully", responseToReturn);

                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred when trying to get customer order history: {ex.Message}");
                return APIResponse<object>.Create(HttpStatusCode.InternalServerError, "An error occurred when trying to get customer order history.", null);
            }

        }
        public async Task<APIResponse<object>> GetOrderHistory(string userId, RequestParameters requestParameters, Sorting? sortParameters, bool isPaginated, OrderFilterParameters orderFilterParameters)
        {
            try
            {
                var orders = await _orderRepository.GetAllAsync(x => x.UserId == userId);
                if (orders == null || !orders.Any())
                {
                    return APIResponse<object>.Create(HttpStatusCode.NotFound, "No order records found", null);
                }

                // filtering
                if (orderFilterParameters != null)
                {
                    if (!string.IsNullOrEmpty(orderFilterParameters.PaymentStatus))
                    {
                        orders = orders.Where(o => o.PaymentStatus.GetEnumDescription().ToLower() == orderFilterParameters.PaymentStatus.ToLower());
                    }
                    if (!string.IsNullOrEmpty(orderFilterParameters.OrderStatus))
                    {
                        orders = orders.Where(o => o.OrderStatus.GetEnumDescription().ToLower() == orderFilterParameters.OrderStatus.ToLower());
                    }
                    if (orderFilterParameters.MinAmount.HasValue)
                    {
                        orders = orders.Where(o => o.TotalAmount >= orderFilterParameters.MinAmount);
                    }
                    if (orderFilterParameters.MaxAmount.HasValue)
                    {
                        orders = orders.Where(o => o.TotalAmount <= orderFilterParameters.MaxAmount);
                    }
                    if (orderFilterParameters.Duration.HasValue)
                    {
                        switch (orderFilterParameters.Duration.Value.ToString())
                        {
                            case "7":
                                orders = orders.Where(o => o.CreatedDate >= DateTimeOffset.Now.AddDays(-7));
                                break;
                            case "30":
                                orders = orders.Where(o => o.CreatedDate >= DateTimeOffset.Now.AddDays(-30));
                                break;
                            case "90":
                                orders = orders.Where(o => o.CreatedDate >= DateTimeOffset.Now.AddDays(-90));
                                break;
                            case "365":
                                orders = orders.Where(o => o.CreatedDate >= DateTimeOffset.Now.AddDays(-365));
                                break;
                            default:
                                break;
                        }
                    }
                }

                // sorting
                switch (sortParameters.SortBy?.ToLower())
                {
                    case "totalamount":
                        orders = sortParameters.IsAscending == true ? orders.OrderBy(o => o.TotalAmount) : orders.OrderByDescending(o => o.TotalAmount);
                        break;
                    default:
                        orders = sortParameters.IsAscending == true ? orders.OrderBy(o => o.CreatedDate) : orders.OrderByDescending(o => o.CreatedDate);
                        break;
                }
                if (!isPaginated)
                {
                    var orderDtos = _mapper.Map<IEnumerable<OrderDto>>(orders);
                    return APIResponse<object>.Create(HttpStatusCode.OK, "Order history retrieved successfully", orderDtos);
                }
                else
                {
                    var pagedOrders = await _orderRepository.GetAllPaginatedAsync(requestParameters, orders.ToList());
                    var result = pagedOrders.Data.Select(order => _mapper.Map<OrderDto>(order));
                    var responseToReturn = new PagedResponse<IEnumerable<OrderDto>>()
                    {
                        Data = result,
                        NextPage = pagedOrders.MetaData.HasNext ? pagedOrders.MetaData.CurrentPage + 1 : null,
                        PreviousPage = pagedOrders.MetaData.HasPrevious ? pagedOrders.MetaData.CurrentPage - 1 : null,
                        PageSize = pagedOrders.MetaData.PageSize,
                        TotalRecords = pagedOrders.MetaData.TotalCount,
                        TotalPages = pagedOrders.MetaData.TotalPages,
                        PageNumber = pagedOrders.MetaData.CurrentPage
                    };
                    return APIResponse<object>.Create(HttpStatusCode.OK, "Orders history retrieved successfully", responseToReturn);

                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred when trying to get customer order history: {ex.Message}");
                return APIResponse<object>.Create(HttpStatusCode.InternalServerError, "An error occurred when trying to get customer order history.", null);
            }

        }

        public async Task<APIResponse<object>> UpdateOrderStatusAsync(int orderId, OrderStatus orderStatus)
        {
            try
            {
                var order = await _orderRepository.GetByIdAsync(orderId);
                if (order == null)
                    return APIResponse<object>.Create(HttpStatusCode.NotFound, "Order not found", null);

                order.OrderStatus = orderStatus;
                order.LastModifiedDate = DateTimeOffset.UtcNow;

                await _orderRepository.UpdateAsync(order);

                return APIResponse<object>.Create(HttpStatusCode.OK, "Order status updated successfully", null);

            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred when trying to get update order status: {ex.Message}");
                return APIResponse<object>.Create(HttpStatusCode.InternalServerError, "An error occured when trying to update order status", null);
            }
        }
        public async Task<APIResponse<object>> CancelOrderAsync(int orderId)
        {
            try
            {
                var order = await _orderRepository.GetByIdAsync(orderId);
                if (order == null)
                    return APIResponse<object>.Create(HttpStatusCode.NotFound, "Order not found", null);
                order.OrderStatus = OrderStatus.Cancelled;
                order.LastModifiedDate = DateTimeOffset.UtcNow;
                await _orderRepository.UpdateAsync(order);
                return APIResponse<object>.Create(HttpStatusCode.OK, "Order cancelled successfully", null);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred when trying to cancel order: {ex.Message}");
                return APIResponse<object>.Create(HttpStatusCode.InternalServerError, "An error occured when trying to cancel order", null);
            }
        }
        public async Task<APIResponse<OrderHistorySummary>> GetOrderHistorySummaryByAdminAsync()
        {
            try
            {
                var orders = await _orderRepository.GetAllAsync(null);
                if (orders == null || !orders.Any())
                    return APIResponse<OrderHistorySummary>.Create(HttpStatusCode.NotFound, "No order summary records were found", null);

                var orderSummary = new OrderHistorySummary
                {
                    TotalOrders = orders.Count(),
                    TotalCancelledOrders = orders.Where(o => o.OrderStatus == OrderStatus.Cancelled).Count(),
                    TotalPaidOrders = orders.Where(o => o.PaymentStatus == PaymentStatus.Paid).Count(),
                    TotalPendingOrders = orders.Where(o => o.OrderStatus == OrderStatus.Pending).Count()
                };

                return APIResponse<OrderHistorySummary>.Create(HttpStatusCode.OK, "Order history summary record retrieved successfully", orderSummary);
            }
            catch (Exception ex)
            {

                _logger.LogError($"An error occurred when trying to retrieve order history summary: {ex.Message}");
                return APIResponse<OrderHistorySummary>.Create(HttpStatusCode.InternalServerError, "An error occured when trying to retrieve order history summary", null);
            }
        }

        private decimal CalculateTax(decimal subTotal) => subTotal * 0.075m;
        private bool EligibleForDiscount(decimal subTotal) => subTotal >= 10000m ? true : false;
        private decimal CalculateDiscount(decimal subTotal)
        {
            if (!EligibleForDiscount(subTotal))
            {
                return 0;
            }
            else return (subTotal * 0.02m);
        }
    }
}
