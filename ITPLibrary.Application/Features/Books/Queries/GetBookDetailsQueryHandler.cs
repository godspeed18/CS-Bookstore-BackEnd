using AutoMapper;
using ITPLibrary.Application.Contracts.Persistance;
using ITPLibrary.Application.Features.Books.ViewModels;
using ITPLibrary.Application.Features.BooksDetails.ViewModels;
using ITPLibrary.Domain.Entites;
using MediatR;

namespace ITPLibrary.Application.Features.Books.Queries
{
    public class GetBookDetailsQueryHandler : IRequestHandler<GetBookDetailsQuery, BookWithDetailsVm>
    {
        private readonly IAsyncRepository<Book> _bookRepository;
        private readonly IMapper _mapper;

        public GetBookDetailsQueryHandler(IAsyncRepository<Book> bookRepository, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
        }

        public async Task<BookWithDetailsVm> Handle(GetBookDetailsQuery request, CancellationToken cancellationToken)
        {
            var response = await _bookRepository.GetByIdAsync(request.Id);
            var mappedBook = _mapper.Map<BookWithDetailsVm>(response);
            mappedBook.BookDetails = _mapper.Map<BookDetailsVm>(response.BookDetails);

            return mappedBook;
        }
    }
}
