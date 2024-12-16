using Epic.CRM.BusinessLogic.Helpers;
using Epic.CRM.BusinessLogic.Interfaces;
using Epic.CRM.Common;
using Epic.CRM.DataDomain.Dtos;
using Epic.CRM.DataDomain.Models;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Epic.CRM.WebApi.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    [ApiController]

    public class UserController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IAppUserManager _appUserManager;

        public UserController(UserManager<IdentityUser> userManager, IAppUserManager appUserManager)
        {
            _userManager = userManager;
            _appUserManager = appUserManager;
        }


        [HttpGet]
        [ProducesResponseType(typeof(PageResult<IEnumerable<AppUserDto>>), 200)]
        public async Task<IActionResult> Get([FromQuery] QueryParams queryParams)
        {
            var result = await _appUserManager.GetAll(queryParams);

            return Ok(result);
        }

        [HttpGet("id:{id}")]
        [ProducesResponseType(typeof(DataResult<AppUserDto>), 200)]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _appUserManager.GetById(id);

            return Ok(result);
        }

        [HttpGet("username:{userName}")]
        [ProducesResponseType(typeof(DataResult<AppUserDto>), 200)]
        public async Task<IActionResult> GetByUserName(string userName)
        {
            var result = await _appUserManager.GetByUserName(userName);

            return Ok(result);
        }

        [HttpGet("loggedin_user")]
        [ProducesResponseType(typeof(DataResult<AppUserDto>), 200)]
        public async Task<IActionResult> GetLoginUser()
        {
            var result = await _appUserManager.GetLoggedInUser();

            return Ok(result);
        }

        [HttpPost("register")]
        [ProducesResponseType(typeof(Result), 200)]
        public async Task<IActionResult> Register([FromBody] AppUserRegisterDto form)
        {
            if (form is null)
                return BadRequest();

            var result = await _appUserManager.CreateUser(form);

            if (result.ResultStatus == ResultStatusEnum.Fail)
                return BadRequest(result.Errors);

            return Ok(result);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int? id, [FromBody] AppUserEditDto form)
        {
            if (form is null || id is null)
                return BadRequest();

            var result = await _appUserManager.EditUser(id.Value, form);

            if (result.ResultStatus == ResultStatusEnum.Fail)
                return BadRequest(result.Errors);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null)
                return BadRequest();

            var result = await _appUserManager.DeleteUser(id.Value);

            if (result.ResultStatus == ResultStatusEnum.Fail)
                return BadRequest(result.Errors);

            return Ok(result);
        }
    }
}
