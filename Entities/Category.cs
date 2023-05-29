
namespace GenericRepositoryPattern.Entities
{
    public class Category : AuditableEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}