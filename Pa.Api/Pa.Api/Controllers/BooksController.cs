using Microsoft.AspNetCore.Mvc;

namespace Pa.Api.Controllers
{
    public class Book
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public int PageCount { get; set; }
    }

    [Route("pa/api/[controller]")]
    [ApiController]
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
        public List<Book> Get()
        {
            return list;
        }

        [HttpGet("{id}")]
        public Book Get(int id)
        {
            return list?.FirstOrDefault(x => x.Id == id);
        }

        [HttpPost]
        public void Post([FromBody] Book value)
        {
            list.Add(value);
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Book value)
        {
            var item = list.FirstOrDefault(x => x.Id == id);
            list.Remove(item);
            list.Add(value);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var item = list.FirstOrDefault(x => x.Id == id);
            list.Remove(item);
        }
    }
}