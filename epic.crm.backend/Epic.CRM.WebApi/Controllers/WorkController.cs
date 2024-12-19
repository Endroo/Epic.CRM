using Epic.CRM.BusinessLogic.Helpers;
using Epic.CRM.Common;
using Epic.CRM.DataDomain.Dtos;

using Microsoft.AspNetCore.Mvc;

using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Epic.CRM.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkController : ControllerBase
    {
        //[HttpGet]
        //[ProducesResponseType(typeof(PageResult<IEnumerable<AppUserDto>>), 200)]
        //public async Task<IActionResult> Get([FromQuery] QueryParams queryParams)
        //{
        //    var identityUserId = Request.HttpContext.User?.FindFirstValue(ClaimTypes.NameIdentifier);
        //    if (string.IsNullOrWhiteSpace(identityUserId))
        //        return Unauthorized("No logged user");

        //    var result = await _customerManager.GetAll(identityUserId, queryParams);

        //    return Ok(result);
        //}
    }
}
