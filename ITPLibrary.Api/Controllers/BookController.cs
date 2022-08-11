using ITPLibrary.Api.Data.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ITPLibrary.Api.Controllers
{
    public class BookController : ControllerBase
    {
        public BookController()
        {

        }

        public IEnumerable<Book> GetPopularBooks()
        {
            return new List<Book> { new Book() { Id = 0, Name = "title1", Author = "author1", Price = 200 }, new Book() { Id = 1, Name = "title2", Author = "author2", Price = 250 }, new Book() { Id = 2, Name = "title3", Author = "author4", Price = 300 } };
        }
    }
}
