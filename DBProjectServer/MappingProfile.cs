using AutoMapper;
using Entities.Models;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Customer, CustomerDto>()
            .ForMember(dest => dest.customerId, opt => opt.MapFrom(src => src.id));
        CreateMap<CustomerCreationDto, Customer>();
        CreateMap<CustomerForUpdateDto, Customer>();

        CreateMap<Address, AddressDto>()
            .ForMember(dest => dest.addressId, opt => opt.MapFrom(src => src.id));
        CreateMap<AddressCreationDto, Address>();
        CreateMap<AddressForUpdateDto, Address>();

        CreateMap<Category, CategoryDto>()
            .ForMember(dest => dest.categoryId, opt => opt.MapFrom(src => src.id));
        CreateMap<CategoryCreationDto, Category>();
        CreateMap<CategoryForUpdateDto, Category>();

        CreateMap<Product, ProductDto>()
            .ForMember(dest => dest.productId, opt => opt.MapFrom(src => src.id));
        CreateMap<ProductCreationDto, Product>();
        CreateMap<ProductForUpdateDto, Product>();

        CreateMap<Size, SizeDto>()
            .ForMember(dest => dest.sizeId, opt => opt.MapFrom(src => src.id));
        CreateMap<SizeCreationDto, Size>();
        CreateMap<SizeForUpdateDto, Size>();

        CreateMap<Color, ColorDto>()
            .ForMember(dest => dest.colorId, opt => opt.MapFrom(src => src.id));
        CreateMap<ColorCreationDto, Color>();
        CreateMap<ColorForUpdateDto, Color>();


        CreateMap<User, UserDto>()
            .ForMember(dest => dest.userId, opt => opt.MapFrom(src => src.id));
        CreateMap<UserCreationDto, User>();
        CreateMap<UserForUpdateDto, User>();

        CreateMap<Rol, RolDto>()
            .ForMember(dest => dest.rolId, opt => opt.MapFrom(src => src.id));

        CreateMap<Order, OrderDto>()
            .ForMember(dest => dest.orderId, opt => opt.MapFrom(src => src.id));
        CreateMap<OrderCreationDto, Order>();
        CreateMap<OrderForUpdateDto, Order>();

    }
}