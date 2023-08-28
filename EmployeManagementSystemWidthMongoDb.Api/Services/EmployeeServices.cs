using EmployeManagementSystemWidthMongoDb.Api.EmployeeModels;
using EmployeManagementSystemWidthMongoDb.Api.Extensions;
using EmployeManagementSystemWidthMongoDb.Api.Models;
using EmployeManagementSystemWidthMongoDb.Api.Repositories;
using Mapster;
using Newtonsoft.Json;

namespace EmployeManagementSystemWidthMongoDb.Api.Services
{
    public class EmployeeServices : IEmployeeServices
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ILogger<EmployeeServices> _logger;
        public EmployeeServices(IEmployeeRepository employeeRepository, ILogger<EmployeeServices> logger)
        {
            _employeeRepository = employeeRepository;
            _logger = logger;
        }

        public  async Task<ApiResponse<Employee>> CreateEmployeeAsync(CreateEmployeeRequest request)
        {
            try
            {
                _logger.LogDebug("creating employee,request={request}", JsonConvert.SerializeObject(request));
                var employee=request.Adapt<Employee>();
                var res = await _employeeRepository.AddAsync(employee);
                if (res == null)
                {
                    return new ApiResponse<Employee>
                    {
                        Code = StatusCodes.Status424FailedDependency.ToString(),
                        Message = "an error occurred while creating employee"
                    };
                }
                return new ApiResponse<Employee>
                {
                    Message = "employee added successfully",
                    Code = StatusCodes.Status201Created.ToString(),
                    Data = employee
                };
            }
            catch (Exception e)
            {

                _logger.LogError(e, "an error occurred while creating employee,request={request}", JsonConvert.SerializeObject(request));
                return new ApiResponse<Employee>
                {
                    Message = "sorry,something went wrong",
                    Code = StatusCodes.Status500InternalServerError.ToString()
                };
            }
        }

        public async Task<ApiResponse<Employee>> DeleteEmployeeAsync(string id)
        {
            try
            {
                _logger.LogDebug("deleting employee={id}", id);
                var res = await _employeeRepository.DeleteAsync(id);
                if (res)
                {
                    return new ApiResponse<Employee>
                    {
                        Code = StatusCodes.Status200OK.ToString(),
                        Message = "Employee Deleted Successfully",
                    };
                }
                return new ApiResponse<Employee>
                {
                    Message = "an error occurred while deleting employee",
                    Code = StatusCodes.Status424FailedDependency.ToString(),
                };
            }
            catch (Exception e)
            {

                _logger.LogDebug(e, "an error occurred while deleting employee,id={id}", id);
                return new ApiResponse<Employee>
                {
                    Message = "sorry,an error occurred while deleting employee",
                    Code = StatusCodes.Status500InternalServerError.ToString(),
                };
            }
        }

        public async Task<ApiResponse<Employee>> GetEmployeeAsync(string id)
        {
            try
            {
                _logger.LogDebug("getting employee by id={id}", id);
                var emp=await _employeeRepository.GetByIdAsync(id);
                if (emp == null)
                {
                    return new ApiResponse<Employee>
                    {
                        Message = "Employee Not Found",
                        Code = StatusCodes.Status404NotFound.ToString(),
                    };
                }
                return new ApiResponse<Employee>
                {
                    Message = "Found",
                    Code = StatusCodes.Status200OK.ToString(),
                    Data = emp
                };
            }
            catch (Exception e)
            {

                _logger.LogError(e, "an error occurred while getting employee by id, id={id}", id);
                return new ApiResponse<Employee>
                {
                    Message = "sorry,something went wrong",
                    Code = StatusCodes.Status500InternalServerError.ToString()
                };
            }
        }

        public async Task<ApiResponse<PagedResults<Employee>>> GetEmployeePagedResultsAsync(BaseFilter filter)
        {
            try
            {
                _logger.LogDebug("getting employees");
                var res = await _employeeRepository.GetEmployeesAsync();
                var data = res.ToPagedResults(filter.Page, filter.PageSize);
                return new ApiResponse<PagedResults<Employee>>
                {
                    Message = "Retrieved",
                    Code = StatusCodes.Status200OK.ToString(),
                    Data = data
                };
            }
            catch (Exception e)
            {

                _logger.LogError(e, "an error occurred while getting employees");
                return new ApiResponse<PagedResults<Employee>>
                {
                    Message = "sorry,something went wrong",
                    Code = StatusCodes.Status500InternalServerError.ToString(),
                };
            }
        }

        public async Task<ApiResponse<Employee>> UpdateEmployeeAsync(string id,CreateEmployeeRequest request)
        {
            try
            {
                _logger.LogDebug("updating employee details={details},id={id}",JsonConvert.SerializeObject(request),id);
                var emp=await _employeeRepository.GetByIdAsync(id);
                if (emp != null)
                {
                    var info = request.Adapt<Employee>();
                    info.Id = emp.Id;
                    info.CreatedAt = emp.CreatedAt;
                    var res=await _employeeRepository.UpdateAsync(info);
                    return new ApiResponse<Employee>
                    {
                        Code = StatusCodes.Status200OK.ToString(),
                        Message = "Employee updated successfully",
                        Data = res
                    };
                }
                return new ApiResponse<Employee>
                {
                    Message = "employee not found",
                    Code = StatusCodes.Status404NotFound.ToString(),
                };
            }
            catch (Exception e)
            {

                _logger.LogError(e, "an error occurred while updating employee details,id={id},request={request}", id, JsonConvert.SerializeObject(request));
                return new ApiResponse<Employee>
                {
                    Message = "sorry,something went wrong",
                    Code = StatusCodes.Status500InternalServerError.ToString(),
                };
            }
        }
    }
}
