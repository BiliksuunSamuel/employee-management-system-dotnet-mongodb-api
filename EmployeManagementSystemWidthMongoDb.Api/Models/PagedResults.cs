namespace EmployeManagementSystemWidthMongoDb.Api.Models
{
    public class PagedResults<T>
    {
        public int Page { get; set; }
        public int PageSize  { get; set; }
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }
        public IEnumerable<T> Results { get; set; }=new List<T>(); 
    }
}
