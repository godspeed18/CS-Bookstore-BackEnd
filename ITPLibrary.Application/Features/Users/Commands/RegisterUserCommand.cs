﻿using ITPLibrary.Application.Features.Users.ViewModels;
using ITPLibrary.Domain.Entites;
using MediatR;

namespace ITPLibrary.Application.Features.Users.Commands
{
    public interface RegisterUserCommand : IRequest<User>
    {
        public RegisterUserVm User { get; set; }
    }
}
