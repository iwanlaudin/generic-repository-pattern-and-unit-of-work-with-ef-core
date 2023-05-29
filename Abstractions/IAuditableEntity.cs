
namespace GenericRepositoryPattern.Abstractions
{
    public interface IAuditableEntity
    {
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}