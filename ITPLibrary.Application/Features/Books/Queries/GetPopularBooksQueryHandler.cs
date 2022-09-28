using AutoMapper;
using ITPLibrary.Application.Contracts.Persistance;
using ITPLibrary.Application.Features.Books.ViewModels;
using ITPLibrary.Application.Features.BooksDetails.ViewModels;
using ITPLibrary.Domain.Entites;
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
            var mapper = CreateCustomMapper();

            var response = await _bookRepository.GetPopularBooks();
            var result = mapper.Map<IReadOnlyList<PopularBookVm>>(response);

            return result;
        }

        private IMapper CreateCustomMapper()
        {
            var mappingConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<BookDetails, BookDetailsVm>();
                cfg.CreateMap<Book, PopularBookVm>();
            }
            );
            mappingConfig.AssertConfigurationIsValid();
            var mapper = mappingConfig.CreateMapper();

            return mapper;
        }
    }
}
