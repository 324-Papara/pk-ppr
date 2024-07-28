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
    public class AccountsController : ControllerBase
    {
        private readonly IMediator mediator;
        
        public AccountsController(IMediator mediator)
        {
            this.mediator = mediator;
        }


        [HttpGet]
        [Authorize(Roles = "admin")]
        public async Task<ApiResponse<List<AccountResponse>>> Get()
        {
            var operation = new GetAllAccountQuery();
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpGet("{AccountId}")]
        [Authorize(Roles = "admin")]
        public async Task<ApiResponse<AccountResponse>> Get([FromRoute]long AccountId)
        {
            var operation = new GetAccountByIdQuery(AccountId);
            var result = await mediator.Send(operation);
            return result;
        }
        
        [HttpGet("ByCustomer")]
        [Authorize(Roles = "customer")]
        public async Task<ApiResponse<List<AccountResponse>>> GetByCustomerId()
        {
            var operation = new GetAccountByCustomerIdQuery();
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<ApiResponse<AccountResponse>> Post([FromBody] AccountRequest value)
        {
            var operation = new CreateAccountCommand(value);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpPut("{AccountId}")]
        [Authorize(Roles = "admin")]
        public async Task<ApiResponse> Put(long AccountId, [FromBody] AccountRequest value)
        {
            var operation = new UpdateAccountCommand(AccountId,value);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpDelete("{AccountId}")]
        [Authorize(Roles = "admin")]
        public async Task<ApiResponse> Delete(long AccountId)
        {
            var operation = new DeleteAccountCommand(AccountId);
            var result = await mediator.Send(operation);
            return result;
        }
    }
}