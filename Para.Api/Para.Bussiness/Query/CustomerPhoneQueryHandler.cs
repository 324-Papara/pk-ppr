using AutoMapper;
using MediatR;
using Para.Base;
using Para.Base.Response;
using Para.Bussiness.Cqrs;
using Para.Data.Domain;
using Para.Data.UnitOfWork;
using Para.Schema;

namespace Para.Bussiness.Query;

public class CustomerPhoneQueryHandler : 
    IRequestHandler<GetAllCustomerPhoneQuery,ApiResponse<List<CustomerPhoneResponse>>>,
    IRequestHandler<GetCustomerPhoneByIdQuery,ApiResponse<CustomerPhoneResponse>>,
    IRequestHandler<GetCustomerPhoneByCustomerIdQuery, ApiResponse<List<CustomerPhoneResponse>>>

{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;
    private readonly ISessionContext sessionContext;

    public CustomerPhoneQueryHandler(IUnitOfWork unitOfWork, IMapper mapper,ISessionContext sessionContext)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
        this.sessionContext = sessionContext;
    }
    
    public async Task<ApiResponse<List<CustomerPhoneResponse>>> Handle(GetAllCustomerPhoneQuery request, CancellationToken cancellationToken)
    {
        List<CustomerPhone> entityList = await unitOfWork.CustomerPhoneRepository.GetAll("Customer");
        var mappedList = mapper.Map<List<CustomerPhoneResponse>>(entityList);
        return new ApiResponse<List<CustomerPhoneResponse>>(mappedList);
    }

    public async Task<ApiResponse<CustomerPhoneResponse>> Handle(GetCustomerPhoneByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await unitOfWork.CustomerPhoneRepository.GetById(request.CustomerPhoneId,"Customer");
        var mapped = mapper.Map<CustomerPhoneResponse>(entity);
        return new ApiResponse<CustomerPhoneResponse>(mapped);
    }

    public async Task<ApiResponse<List<CustomerPhoneResponse>>> Handle(GetCustomerPhoneByCustomerIdQuery request, CancellationToken cancellationToken)
    {
        List<CustomerPhone> entityList = await unitOfWork.CustomerPhoneRepository.Where(x=> x.CustomerId == sessionContext.Session.CustomerId, "Customer");
        var mappedList = mapper.Map<List<CustomerPhoneResponse>>(entityList);
        return new ApiResponse<List<CustomerPhoneResponse>>(mappedList);
    }
}