using ITPLibrary.Application.Features.Books.ViewModels;
using ITPLibrary.Domain.Entites;
using MediatR;

namespace ITPLibrary.Application.Features.Books.Commands
{
    public class PostBookCommand : IRequest<Book>
    {
        public BookPostVm Book { get; set; }
    }
}
