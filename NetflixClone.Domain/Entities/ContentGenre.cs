using NetflixClone.Domain.Common;

namespace NetflixClone.Domain.Entities
{
    public class ContentGenre : EntityBase
    {
        public int ContentId { get; set; }
        public int GenreId { get; set; }
        
        // Navigation properties
        public virtual Content Content { get; set; } = null!;
        public virtual Genre Genre { get; set; } = null!;
        
        public ContentGenre() { }
        
        public ContentGenre(int contentId, int genreId)
        {
            ContentId = contentId;
            GenreId = genreId;
            CreatedAt = DateTime.UtcNow;
        }
    }
}