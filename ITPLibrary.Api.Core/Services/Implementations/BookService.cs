using AutoMapper;
using ITPLibrary.Api.Core.Dtos;
using ITPLibrary.Api.Core.Services.Interfaces;
using ITPLibrary.Api.Data.Data.Data_Provider.Interfaces;
using ITPLibrary.Api.Data.Entities;
using ITPLibrary.Api.Data.Repositories.Interfaces;

namespace ITPLibrary.Api.Core.Services.Implementations
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _repository;
        private readonly IMapper _mapper;
        private readonly IBookDataProvider _dataProvider;

        public BookService(IBookRepository repository, IMapper mapper, IBookDataProvider dataProvider)
        {
            _repository = repository;
            _mapper = mapper;
            _dataProvider = dataProvider;
        }

        public async Task<IEnumerable<PromotedBookDto>> GetPromotedBooks()
        {
            var books = await _repository.GetPromotedBooks();
            return _mapper.Map<List<PromotedBookDto>>(books);
        }

        public async Task<IEnumerable<BookDto>> GetPopularBooks()
        {
            var books = await _dataProvider.GetPopularBooks();
            return _mapper.Map<List<BookDto>>(books);
        }

        public async Task<BookDto> GetBookById(int id)
        {
            var book = await _repository.GetBookById(id);
            return _mapper.Map<BookDto>(book);
        }

        public async Task<bool> PostBook(PostBookDto newBook)
        {
            await _repository.PostBook(_mapper.Map<Book>(newBook));
            return true;
        }

        public async Task<IEnumerable<BookDto>> GetAllBooks()
        {
            var books = await _repository.GetAllBooks();
            return _mapper.Map<List<BookDto>>(books);
        }

        public async Task<IEnumerable<RecentlyAddedAndPopularBookDto>> GetPopularAndRecentlyAddedBooks()
        {
            var books = await _repository.GetPopularAndRecentlyAddedBooks();
            return _mapper.Map<List<RecentlyAddedAndPopularBookDto>>(books);
        }

        public async Task<BookDetailsDto> GetBookDetails(int bookId)
        {
            var books = await _repository.GetBookDetails(bookId);
            return _mapper.Map<BookDetailsDto>(books);
        }
    }
}
