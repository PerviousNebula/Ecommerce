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
        CreateMap<AddressForUpdateDto, Address>()
            .ForMember(dest => dest.id, opt => opt.MapFrom(src => src.addressId));

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
        CreateMap<SizeForUpdateDto, Size>()
            .ForMember(dest => dest.id, opt => opt.MapFrom(src => src.sizeId));

        CreateMap<Color, ColorDto>()
            .ForMember(dest => dest.colorId, opt => opt.MapFrom(src => src.id));
        CreateMap<ColorCreationDto, Color>();
        CreateMap<ColorForUpdateDto, Color>()
            .ForMember(dest => dest.id, opt => opt.MapFrom(src => src.colorId));

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

        CreateMap<OrderDetail, OrderDetailDto>()
            .ForMember(dest => dest.orderDetailId, opt => opt.MapFrom(src => src.id));
        CreateMap<OrderDetailCreationDto, OrderDetail>();
        CreateMap<OrderDetailForUpdateDto, OrderDetail>();

        CreateMap<ProductDesign, ProductDesignDto>()
            .ForMember(dest => dest.productDesignId, opt => opt.MapFrom(src => src.id));
        CreateMap<ProductDesignCreationDto, ProductDesign>();
        CreateMap<ProductDesignForUpdateDto, ProductDesign>();

    }
}