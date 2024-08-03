using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Para.Base.Response;
using Para.IdentityApi.Schema;
using Para.IdentityApi.Service;

namespace Para.IdentityApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IPostService service;
        
        public PostsController(IPostService service)
        {
            this.service = service;
        }


        [HttpGet]
        [Authorize]
        public async Task<ApiResponse<List<PostResponse>>> Get()
        {
            var result = await service.GetAll();
            return result;
        }

        [HttpGet("{PostId}")]
        [Authorize]
        public async Task<ApiResponse<PostResponse>> Get([FromRoute]long PostId)
        {;
            var result = await service.GetById(PostId);
            return result;
        }
        
        [HttpPost]
        [Authorize]
        public async Task<ApiResponse<PostResponse>> Post([FromBody] PostRequest value)
        {
            var result = await service.Insert(value);
            return result;
        }

        [HttpPut("{PostId}")]
        [Authorize]
        public async Task<ApiResponse> Put(long PostId, [FromBody] PostRequest value)
        {
            var result = await service.Update(PostId,value);
            return result;
        }

        [HttpDelete("{PostId}")]
        [Authorize]
        public async Task<ApiResponse> Delete(long PostId)
        {
            var result = await service.Delete(PostId);
            return result;
        }
    }
}