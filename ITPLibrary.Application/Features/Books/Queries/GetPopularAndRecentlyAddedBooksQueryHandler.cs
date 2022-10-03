using AutoMapper;
using ITPLibrary.Application.Contracts.Persistance;
using ITPLibrary.Application.Features.Books.ViewModels;
using MediatR;

namespace ITPLibrary.Application.Features.Books.Queries
{
    public class GetPopularAndRecentlyAddedBooksQueryHandler : IRequestHandler<GetPopularAndRecentlyAddedBooksQuery,
                                                                            IEnumerable<RecentlyAddedAndPopularBookVm>>
    {
        private readonly IMapper _mapper;
        private readonly IBookRepository _bookRepository;

        public GetPopularAndRecentlyAddedBooksQueryHandler(IMapper mapper, IBookRepository bookRepository)
        {
            _mapper = mapper;
            _bookRepository = bookRepository;   
        }

        public async Task<IEnumerable<RecentlyAddedAndPopularBookVm>> Handle(GetPopularAndRecentlyAddedBooksQuery request, CancellationToken cancellationToken)
        {
            var response = await _bookRepository.GetPopularAndRecentlyAddedBooks();
            var mappedList = _mapper.Map<IEnumerable<RecentlyAddedAndPopularBookVm>>(response);
            
            return mappedList;
        }
    }
}
