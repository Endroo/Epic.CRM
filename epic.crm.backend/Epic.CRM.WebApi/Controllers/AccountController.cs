using Azure.Core;
using Epic.CRM.BusinessLogic.Helpers;
using Epic.CRM.BusinessLogic.Interfaces;
using Epic.CRM.Common;
using Epic.CRM.DataDomain.Dtos;
using Epic.CRM.DataDomain.Models;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using System.Security.Claims;

namespace Epic.CRM.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IAppUserManager _appUserManager;

        public AccountController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager, IAppUserManager appUserManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _appUserManager = appUserManager;
        }

        [HttpGet("getCurrentUser")]
        [ProducesResponseType(typeof(LoggedInUserDto), 200)]
        public async Task<IActionResult> GetLoggedUser()
        {
            var identityUserId = Request.HttpContext.User?.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrWhiteSpace(identityUserId))
                return Ok();

            var identityUserResult = await _appUserManager.GetByIdentityUserId(identityUserId);
            if (identityUserResult.Data is not null)
            {
                var user = identityUserResult.Data;
                return Ok(new LoggedInUserDto
                {
                    Name = user.Name,
                    Email = user.Email,
                    IsAdmin = user.IsAdmin
                });
            }
            return BadRequest(ErrorCodes.account_error_no_user_found);
        }



        [HttpPost("login")]
        [ProducesResponseType(typeof(DataResult<LoggedInUserDto>), 200)]
        public async Task<IActionResult> Login([FromBody] LoginDto form)
        {
            if (form.UserName is null || form.Password is null)
                return BadRequest(ErrorCodes.account_error_username_or_password_null);

            var result = new DataResult<LoggedInUserDto>();

            var loginResult = await _signInManager.PasswordSignInAsync(form.UserName, form.Password, isPersistent: true, lockoutOnFailure: false);

            if (!loginResult.Succeeded)
            {
                return BadRequest(ErrorCodes.account_error_wrong_username_or_password);
            }

            var identityUserId = Request.HttpContext.User?.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrWhiteSpace(identityUserId))
                return Unauthorized(ErrorCodes.account_error_no_logged_user);

            var identityUserResult = await _appUserManager.GetByIdentityUserId(identityUserId);
            if (identityUserResult.Data is not null)
            {
                var user = identityUserResult.Data;
                return Ok(new LoggedInUserDto
                { 
                    Name = user.Name,
                    Email = user.Email,
                    IsAdmin = user.IsAdmin
                });
            }
            else
            {
                return BadRequest(ErrorCodes.account_error_no_user_found);
            }
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            try
            {
                await _signInManager.SignOutAsync();
            }
            catch (Exception ex)
            {
                return BadRequest($"{ErrorCodes.common_error_internal_server_error}");
            }
            
            return Ok(true);
        }
    }
}
