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
    public class CustomerDetailsController : ControllerBase
    {
        private readonly IMediator mediator;
        
        public CustomerDetailsController(IMediator mediator)
        {
            this.mediator = mediator;
        }


        [HttpGet]
        [Authorize(Roles = "admin")]
        public async Task<ApiResponse<List<CustomerDetailResponse>>> Get()
        {
            var operation = new GetAllCustomerDetailQuery();
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpGet("{CustomerDetailId}")]
        [Authorize(Roles = "admin")]
        public async Task<ApiResponse<CustomerDetailResponse>> Get([FromRoute]long CustomerDetailId)
        {
            var operation = new GetCustomerDetailByIdQuery(CustomerDetailId);
            var result = await mediator.Send(operation);
            return result;
        }
        
        [HttpGet("ByCustomer")]
        [Authorize(Roles = "customer")]
        public async Task<ApiResponse<CustomerDetailResponse>> GetByCustomerId()
        {
            var operation = new GetCustomerDetailByCustomerIdQuery();
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<ApiResponse<CustomerDetailResponse>> Post([FromBody] CustomerDetailRequest value)
        {
            var operation = new CreateCustomerDetailCommand(value);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpPut("{CustomerDetailId}")]
        [Authorize(Roles = "admin")]
        public async Task<ApiResponse> Put(long CustomerDetailId, [FromBody] CustomerDetailRequest value)
        {
            var operation = new UpdateCustomerDetailCommand(CustomerDetailId,value);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpDelete("{CustomerDetailId}")]
        [Authorize(Roles = "admin")]
        public async Task<ApiResponse> Delete(long CustomerDetailId)
        {
            var operation = new DeleteCustomerDetailCommand(CustomerDetailId);
            var result = await mediator.Send(operation);
            return result;
        }
    }
}