
using System.ComponentModel.DataAnnotations.Schema;

namespace GenericRepositoryPattern.Entities
{
    public class Article : AuditableEntity
    {
        public Article()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime PublishedDate { get; set; }
        [ForeignKey(nameof(Category))]
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }
    }
}