
namespace GenericRepositoryPattern.DTOs
{
    public class ArticleDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime PublishedDate { get; set; }
        public CategoryDto Category { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }

    public record ArticleRequest(
        string Title,
        string Content,
        bool isPublished,
        Guid CategoryId
    );
}