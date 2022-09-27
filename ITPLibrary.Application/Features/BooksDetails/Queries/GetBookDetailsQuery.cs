using ITPLibrary.Application.Features.BooksDetails.ViewModels;
using MediatR;

namespace ITPLibrary.Application.Features.BooksDetails.Queries
{
    public class GetBookDetailsQuery:IRequest<BookDetailsVm>
    {
        public int Id { get; set; }
    }
}
