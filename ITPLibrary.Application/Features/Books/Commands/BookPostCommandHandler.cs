using AutoMapper;
using ITPLibrary.Application.Contracts.Persistance;
using ITPLibrary.Domain.Entites;
using MediatR;

namespace ITPLibrary.Application.Features.Books.Commands
{
    public class BookPostCommandHandler : IRequestHandler<PostBookCommand, Book>
    {
        private readonly IMapper _mapper;
        private readonly IAsyncRepository<Book> _bookRepository;
        public BookPostCommandHandler(IMapper mapper, IAsyncRepository<Book> bookRepository)
        {
            _mapper = mapper;
            _bookRepository = bookRepository;
        }

        public async Task<Book> Handle(PostBookCommand request, CancellationToken cancellationToken)
        {
            if(request.Book==null)
            {
                return null;
            }

            var book = _mapper.Map<Book>(request.Book);
            var response = await _bookRepository.AddAsync(book);
            await _bookRepository.SaveChangesAsync();
            
            return response;
        }
    }
}
