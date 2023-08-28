using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace EmployeManagementSystemWidthMongoDb.Api.EmployeeModels
{
    public class Employee:BaseEntity
    {
        
        public string? PhoneNumber { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
    }

    public class BaseEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
    }
}
