using AutoMapper;
using MediatR;
using Para.Base;
using Para.Base.Response;
using Para.Bussiness.Cqrs;
using Para.Data.Domain;
using Para.Data.UnitOfWork;
using Para.Schema;

namespace Para.Bussiness.Query;

public class CustomerAddressQueryHandler : 
    IRequestHandler<GetAllCustomerAddressQuery,ApiResponse<List<CustomerAddressResponse>>>,
    IRequestHandler<GetCustomerAddressByIdQuery,ApiResponse<CustomerAddressResponse>>,
    IRequestHandler<GetCustomerAddressByCustomerIdQuery,ApiResponse<List<CustomerAddressResponse>>>
    
    
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;
    private readonly ISessionContext sessionContext;

    public CustomerAddressQueryHandler(IUnitOfWork unitOfWork, IMapper mapper,ISessionContext sessionContext)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
        this.sessionContext = sessionContext;
    }
    
    public async Task<ApiResponse<List<CustomerAddressResponse>>> Handle(GetAllCustomerAddressQuery request, CancellationToken cancellationToken)
    {
        List<CustomerAddress> entityList = await unitOfWork.CustomerAddressRepository.GetAll("Customer");
        var mappedList = mapper.Map<List<CustomerAddressResponse>>(entityList);
        return new ApiResponse<List<CustomerAddressResponse>>(mappedList);
    }

    public async Task<ApiResponse<CustomerAddressResponse>> Handle(GetCustomerAddressByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await unitOfWork.CustomerAddressRepository.GetById(request.CustomerAddressId,"Customer");
        var mapped = mapper.Map<CustomerAddressResponse>(entity);
        return new ApiResponse<CustomerAddressResponse>(mapped);
    }

    public async Task<ApiResponse<List<CustomerAddressResponse>>> Handle(GetCustomerAddressByCustomerIdQuery request, CancellationToken cancellationToken)
    {
        var customerId = sessionContext.Session.CustomerId;
        List<CustomerAddress> entityList = await unitOfWork.CustomerAddressRepository.Where(x=> x.CustomerId == customerId,"Customer");
        var mappedList = mapper.Map<List<CustomerAddressResponse>>(entityList);
        return new ApiResponse<List<CustomerAddressResponse>>(mappedList);
    }
}