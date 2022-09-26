using AutoMapper;
using ITPLibrary.Application.Contracts.Persistance;
using ITPLibrary.Application.Features.Books.ViewModels;
using MediatR;

namespace ITPLibrary.Application.Features.Books.Queries
{
    public class GetPopularAndRecentlyAddedBooksQueryHandler : IRequestHandler<GetPopularAndRecentlyAddedBooksQuery,
                                                                            IReadOnlyList<RecentlyAddedAndPopularBookVm>>
    {
        private readonly IMapper _mapper;
        private readonly IBookRepository _bookRepository;

        public async Task<IReadOnlyList<RecentlyAddedAndPopularBookVm>> Handle(GetPopularAndRecentlyAddedBooksQuery request, CancellationToken cancellationToken)
        {
            var response = await _bookRepository.GetPopularAndRecentlyAddedBooks();
            var mappedList = _mapper.Map<IReadOnlyList<RecentlyAddedAndPopularBookVm>>(response);

            return mappedList;
        }
    }
}
