using Epic.CRM.BusinessLogic.Helpers;
using Epic.CRM.BusinessLogic.Interfaces;
using Epic.CRM.Common;
using Epic.CRM.DataDomain.Dtos;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Epic.CRM.WebApi.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin,User")]
    [ApiController]
    public class WorkController : ControllerBase
    {
        private readonly IWorkManager _workManager;

        public WorkController(IWorkManager workManager)
        {
            _workManager = workManager;
        }

        [HttpGet]
        [ProducesResponseType(typeof(PageResult<IEnumerable<WorkDto>>), 200)]
        public async Task<IActionResult> Get([FromQuery] QueryParams queryParams)
        {
            var identityUserId = Request.HttpContext.User?.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrWhiteSpace(identityUserId))
                return Unauthorized("No logged user");

            var result = await _workManager.GetAll(identityUserId, queryParams);

            return Ok(result);
        }

        [HttpPost("create")]
        [ProducesResponseType(typeof(Result), 200)]
        public async Task<IActionResult> Create([FromBody] WorkEditRegisterDto form)
        {
            if (form is null)
                return BadRequest();

            var identityUserId = Request.HttpContext.User?.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrWhiteSpace(identityUserId))
                return Unauthorized("No logged user");

            var result = await _workManager.CreateWork(identityUserId, form);

            if (result.ResultStatus == ResultStatusEnum.Fail)
                return BadRequest(result.Errors);

            return Ok(result);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(Result), 200)]
        public async Task<IActionResult> Put(int? id, [FromBody] WorkEditRegisterDto form)
        {
            if (form is null || id is null)
                return BadRequest();

            var result = await _workManager.EditWork(id.Value, form);

            if (result.ResultStatus == ResultStatusEnum.Fail)
                return BadRequest(result.Errors);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(Result), 200)]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null)
                return BadRequest();

            var result = await _workManager.DeleteWork(id.Value);

            if (result.ResultStatus == ResultStatusEnum.Fail)
                return BadRequest(result.Errors);

            return Ok(result);
        }
    }
}
