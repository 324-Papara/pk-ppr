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
        
        CreateMap<CountryRequest, Country>();
        CreateMap<Country, CountryResponse>();
        
        CreateMap<User, UserResponse>()
            .ForMember(dest => dest.CustomerIdentityNumber, opt => opt.MapFrom(src => src.Customer.IdentityNumber))
            .ForMember(dest => dest.CustomerNumber, opt => opt.MapFrom(src => src.Customer.CustomerNumber))
            .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.Customer.FirstName + " " + src.Customer.LastName));
        CreateMap<UserRequest, User>();
        
        CreateMap<Account, AccountResponse>()
            .ForMember(dest => dest.CustomerIdentityNumber, opt => opt.MapFrom(src => src.Customer.IdentityNumber))
            .ForMember(dest => dest.CustomerNumber, opt => opt.MapFrom(src => src.Customer.CustomerNumber))
            .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.Customer.FirstName + " " + src.Customer.LastName));
        CreateMap<AccountRequest, Account>();
        
        CreateMap<AccountTransaction, AccountTransactionResponse>()
            .ForMember(dest => dest.CustomerIdentityNumber, opt => opt.MapFrom(src => src.Account.Customer.IdentityNumber))
            .ForMember(dest => dest.CustomerNumber, opt => opt.MapFrom(src => src.Account.Customer.CustomerNumber))
            .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.Account.Customer.FirstName + " " + src.Account.Customer.LastName));
        CreateMap<AccountTransactionRequest, AccountTransaction>();
    }
}