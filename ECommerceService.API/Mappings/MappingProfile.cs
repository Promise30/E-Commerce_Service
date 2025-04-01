using AutoMapper;
using ECommerceService.API.Data.Dtos.Auth;
using ECommerceService.API.Data.Dtos.CartItem;
using ECommerceService.API.Data.Dtos.Category;
using ECommerceService.API.Data.Dtos.Order;
using ECommerceService.API.Data.Dtos.Product;
using ECommerceService.API.Domain.Entities;
using ECommerceService.API.Helpers;
using k8s.Authentication;

namespace ECommerceService.API.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateProductDto, Product>();
            CreateMap<UpdateProductDto, Product>();
            CreateMap<CreateCategoryDto, Category>();
            CreateMap<UpdateCategoryDto, Category>();
            CreateMap<Product, ProductDto>();
            CreateMap<Category, CategoryDto>();
            CreateMap<ApplicationRole,  ApplicationRoleDto>().ReverseMap();
            CreateMap<CreateRoleDto, ApplicationRoleDto>();
            CreateMap<UpdateRoleDto, ApplicationRoleDto>();
            CreateMap<ApplicationUser, ApplicationUserDto>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Id));
            CreateMap<Cart, CartDto>().ReverseMap();
            CreateMap<CartItem, CartItemDto>().ReverseMap();
            CreateMap<AddCartItemDto, CartItem>();

            CreateMap<Order, OrderDto>()
                .ForMember(dest => dest.PaymentStatus, opt => opt.MapFrom(src => src.PaymentStatus.GetEnumDescription()))
                .ForMember(dest => dest.OrderStatus, opt => opt.MapFrom(src => src.OrderStatus.GetEnumDescription()))
                .ForMember(dest => dest.CustomerLastName, opt => opt.MapFrom(src => src.User.LastName))
                .ForMember(dest => dest.CustomerFirstName, opt => opt.MapFrom(src => src.User.FirstName))
                .ForMember(dest=> dest.CreatedDate , opt => opt.MapFrom(src => src.CreatedDate));

            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(dest=> dest.DateCreated, opt => opt.MapFrom(src => src.CreatedDate));



        }
    }
}
