using Epic.CRM.BusinessLogic.Helpers;
using Epic.CRM.BusinessLogic.Interfaces;
using Epic.CRM.BusinessLogic.Managers;
using Epic.CRM.Common;
using Epic.CRM.DataDomain.Dtos;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

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
        [ProducesResponseType(typeof(PageResult<IEnumerable<AppUserDto>>), 200)]
        public async Task<IActionResult> Get([FromQuery] QueryParams queryParams)
        {
            var result = await _customerManager.GetAll(queryParams);

            return Ok(result);
        }

        [HttpPost("register")]
        [ProducesResponseType(typeof(Result), 200)]
        public async Task<IActionResult> Register([FromBody] CustomerRegisterDto form)
        {
            if (form is null)
                return BadRequest();

            var result = await _customerManager.CreateCustomer(form);

            if (result.ResultStatus == ResultStatusEnum.Fail)
                return BadRequest(result.Errors);

            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int? id, [FromBody] CustomerRegisterDto form)
        {
            if (form is null || id is null)
                return BadRequest();

            var result = await _customerManager.EditCustomer(id.Value, form);

            if (result.ResultStatus == ResultStatusEnum.Fail)
                return BadRequest(result.Errors);

            return Ok(result);
        }

        [HttpDelete("{id}")]
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
