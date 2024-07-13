using Microsoft.AspNetCore.Mvc;

namespace Para.Api.Controllers
{
    [NonController]
    [ApiController]
    [Route("api/[controller]")]
    public class BookDetailsController : ControllerBase
    {
        private List<Book> list;

        public BookDetailsController()
        {
            list = new List<Book>();
            list.Add(new Book() { Id = 1, Name = "Test1", Author = "Author1", PageCount = 993 });
            list.Add(new Book() { Id = 2, Name = "Test2", Author = "Author2", PageCount = 234 });
            list.Add(new Book() { Id = 3, Name = "Test3", Author = "Author3", PageCount = 963 });
            list.Add(new Book() { Id = 4, Name = "Test4", Author = "Author4", PageCount = 547 });
            list.Add(new Book() { Id = 5, Name = "Test5", Author = "Author5", PageCount = 134 });
        }

        [HttpGet("ByIdQuery")]
        public Book ByIdQuery([FromQuery] int id)
        {
            return list?.FirstOrDefault(x => x.Id == id);
        }

        [HttpGet("ByIdRoute/{id}")]
        public Book ByIdRoute([FromRoute] int id)
        {
            return list?.FirstOrDefault(x => x.Id == id);
        }

        [HttpGet("ByDetailQuery")]
        public string ByIdQuery([FromQuery] int? id,[FromQuery] string? name,[FromQuery] string? author,[FromQuery] int? pageCount)
        {
            return $"id:{id}-name:{name}-author:{author}-pageCount:{pageCount}";
        }

        [HttpGet("ByDetailRoute/{id}/{name}/{author}/{pageCount}")]
        public string ByIdRoute(int? id,string? name,string? author,int? pageCount)
        {
            return $"id:{id}-name:{name}-author:{author}-pageCount:{pageCount}";
        }
        
        [HttpGet("ByDetail/{id}")]
        public string ByDetail(int? id,[FromQuery] string? name,[FromQuery] string? author,[FromQuery] int? pageCount)
        {
            return $"id:{id}-name:{name}-author:{author}-pageCount:{pageCount}";
        }
        
    }
}

// http://localhost:5029/pa/api/BookDetails/ByIdRoute/1
// http://localhost:5029/pa/api/BookDetails/ByIdQuery?id=4

// http://localhost:5029/pa/api/BookDetails/ByDetailQuery?id=1&name=aaa&author=bbb&pageCount=55
// http://localhost:5029/pa/api/BookDetails/ByDetailRoute/1/aaa/bbb/55