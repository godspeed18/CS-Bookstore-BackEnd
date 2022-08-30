using ITPLibrary.Api.Controllers.Method_Routes;
using ITPLibrary.Api.Core.Dtos;
using ITPLibrary.Api.Core.Services.Interfaces;
using ITPLibrary.Api.Data.Entities;
using ITPLibrary.Api.Data.Entities.RequestStatuses;
using ITPLibrary.Api.Data.Entities.Validation_Rules.Validation_Regex;
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
                var response = _userService.SendEmail(user.Password, user.Email);
                if (response == false)
                {
                    return BadRequest("Oops... An error occured. The password recovery e-mail was not sent.");
                }
                else
                {
                    return Ok("E-mail was successfully sent");
                }
            }

            else
            {
                return BadRequest("E-mail is not valid");
            }
        }

        [HttpPost(UserControllerRoutes.LoginUser)]
        [AllowAnonymous]
        public async Task<ActionResult> Login(UserLoginDto loginUser)
        {
            var user = await GetUser(loginUser);
            if (user != null)
            {
                return Ok(_userLoginService.Login(user));
            }

            else
            {
                return BadRequest("Invalid login credentials");
            }
        }

        [HttpPost(UserControllerRoutes.RegisterUser)]
        [AllowAnonymous]
        public async Task<ActionResult> RegisterUser(UserRegisterDto newUser)
        {
            UserValidationRegex validation = new UserValidationRegex();

            var dataValidationResponse = _userService.IsPasswordValid(validation, newUser.Password);
            if (!dataValidationResponse)
                return BadRequest("Password is not strong enough.");

            dataValidationResponse = _userService.PasswordAndConfirmedPasswordMatch(newUser.Password, newUser.ConfirmedPassword);
            if (!dataValidationResponse)
                return BadRequest("Password and confirmed password do not match.");

            dataValidationResponse = _userService.IsEmailValid(validation, newUser.Email);
            if (!dataValidationResponse)
                return BadRequest("E-mail is not valid");

            dataValidationResponse = _userService.IsNameValid(validation, newUser.Name);
            if (!dataValidationResponse)
                return BadRequest("Name is not valid");

            var repositoryResponse = await _userService.RegisterUser(newUser);
            if (repositoryResponse == UserRegisterStatus.EmailAlreadyRegistered)
            {
                return Conflict("An account with this e-mail already exists.");
            }

            return Ok("Registration was successful.");
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
