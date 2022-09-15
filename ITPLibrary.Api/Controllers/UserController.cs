using ITPLibrary.Api.Controllers.Method_Routes;
using ITPLibrary.Api.Core.Dtos;
using ITPLibrary.Api.Core.Services.Interfaces;
using ITPLibrary.Api.Data.Entities;
using ITPLibrary.Api.Data.Entities.ErrorMessages;
using ITPLibrary.Api.Data.Entities.RequestStatuses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace ITPLibrary.Api.Controllers
{
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IUserLoginService _userLoginService;
        private readonly IRecoveryCodeService _recoveryCodeService;

        public UserController(IUserService userService,
                                IUserLoginService userLoginService,
                                    IConfiguration configuration,
                                    IRecoveryCodeService recoveryCodeService)
        {
            _userService = userService;
            _userLoginService = userLoginService;
            _recoveryCodeService = recoveryCodeService;
        }

        [HttpPost(UserControllerRoutes.RequestRecoverPassword)]
        [AllowAnonymous]
        public async Task<ActionResult> RequestPasswordRecovery(string email)
        {
            var user = await GetUser(email);

            if (user != null)
            {
                string recoveryCode = _userService.GenerateRandomRecoveryCode();

                var userServiceResponse = await _userService.SendRecoveryEmail(user.Email, recoveryCode);
                var recoveryCodeServiceResponse = await _recoveryCodeService.PostRecoveryCode(recoveryCode, user.Email);

                if (userServiceResponse == false || recoveryCodeServiceResponse == false)
                {
                    return BadRequest(UserMessages.RecoveryEmailNotSent);
                }
            }

            return Ok(UserMessages.RecoveryEmailSent);
        }

        [HttpPost(UserControllerRoutes.ChangePassword)]
        [AllowAnonymous]
        public async Task<ActionResult> ChangePassword(ChangePasswordDto changePassword)
        {
            if (ModelState.IsValid != true)
            {
                return BadRequest();
            }

            var user = await _userService.GetUser(changePassword.Email);

            if (user != default)
            {
                if (await _recoveryCodeService.IsCodeValid(user.Id, changePassword.RecoveryCode))
                {
                    var response = await _userService.ChangePassword(user.Id, changePassword.Password);
                    await _recoveryCodeService.SetRecoveryCodeNotValid(changePassword.RecoveryCode);

                    if (response != true)
                    {
                        return BadRequest(UserMessages.UnknownError);
                    }
                }
                else
                {
                    return BadRequest(UserMessages.RecoveryCodeNotValid);
                }
            }

            return Ok(UserMessages.Success);
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

            return Ok(new JwtSecurityTokenHandler().
                            WriteToken(loginResponse.Token));
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

        private async Task<User> GetUser(string email)
        {
            return await _userService.GetUser(email);
        }
    }
}
