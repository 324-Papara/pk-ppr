using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Para.Base.Response;
using Para.Bussiness.Cqrs;
using Para.Schema;

namespace Para.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerAddressesController : ControllerBase
    {
        private readonly IMediator mediator;
        
        public CustomerAddressesController(IMediator mediator)
        {
            this.mediator = mediator;
        }


        [HttpGet]
        public async Task<ApiResponse<List<CustomerAddressResponse>>> Get()
        {
            var operation = new GetAllCustomerAddressQuery();
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpGet("{CustomerAddressId}")]
        public async Task<ApiResponse<CustomerAddressResponse>> Get([FromRoute]long CustomerAddressId)
        {
            var operation = new GetCustomerAddressByIdQuery(CustomerAddressId);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpPost]
        public async Task<ApiResponse<CustomerAddressResponse>> Post([FromBody] CustomerAddressRequest value)
        {
            var operation = new CreateCustomerAddressCommand(value);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpPut("{CustomerAddressId}")]
        public async Task<ApiResponse> Put(long CustomerAddressId, [FromBody] CustomerAddressRequest value)
        {
            var operation = new UpdateCustomerAddressCommand(CustomerAddressId,value);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpDelete("{CustomerAddressId}")]
        public async Task<ApiResponse> Delete(long CustomerAddressId)
        {
            var operation = new DeleteCustomerAddressCommand(CustomerAddressId);
            var result = await mediator.Send(operation);
            return result;
        }
    }
}