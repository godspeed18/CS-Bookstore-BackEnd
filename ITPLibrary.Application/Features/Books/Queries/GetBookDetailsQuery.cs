﻿using ITPLibrary.Application.Features.Books.ViewModels;
using MediatR;

namespace ITPLibrary.Application.Features.Books.Queries
{
    public class GetBookDetailsQuery : IRequest<BookDetailsVm>
    {
        public int Id { get; set; }
    }
}