using Microsoft.AspNetCore.Mvc;
using Para.Base.Response;

namespace Para.Api.Controllers
{
    [NonController]
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private List<Book> list;

        public BooksController()
        {
            list = new List<Book>();
            list.Add(new Book() { Id = 1, Name = "Test1", Author = "Author1", PageCount = 993 });
            list.Add(new Book() { Id = 2, Name = "Test2", Author = "Author2", PageCount = 234 });
        }

        [HttpGet]
        public ApiResponse<List<Book>> Get()
        {
            return new ApiResponse<List<Book>>(list);
        }

        [HttpGet("{id}")]
        public ApiResponse<Book> Get(int id)
        {
            var item = list?.FirstOrDefault(x => x.Id == id);
            if (item is null)
            {
                return new ApiResponse<Book>("Item not found in system.");
            }

            return new ApiResponse<Book>(item);
        }

        [HttpPost]
        public ApiResponse Post([FromBody] Book value)
        {
            list.Add(value);
            return new ApiResponse();
        }

        [HttpPut("{id}")]
        public ApiResponse Put(int id, [FromBody] Book value)
        {
            var item = list.FirstOrDefault(x => x.Id == id);
            if (item is null)
            {
                return new ApiResponse("Item not found in system.");
            }

            list.Remove(item);
            list.Add(value);
            return new ApiResponse();
        }

        [HttpDelete("{id}")]
        public ApiResponse Delete(int id)
        {
            var item = list.FirstOrDefault(x => x.Id == id);
            if (item is null)
            {
                return new ApiResponse("Item not found in system.");
            }

            list.Remove(item);
            return new ApiResponse();
        }
    }
}