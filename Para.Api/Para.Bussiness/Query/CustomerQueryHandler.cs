using AutoMapper;
using LinqKit;
using MediatR;
using Para.Base;
using Para.Base.Response;
using Para.Bussiness.Cqrs;
using Para.Data.Domain;
using Para.Data.UnitOfWork;
using Para.Schema;

namespace Para.Bussiness.Query;

public class CustomerQueryHandler : 
    IRequestHandler<GetAllCustomerQuery,ApiResponse<List<CustomerResponse>>>,
    IRequestHandler<GetCustomerByIdQuery,ApiResponse<CustomerResponse>>,
    IRequestHandler<GetCustomerByParametersQuery,ApiResponse<List<CustomerResponse>>>,
    IRequestHandler<GetCustomerByCustomerIdQuery, ApiResponse<CustomerResponse>>

{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;
    private readonly ISessionContext sessionContext;

    public CustomerQueryHandler(IUnitOfWork unitOfWork, IMapper mapper,ISessionContext sessionContext)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
        this.sessionContext = sessionContext;
    }
    
    public async Task<ApiResponse<List<CustomerResponse>>> Handle(GetAllCustomerQuery request, CancellationToken cancellationToken)
    {
        List<Customer> entityList = await unitOfWork.CustomerRepository.GetAll("CustomerDetail","CustomerAddresses", "CustomerPhones");
        var mappedList = mapper.Map<List<CustomerResponse>>(entityList);
        return new ApiResponse<List<CustomerResponse>>(mappedList);
    }

    public async Task<ApiResponse<CustomerResponse>> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await unitOfWork.CustomerRepository.GetById(request.CustomerId,"CustomerDetail","CustomerAddresses", "CustomerPhones");
        var mapped = mapper.Map<CustomerResponse>(entity);
        return new ApiResponse<CustomerResponse>(mapped);
    }

    public async Task<ApiResponse<List<CustomerResponse>>> Handle(GetCustomerByParametersQuery request, CancellationToken cancellationToken)
    {
        var predicate = PredicateBuilder.New<Customer>(true);
        if (request.CustomerNumber > 0)
            predicate.And(x => x.CustomerNumber == request.CustomerNumber);
        if (!string.IsNullOrEmpty(request.FirstName))
            predicate.And(x => x.FirstName.ToUpper().Contains(request.FirstName.ToUpper()));
        if (!string.IsNullOrEmpty(request.LastName))
            predicate.And(x => x.LastName.ToUpper().Contains(request.LastName.ToUpper()));
        if (!string.IsNullOrEmpty(request.IdentityNumber))
            predicate.And(x => x.IdentityNumber.ToUpper().Contains(request.IdentityNumber.ToUpper()));
        
        List<Customer> entityList = await unitOfWork.CustomerRepository.Where(predicate,"CustomerDetail");
        
        var mappedList = mapper.Map<List<CustomerResponse>>(entityList);
        return new ApiResponse<List<CustomerResponse>>(mappedList);
    }

    public async Task<ApiResponse<CustomerResponse>> Handle(GetCustomerByCustomerIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await unitOfWork.CustomerRepository.GetById(sessionContext.Session.CustomerId,"CustomerDetail","CustomerAddresses", "CustomerPhones");
        var mapped = mapper.Map<CustomerResponse>(entity);
        return new ApiResponse<CustomerResponse>(mapped);
    }
}