using EmployeManagementSystemWidthMongoDb.Api.EmployeeModels;
using EmployeManagementSystemWidthMongoDb.Api.Models;
using EmployeManagementSystemWidthMongoDb.Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;

namespace EmployeManagementSystemWidthMongoDb.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError,Type=typeof(ApiResponse<string>))]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeServices _employeeService;

        public EmployeeController(IEmployeeServices employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created,Type=typeof(ApiResponse<Employee>))]
        [SwaggerOperation(nameof(CreateEmployee),OperationId =nameof(CreateEmployee))]
        public async Task<IActionResult> CreateEmployee([FromBody]CreateEmployeeRequest request)
        {
            var res = await _employeeService.CreateEmployeeAsync(request);
            return StatusCode(int.Parse(res.Code!), res);
        }

        [HttpGet,Route("{id}")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ApiResponse<Employee>))]
        [SwaggerOperation(nameof(GetEmployee), OperationId = nameof(GetEmployee))]
        public async Task<IActionResult> GetEmployee([FromRoute] string id)
        {
            var res = await _employeeService.GetEmployeeAsync(id);
            return StatusCode(int.Parse(res.Code!), res);
        }

        [HttpDelete, Route("{id}")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ApiResponse<Employee>))]
        [SwaggerOperation(nameof(DeleteEmployee), OperationId = nameof(DeleteEmployee))]
        public async Task<IActionResult> DeleteEmployee([FromRoute] string id)
        {
            var res = await _employeeService.DeleteEmployeeAsync(id);
            return StatusCode(int.Parse(res.Code!), res);
        }

        [HttpPatch, Route("{id}")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ApiResponse<Employee>))]
        [SwaggerOperation(nameof(UpdateEmployee), OperationId = nameof(UpdateEmployee))]
        public async Task<IActionResult> UpdateEmployee([FromRoute] string id, [FromBody]CreateEmployeeRequest request)
        {
            var res = await _employeeService.UpdateEmployeeAsync(id,request);
            return StatusCode(int.Parse(res.Code!), res);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ApiResponse<PagedResults<Employee>>))]
        [SwaggerOperation(nameof(GetEmployees), OperationId = nameof(GetEmployees))]
        public async Task<IActionResult> GetEmployees([FromQuery] BaseFilter filter)
        {
            var res = await _employeeService.GetEmployeePagedResultsAsync(filter);
            return StatusCode(int.Parse(res.Code!), res);
        }
    }
}
