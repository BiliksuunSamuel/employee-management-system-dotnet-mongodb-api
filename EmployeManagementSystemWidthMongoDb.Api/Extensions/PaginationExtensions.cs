using EmployeManagementSystemWidthMongoDb.Api.Models;

namespace EmployeManagementSystemWidthMongoDb.Api.Extensions
{
    public static class PaginationExtensions
    {
        public static PagedResults<T> ToPagedResults<T>(this IEnumerable<T> data,int page,int pageSize)
        {
            var total=data.Count();
            var totalPages=total/pageSize+(total%pageSize==0?0:1);
            return new PagedResults<T>
            {
                Results = data.Skip(Math.Abs(page - 1) * pageSize).Take(pageSize),
                Page = page,
                TotalCount = total,
                TotalPages = totalPages,
                PageSize = pageSize
            };
        }
    }
}
