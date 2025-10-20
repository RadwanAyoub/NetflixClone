using NetflixClone.Domain.Common;

namespace NetflixClone.Domain.Entities
{
    public class Genre : EntityBase
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        
        // Navigation properties
        public virtual ICollection<Content> Contents { get; set; } = new List<Content>();
        public virtual ICollection<ContentGenre> ContentGenres { get; set; } = new List<ContentGenre>();
        
        public Genre() { }
        
        public Genre(string name, string description)
        {
            Name = name;
            Description = description;
            CreatedAt = DateTime.UtcNow;
        }
    }
}