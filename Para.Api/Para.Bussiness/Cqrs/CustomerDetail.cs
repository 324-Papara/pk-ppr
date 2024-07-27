using MediatR;
using Para.Base.Response;
using Para.Schema;

namespace Para.Bussiness.Cqrs;

public record CreateCustomerDetailCommand(CustomerDetailRequest Request) : IRequest<ApiResponse<CustomerDetailResponse>>;
public record UpdateCustomerDetailCommand(long CustomerDetailId,CustomerDetailRequest Request) : IRequest<ApiResponse>;
public record DeleteCustomerDetailCommand(long CustomerDetailId) : IRequest<ApiResponse>;

public record GetAllCustomerDetailQuery() : IRequest<ApiResponse<List<CustomerDetailResponse>>>;
public record GetCustomerDetailByIdQuery(long CustomerDetailId) : IRequest<ApiResponse<CustomerDetailResponse>>;