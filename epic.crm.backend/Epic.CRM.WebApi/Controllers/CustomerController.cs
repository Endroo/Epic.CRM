using Epic.CRM.BusinessLogic.Helpers;
using Epic.CRM.BusinessLogic.Interfaces;
using Epic.CRM.BusinessLogic.Managers;
using Epic.CRM.Common;
using Epic.CRM.DataDomain.Dtos;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Epic.CRM.WebApi.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin,User")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerManager _customerManager;

        public CustomerController(ICustomerManager customerManager)
        {
            _customerManager = customerManager;
        }

        [HttpGet]
        [ProducesResponseType(typeof(PageResult<IEnumerable<CustomerDto>>), 200)]
        public async Task<IActionResult> Get([FromQuery] QueryParams queryParams)
        {
            var identityUserId = Request.HttpContext.User?.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrWhiteSpace(identityUserId))
                return Unauthorized(ErrorCodes.account_error_no_logged_user);

            var result = await _customerManager.GetAll(identityUserId, queryParams);

            return Ok(result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(DataResult<CustomerDto>), 200)]
        public async Task<IActionResult> GetById(int? id)
        {
            var result = await _customerManager.GetById(id.Value);
            return Ok(result);
        }

        [HttpPost("create")]
        [ProducesResponseType(typeof(Result), 200)]
        public async Task<IActionResult> Create([FromBody] CustomerEditRegisterDto form)
        {
            if (form is null)
                return BadRequest();

            var identityUserId = Request.HttpContext.User?.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrWhiteSpace(identityUserId))
                return Unauthorized(ErrorCodes.account_error_no_logged_user);

            var result = await _customerManager.CreateCustomer(identityUserId, form);

            if (result.ResultStatus == ResultStatusEnum.Fail)
                return BadRequest(result.Errors);

            return Ok(result);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(Result), 200)]
        public async Task<IActionResult> Put(int? id, [FromBody] CustomerEditRegisterDto form)
        {
            if (form is null || id is null)
                return BadRequest();

            var result = await _customerManager.EditCustomer(id.Value, form);

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

            var result = await _customerManager.DeleteCustomer(id.Value);
         

            if (result.ResultStatus == ResultStatusEnum.Fail)
                return BadRequest(result.Errors);

            return Ok(result);
        }
    }
}
