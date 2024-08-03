using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Para.Base.Response;
using Para.IdentityApi.Schema;
using Para.IdentityApi.Service;

namespace Para.IdentityApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService service;
        
        public CategoriesController(ICategoryService service)
        {
            this.service = service;
        }


        [HttpGet]
        [Authorize]
        public async Task<ApiResponse<List<CategoryResponse>>> Get()
        {
            var result = await service.GetAll();
            return result;
        }

        [HttpGet("{CategoryId}")]
        [Authorize]
        public async Task<ApiResponse<CategoryResponse>> Get([FromRoute]long CategoryId)
        {;
            var result = await service.GetById(CategoryId);
            return result;
        }
        
        [HttpPost]
        [Authorize]
        public async Task<ApiResponse<CategoryResponse>> Post([FromBody] CategoryRequest value)
        {
            var result = await service.Insert(value);
            return result;
        }

        [HttpPut("{CategoryId}")]
        [Authorize]
        public async Task<ApiResponse> Put(long CategoryId, [FromBody] CategoryRequest value)
        {
            var result = await service.Update(CategoryId,value);
            return result;
        }

        [HttpDelete("{CategoryId}")]
        [Authorize]
        public async Task<ApiResponse> Delete(long CategoryId)
        {
            var result = await service.Delete(CategoryId);
            return result;
        }
    }
}