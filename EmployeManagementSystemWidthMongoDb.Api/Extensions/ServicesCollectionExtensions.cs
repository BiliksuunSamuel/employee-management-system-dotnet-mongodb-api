using EmployeManagementSystemWidthMongoDb.Api.Repositories;
using EmployeManagementSystemWidthMongoDb.Api.Services;

namespace EmployeManagementSystemWidthMongoDb.Api.Extensions
{
    public static class ServicesCollectionExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            return services;
        }

        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IEmployeeServices,EmployeeServices>();

            return services;
        }
    }
}
