using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Para.Base.Response;
using Para.Bussiness.Cqrs;
using Para.Schema;

namespace Para.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountTransactionsController : ControllerBase
    {
        private readonly IMediator mediator;
        
        public AccountTransactionsController(IMediator mediator)
        {
            this.mediator = mediator;
        }


        [HttpGet]
        [Authorize(Roles = "admin")]
        public async Task<ApiResponse<List<AccountTransactionResponse>>> Get()
        {
            var operation = new GetAllAccountTransactionQuery();
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpGet("{AccountTransactionId}")]
        [Authorize(Roles = "admin")]
        public async Task<ApiResponse<AccountTransactionResponse>> Get([FromRoute]long AccountTransactionId)
        {
            var operation = new GetAccountTransactionByIdQuery(AccountTransactionId);
            var result = await mediator.Send(operation);
            return result;
        }
        
        [HttpGet("ByCustomer")]
        [Authorize(Roles = "customer")]
        public async Task<ApiResponse<List<AccountTransactionResponse>>> GetByCustomerId()
        {
            var operation = new GetAccountTransactionByCustomerIdQuery();
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<ApiResponse<AccountTransactionResponse>> Post([FromBody] AccountTransactionRequest value)
        {
            var operation = new CreateAccountTransactionCommand(value);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpPut("{AccountTransactionId}")]
        [Authorize(Roles = "admin")]
        public async Task<ApiResponse> Put(long AccountTransactionId, [FromBody] AccountTransactionRequest value)
        {
            var operation = new UpdateAccountTransactionCommand(AccountTransactionId,value);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpDelete("{AccountTransactionId}")]
        [Authorize(Roles = "admin")]
        public async Task<ApiResponse> Delete(long AccountTransactionId)
        {
            var operation = new DeleteAccountTransactionCommand(AccountTransactionId);
            var result = await mediator.Send(operation);
            return result;
        }
    }
}