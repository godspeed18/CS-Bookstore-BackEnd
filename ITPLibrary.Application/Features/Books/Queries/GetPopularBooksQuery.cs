using ITPLibrary.Application.Features.Books.ViewModels;
using MediatR;

namespace ITPLibrary.Application.Features.Books.Queries
{
    public class GetPopularBooksQuery : IRequest<IReadOnlyList<PopularBookVm>>
    {
    }
}
