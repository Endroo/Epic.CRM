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
        private readonly UserManager<Felhasznalo> _userManager;

        public UserController(UserManager<Felhasznalo> userManager)
        {
            _userManager = userManager;
        }


        // GET: api/<UserController>
        [HttpGet]
        
        [ProducesResponseType(typeof(IEnumerable<FelhasznaloDto>), 200)]
        public async Task<IActionResult> Get()
        {
            return Ok(await _userManager.Users.Select(u => new FelhasznaloDto().Map(u)).ToListAsync());
        }

        // GET: api/<UserController>
        [HttpGet ("id: {id}")]
        [ProducesResponseType(typeof(IEnumerable<FelhasznaloDto>), 200)]
        public async Task<IActionResult> Get(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user is null)
                return BadRequest("No user is found");

            return Ok(new FelhasznaloDto().Map(user));
        }

        [HttpGet("username: {userName}")]
        [ProducesResponseType(typeof(IEnumerable<FelhasznaloDto>), 200)]
        public async Task<IActionResult> GetByUserName(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (user is null)
                return BadRequest("No user is found");

            return Ok(new FelhasznaloDto().Map(user));
        }

        [HttpGet("login_user")]
        [ProducesResponseType(typeof(FelhasznaloDto), 200)]
        public async Task<IActionResult> GetLoginUser()
        {
            var userPrincipal = Request.HttpContext.User;
            var user = await _userManager.GetUserAsync(userPrincipal);

            if (user is null)
                return BadRequest("No user is found");

            return Ok(new FelhasznaloDto().Map(user));
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] FelhasznaloRegisterDto form)
        {
            if (form is null)
                return BadRequest();

            Felhasznalo felhasznalo = form.Map();

            var result = await _userManager.CreateAsync(felhasznalo, form.Password);

            if(!result.Succeeded)
            {
                var errors = result.Errors.Select(x => x.Description);

                return BadRequest(errors);
            }

            result = await _userManager.AddToRoleAsync(felhasznalo, form.IsAdmin ? "Admin" : "User");

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(x => x.Description);

                return BadRequest(errors);
            }

            return Ok();
        }

        
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody] FelhasznaloEditDto form)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user is null)
                return BadRequest("No user is found");

            user.Nev = form.Nev;
            user.IsAdmin = form.IsAdmin;
            user.Tevekenyseg = form.Tevekenyseg;

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(x => x.Description);

                return BadRequest(errors);
            }

            return Ok();
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user is null)
                return BadRequest("No user is found");

            var result = await _userManager.DeleteAsync(user);
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(x => x.Description);

                return BadRequest(errors);
            }

            return Ok();
        }
    }
}
