
namespace GenericRepositoryPattern.Entities
{
    public class Category : AuditableEntity
    {
        public Category()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}