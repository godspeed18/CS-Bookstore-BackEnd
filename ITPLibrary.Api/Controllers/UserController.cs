using ITPLibrary.Api.Controllers.Method_Routes;
using ITPLibrary.Api.Data.Entities.ErrorMessages;
using ITPLibrary.Application.Features.RecoveryCodes.Commands;
using ITPLibrary.Application.Features.Users.Commands;
using ITPLibrary.Application.Features.Users.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace ITPLibrary.Api.Controllers
{
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost(UserControllerRoutes.RequestRecoverPassword)]
        [AllowAnonymous]
        public async Task<ActionResult> RequestPasswordRecovery(string email)
        {
            if (email == null)
            {
                return NotFound();
            }

            RequestPasswordRecoveryCommand passwordRecoveryCommand = new RequestPasswordRecoveryCommand();
            passwordRecoveryCommand.UserEmail = email;
            var response = await _mediator.Send(passwordRecoveryCommand);

            if (response == null)
            {
                return BadRequest(UserMessages.RecoveryEmailNotSent);
            }

            return Ok(UserMessages.RecoveryEmailSent);
        }

        [HttpPost(UserControllerRoutes.ChangePassword)]
        [AllowAnonymous]
        public async Task<ActionResult> ChangePassword(PasswordRecoveryVm changePassword)
        {
            if (ModelState.IsValid != true)
            {
                return BadRequest();
            }

            ChangePasswordCommand changePasswordCommand = new ChangePasswordCommand();
            changePasswordCommand.PasswordReset = changePassword;

            var response = await _mediator.Send(changePasswordCommand);
            if (response == null)
            {
                return NotFound();
            }

            return Ok(UserMessages.Success);
        }

        [HttpPost(UserControllerRoutes.LoginUser)]
        [AllowAnonymous]
        public async Task<ActionResult> Login(LoginUserVm loginUser)
        {
            LoginUserCommand loginUserCommand = new LoginUserCommand();
            loginUserCommand.UserLoginInfo = loginUser;

            var loginResponse = await _mediator.Send(loginUserCommand);
            if (loginResponse == null)
            {
                return Unauthorized();
            }

            return Ok(new JwtSecurityTokenHandler().
                            WriteToken(loginResponse.Token));
        }

        [HttpPost(UserControllerRoutes.RegisterUser)]
        [AllowAnonymous]
        public async Task<ActionResult> RegisterUser(RegisterUserVm newUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            RegisterUserCommand registerUserCommand = new RegisterUserCommand();
            registerUserCommand.User = newUser;

            var response = await _mediator.Send(registerUserCommand);
            return Ok(response);
        }
    }
}
