using MediatR;
using Para.Base.Response;
using Para.Schema;

namespace Para.Bussiness.Cqrs;

public record CreateCustomerPhoneCommand(CustomerPhoneRequest Request) : IRequest<ApiResponse<CustomerPhoneResponse>>;
public record UpdateCustomerPhoneCommand(long CustomerPhoneId,CustomerPhoneRequest Request) : IRequest<ApiResponse>;
public record DeleteCustomerPhoneCommand(long CustomerPhoneId) : IRequest<ApiResponse>;

public record GetAllCustomerPhoneQuery() : IRequest<ApiResponse<List<CustomerPhoneResponse>>>;
public record GetCustomerPhoneByIdQuery(long CustomerPhoneId) : IRequest<ApiResponse<CustomerPhoneResponse>>;