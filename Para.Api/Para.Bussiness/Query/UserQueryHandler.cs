using AutoMapper;
using MediatR;
using Para.Base;
using Para.Base.Response;
using Para.Bussiness.Cqrs;
using Para.Data.Domain;
using Para.Data.UnitOfWork;
using Para.Schema;

namespace Para.Bussiness.Query;

public class UserQueryHandler : 
    IRequestHandler<GetAllUserQuery,ApiResponse<List<UserResponse>>>,
    IRequestHandler<GetUserByIdQuery,ApiResponse<UserResponse>>,
    IRequestHandler<GetUserByCustomerIdQuery, ApiResponse<List<UserResponse>>>

{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;
    private readonly ISessionContext sessionContext;

    public UserQueryHandler(IUnitOfWork unitOfWork, IMapper mapper,ISessionContext sessionContext)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
        this.sessionContext = sessionContext;
    }
    
    public async Task<ApiResponse<List<UserResponse>>> Handle(GetAllUserQuery request, CancellationToken cancellationToken)
    {
        List<User> entityList = await unitOfWork.UserRepository.GetAll("Customer");
        var mappedList = mapper.Map<List<UserResponse>>(entityList);
        return new ApiResponse<List<UserResponse>>(mappedList);
    }

    public async Task<ApiResponse<UserResponse>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await unitOfWork.UserRepository.GetById(request.UserId,"Customer");
        var mapped = mapper.Map<UserResponse>(entity);
        return new ApiResponse<UserResponse>(mapped);
    }

    public async Task<ApiResponse<List<UserResponse>>> Handle(GetUserByCustomerIdQuery request, CancellationToken cancellationToken)
    {
        List<User> entityList = await unitOfWork.UserRepository.Where(x=> x.CustomerId == sessionContext.Session.CustomerId, "Customer");
        var mappedList = mapper.Map<List<UserResponse>>(entityList);
        return new ApiResponse<List<UserResponse>>(mappedList);
    }
}