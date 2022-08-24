using ITPLibrary.Api.Data.Data;
using ITPLibrary.Api.Data.Entities;
using ITPLibrary.Api.Data.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Web.Mvc;

namespace ITPLibrary.Api.Data.Repositories.Implementations
{
    public class BookRepository : IBookRepository
    {
        private readonly ApplicationDbContext _db;

        public BookRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<ActionResult<IEnumerable<Book>>> GetAllBooks()
        {
            return await _db.Books.ToListAsync();
        }

        public async Task<ActionResult<Book>> GetBookById(int id)
        {
            return await _db.Books.FindAsync(id);
        }

        public async Task<ActionResult<IEnumerable<Book>>> GetPopularBooks()
        {
            return await _db.Books.ToListAsync();
        }
    }
}
