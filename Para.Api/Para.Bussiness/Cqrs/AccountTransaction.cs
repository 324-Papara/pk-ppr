using MediatR;
using Para.Base.Response;
using Para.Schema;

namespace Para.Bussiness.Cqrs;

public record CreateAccountTransactionCommand(AccountTransactionRequest Request) : IRequest<ApiResponse<AccountTransactionResponse>>;
public record UpdateAccountTransactionCommand(long AccountTransactionId,AccountTransactionRequest Request) : IRequest<ApiResponse>;
public record DeleteAccountTransactionCommand(long AccountTransactionId) : IRequest<ApiResponse>;

public record GetAllAccountTransactionQuery() : IRequest<ApiResponse<List<AccountTransactionResponse>>>;
public record GetAccountTransactionByIdQuery(long AccountTransactionId) : IRequest<ApiResponse<AccountTransactionResponse>>;
public record GetAccountTransactionByCustomerIdQuery () : IRequest<ApiResponse<List<AccountTransactionResponse>>>;