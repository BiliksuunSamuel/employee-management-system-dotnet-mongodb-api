using EmployeManagementSystemWidthMongoDb.Api.EmployeeModels;

namespace EmployeManagementSystemWidthMongoDb.Api.Repositories
{
    public interface IEmployeeRepository
    {
        public Task<Employee> AddAsync(Employee employee);
        public Task<bool> DeleteAsync(string id);
        public Task<Employee> GetByIdAsync(string id);
        public Task<IEnumerable<Employee>> GetEmployeesAsync();
        public Task<Employee>UpdateAsync(Employee employee);
    }
}
