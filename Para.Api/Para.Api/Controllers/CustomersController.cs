using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Para.Api.Filter;
using Para.Base.Response;
using Para.Bussiness.Cqrs;
using Para.Schema;

namespace Para.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly IMediator mediator;
        
        public CustomersController(IMediator mediator)
        {
            this.mediator = mediator;
        }


        [HttpGet]
        [Authorize(Roles = "admin")]
        public async Task<ApiResponse<List<CustomerResponse>>> Get()
        {
            var operation = new GetAllCustomerQuery();
            var result = await mediator.Send(operation);
            return result;
        }
        
        [HttpGet("ByParameters")]
        [Authorize(Roles = "admin")]
        public async Task<ApiResponse<List<CustomerResponse>>> GetByParameters(
            [FromQuery] long? CustomerNumber,
            [FromQuery] string FirstName = null,
            [FromQuery] string LastName = null,
            [FromQuery] string IdentityNumber = null)
        {
            var operation = new GetCustomerByParametersQuery(CustomerNumber,FirstName,LastName,IdentityNumber);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpGet("ByCustomer")]
        [Authorize(Roles = "customer")]
        public async Task<ApiResponse<CustomerResponse>> GetByCustomerId()
        {
            var operation = new GetCustomerByCustomerIdQuery();
            var result = await mediator.Send(operation);
            return result;
        }
        
        [HttpGet("{customerId}")]
        [Authorize(Roles = "admin")]
        public async Task<ApiResponse<CustomerResponse>> Get([FromRoute]long customerId)
        {
            var operation = new GetCustomerByIdQuery(customerId);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<ApiResponse<CustomerResponse>> Post([FromBody] CustomerRequest value)
        {
            var operation = new CreateCustomerCommand(value);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpPut("{customerId}")]
        [Authorize(Roles = "admin")]
        public async Task<ApiResponse> Put(long customerId, [FromBody] CustomerRequest value)
        {
            var operation = new UpdateCustomerCommand(customerId,value);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpDelete("{customerId}")]
        [Authorize(Roles = "admin")]
        public async Task<ApiResponse> Delete(long customerId)
        {
            var operation = new DeleteCustomerCommand(customerId);
            var result = await mediator.Send(operation);
            return result;
        }
    }
}