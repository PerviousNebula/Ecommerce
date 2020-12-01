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
    }
}