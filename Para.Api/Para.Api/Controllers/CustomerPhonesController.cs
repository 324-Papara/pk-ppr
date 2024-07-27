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
    public class CustomerPhonesController : ControllerBase
    {
        private readonly IMediator mediator;
        
        public CustomerPhonesController(IMediator mediator)
        {
            this.mediator = mediator;
        }


        [HttpGet]
        public async Task<ApiResponse<List<CustomerPhoneResponse>>> Get()
        {
            var operation = new GetAllCustomerPhoneQuery();
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpGet("{CustomerPhoneId}")]
        public async Task<ApiResponse<CustomerPhoneResponse>> Get([FromRoute]long CustomerPhoneId)
        {
            var operation = new GetCustomerPhoneByIdQuery(CustomerPhoneId);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpPost]
        public async Task<ApiResponse<CustomerPhoneResponse>> Post([FromBody] CustomerPhoneRequest value)
        {
            var operation = new CreateCustomerPhoneCommand(value);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpPut("{CustomerPhoneId}")]
        public async Task<ApiResponse> Put(long CustomerPhoneId, [FromBody] CustomerPhoneRequest value)
        {
            var operation = new UpdateCustomerPhoneCommand(CustomerPhoneId,value);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpDelete("{CustomerPhoneId}")]
        public async Task<ApiResponse> Delete(long CustomerPhoneId)
        {
            var operation = new DeleteCustomerPhoneCommand(CustomerPhoneId);
            var result = await mediator.Send(operation);
            return result;
        }
    }
}