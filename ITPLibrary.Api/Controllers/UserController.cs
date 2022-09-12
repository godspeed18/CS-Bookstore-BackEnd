using ITPLibrary.Api.Controllers.Method_Routes;
using ITPLibrary.Api.Core.Dtos;
using ITPLibrary.Api.Core.Services.Interfaces;
using ITPLibrary.Api.Data.Entities;
using ITPLibrary.Api.Data.Entities.ErrorMessages;
using ITPLibrary.Api.Data.Entities.RequestStatuses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ITPLibrary.Api.Controllers
{
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IUserLoginService _userLoginService;

        public UserController(IUserService userService, IUserLoginService userLoginService, IConfiguration configuration)
        {
            _userService = userService;
            _userLoginService = userLoginService;
        }

        [HttpPost(UserControllerRoutes.RecoverPassword)]
        [AllowAnonymous]
        public async Task<ActionResult> RecoverPassword(string email)
        {
            var user = await GetUser(email);

            if (user != null)
            {
                var response = _userService.SendEmail(user.HashedPassword, user.Email);
                if (response == false)
                {
                    return BadRequest(UserMessages.RecoveryEmailNotSent);
                }
                else
                {
                    return Ok(UserMessages.RecoveryEmailSent);
                }
            }
            else
            {
                return BadRequest(UserMessages.EmailNotValid);
            }
        }

        [HttpPost(UserControllerRoutes.LoginUser)]
        [AllowAnonymous]
        public async Task<ActionResult> Login(UserLoginDto loginUser)
        {
            var loginResponse = await _userLoginService.Login(loginUser);
            if (loginResponse == null)
            {
                return Unauthorized();
            }

            return Ok(loginResponse);
        }

        [HttpPost(UserControllerRoutes.RegisterUser)]
        [AllowAnonymous]
        public async Task<ActionResult> RegisterUser(UserRegisterDto newUser)
        {
            if (ModelState.IsValid)
            {
                var dataValidationResponse = await _userService.ValidateUserData(newUser);

                if (dataValidationResponse == UserRegisterStatus.Success)
                {
                    await _userService.RegisterUser(newUser);
                    return Ok();
                }
                else if (dataValidationResponse == UserRegisterStatus.EmailAlreadyRegistered)
                {
                    return Conflict(UserMessages.EmailAlreadyRegistered);
                }
                else
                {
                    return BadRequest();
                }
            }
            else
            {
                return BadRequest();
            }
        }

        private async Task<User> GetUser(UserLoginDto user)
        {
            return await _userService.GetUser(user);
        }

        private async Task<User> GetUser(string email)
        {
            return await _userService.GetUser(email);
        }
    }
}
