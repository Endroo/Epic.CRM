using Epic.CRM.BusinessLogic.Helpers;
using Epic.CRM.BusinessLogic.Interfaces;
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

        [HttpPost("login")]
        [ProducesResponseType(typeof(LoggedInUserDto), 200)]
        public async Task<IActionResult> Login([FromBody] LoginDto form)
        {
            if (form.UserName is null || form.Password is null)
                return BadRequest("User name or password can not be null");

            var result = await _signInManager.PasswordSignInAsync(form.UserName, form.Password, isPersistent: true, lockoutOnFailure: false);

            if (!result.Succeeded)
            {
                return Unauthorized(result.ToString());
            }

            var identityUserId = Request.HttpContext.User?.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrWhiteSpace(identityUserId))
                return Unauthorized("No logged user");

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
                return BadRequest("No user found");
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
                return BadRequest(ex);
            }
            
            return Ok();
        }
    }
}
