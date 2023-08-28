using EmployeManagementSystemWidthMongoDb.Api.EmployeeModels;
using EmployeManagementSystemWidthMongoDb.Api.Options;
using Mapster;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace EmployeManagementSystemWidthMongoDb.Api.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {

        private readonly IMongoCollection<Employee> _employeeCollection;

        public EmployeeRepository(IOptions<MongoDbConfiguration> mongoConfiguration)
        {
            var mongoDbClient = new MongoClient(mongoConfiguration.Value.Url);
            var database = mongoDbClient.GetDatabase(mongoConfiguration.Value.Database);
            _employeeCollection = database.GetCollection<Employee>("Employees");
        }
        public async Task<Employee> AddAsync(Employee employee)
        {
            await _employeeCollection.InsertOneAsync(employee);
            return employee;
        }

        public async Task<bool> DeleteAsync(string id)
        {
           var res=await _employeeCollection.FindOneAndDeleteAsync(x=>x.Id!.Equals(id));
            return res != null;
        }

        public async Task<Employee> GetByIdAsync(string id)
        {
            var res = await _employeeCollection.Find(d => d.Id!.Equals(id)).FirstOrDefaultAsync();
            return res;
        }

        public async Task<IEnumerable<Employee>> GetEmployeesAsync()
        {
            var res=await _employeeCollection.Find(_=>true).ToListAsync();
            return res;
        }

        public async Task<Employee> UpdateAsync(Employee employee)
        {
            employee.UpdatedAt = DateTime.Now;
            await _employeeCollection.ReplaceOneAsync(x=>x.Id!.Equals(employee.Id),employee);
            return employee;
        }
    }
}
