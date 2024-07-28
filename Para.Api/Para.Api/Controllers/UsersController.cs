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
    public class UsersController : ControllerBase
    {
        private readonly IMediator mediator;
        
        public UsersController(IMediator mediator)
        {
            this.mediator = mediator;
        }


        [HttpGet]
        [Authorize(Roles = "admin")]
        public async Task<ApiResponse<List<UserResponse>>> Get()
        {
            var operation = new GetAllUserQuery();
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpGet("{UserId}")]
        [Authorize(Roles = "admin")]
        public async Task<ApiResponse<UserResponse>> Get([FromRoute]long UserId)
        {
            var operation = new GetUserByIdQuery(UserId);
            var result = await mediator.Send(operation);
            return result;
        }
        
        [HttpGet("ByCustomer")]
        [Authorize(Roles = "customer")]
        public async Task<ApiResponse<List<UserResponse>>> GetByCustomerId()
        {
            var operation = new GetUserByCustomerIdQuery();
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<ApiResponse<UserResponse>> Post([FromBody] UserRequest value)
        {
            var operation = new CreateUserCommand(value);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpPut("{UserId}")]
        [Authorize(Roles = "admin")]
        public async Task<ApiResponse> Put(long UserId, [FromBody] UserRequest value)
        {
            var operation = new UpdateUserCommand(UserId,value);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpDelete("{UserId}")]
        [Authorize(Roles = "admin")]
        public async Task<ApiResponse> Delete(long UserId)
        {
            var operation = new DeleteUserCommand(UserId);
            var result = await mediator.Send(operation);
            return result;
        }
    }
}