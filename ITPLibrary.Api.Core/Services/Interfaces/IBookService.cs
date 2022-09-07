using ITPLibrary.Api.Core.Dtos;

namespace ITPLibrary.Api.Core.Services.Interfaces
{
    public interface IBookService
    {
        public Task<BookDetailsDto> GetBookDetails(int bookId);
        public Task<IEnumerable<PromotedBookDto>> GetPromotedBooks();
        public Task<IEnumerable<BookDto>> GetPopularBooks();
        public Task<BookDto> GetBookById(int id);
        public Task<IEnumerable<BookDto>> GetAllBooks();
        public Task<bool> PostBook(PostBookDto newBook);
        public Task<IEnumerable<RecentlyAddedAndPopularBookDto>> GetPopularAndRecentlyAddedBooks();
    }
}
