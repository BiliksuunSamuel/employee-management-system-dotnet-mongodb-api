using EmployeManagementSystemWidthMongoDb.Api.EmployeeModels;
using EmployeManagementSystemWidthMongoDb.Api.Models;

namespace EmployeManagementSystemWidthMongoDb.Api.Services
{
    public interface IEmployeeServices
    {
        Task<ApiResponse<Employee>> CreateEmployeeAsync(CreateEmployeeRequest request);
        Task<ApiResponse<Employee>> UpdateEmployeeAsync(string id,CreateEmployeeRequest request);
        Task<ApiResponse<Employee>> GetEmployeeAsync(string id);
        Task<ApiResponse<Employee>> DeleteEmployeeAsync(string id);
        Task<ApiResponse<PagedResults<Employee>>> GetEmployeePagedResultsAsync(BaseFilter filter);

    }
}
