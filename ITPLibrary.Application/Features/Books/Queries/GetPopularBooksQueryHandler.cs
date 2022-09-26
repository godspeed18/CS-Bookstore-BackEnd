using AutoMapper;
using ITPLibrary.Application.Contracts.Persistance;
using ITPLibrary.Application.Features.Books.ViewModels;
using MediatR;

namespace ITPLibrary.Application.Features.Books.Queries
{
    public class GetPopularBooksQueryHandler : IRequestHandler<GetPopularBooksQuery, IReadOnlyList<PopularBookVm>>
    {
        private readonly IMapper _mapper;
        private readonly IBookRepository _bookRepository;

        public GetPopularBooksQueryHandler(IMapper mapper, IBookRepository bookRepository)
        {
            _mapper = mapper;
            _bookRepository = bookRepository;
        }

        public async Task<IReadOnlyList<PopularBookVm>> Handle(GetPopularBooksQuery request, CancellationToken cancellationToken)
        {
            var response = await _bookRepository.GetPopularBooks();
            var result = _mapper.Map<IReadOnlyList<PopularBookVm>>(response);

            return result;
        }
    }
}
