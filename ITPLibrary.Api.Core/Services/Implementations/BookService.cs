using AutoMapper;
using ITPLibrary.Api.Core.Dtos;
using ITPLibrary.Api.Core.Services.Interfaces;
using ITPLibrary.Api.Data.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ITPLibrary.Api.Core.Services.Implementations
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _repository;
        private readonly IMapper _mapper;

        public BookService(IBookRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ActionResult<IEnumerable<BookDto>>> GetPopularBooks()
        {
            var books = await _repository.GetPopularBooks();
            return _mapper.Map<List<BookDto>>(books);
        }

        public async Task<ActionResult<BookDto>> GetBookById(int id)
        {
            var book = await _repository.GetBookById(id);
            return _mapper.Map<BookDto>(book);
        }

        public async Task<ActionResult<IEnumerable<BookDto>>> GetAllBooks()
        {
            var books = await _repository.GetAllBooks();
            return _mapper.Map<List<BookDto>>(books);
        }
    }
}
