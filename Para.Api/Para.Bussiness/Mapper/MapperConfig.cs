using AutoMapper;
using Para.Data.Domain;
using Para.Schema;

namespace Para.Bussiness;

public class MapperConfig : Profile
{

    public MapperConfig()
    {
        CreateMap<Customer, CustomerResponse>();
        CreateMap<CustomerRequest, Customer>();

        CreateMap<CustomerAddress, CustomerAddressResponse>()
            .ForMember(dest => dest.CustomerIdentityNumber, opt => opt.MapFrom(src => src.Customer.IdentityNumber))
            .ForMember(dest => dest.CustomerNumber, opt => opt.MapFrom(src => src.Customer.CustomerNumber))
            .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.Customer.FirstName + " " + src.Customer.LastName));
        CreateMap<CustomerAddressRequest, CustomerAddress>();
        
        CreateMap<CustomerPhone, CustomerPhoneResponse>()
            .ForMember(dest => dest.CustomerIdentityNumber, opt => opt.MapFrom(src => src.Customer.IdentityNumber))
            .ForMember(dest => dest.CustomerNumber, opt => opt.MapFrom(src => src.Customer.CustomerNumber))
            .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.Customer.FirstName + " " + src.Customer.LastName));
        CreateMap<CustomerPhoneRequest, CustomerPhone>();
        
        CreateMap<CustomerDetail, CustomerDetailResponse>()
            .ForMember(dest => dest.CustomerIdentityNumber, opt => opt.MapFrom(src => src.Customer.IdentityNumber))
            .ForMember(dest => dest.CustomerNumber, opt => opt.MapFrom(src => src.Customer.CustomerNumber))
            .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.Customer.FirstName + " " + src.Customer.LastName));
        CreateMap<CustomerDetailRequest, CustomerDetail>();
    }
}