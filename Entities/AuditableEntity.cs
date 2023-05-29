
using GenericRepositoryPattern.Abstractions;

namespace GenericRepositoryPattern.Entities
{
    public class AuditableEntity : IAuditableEntity
    {
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}