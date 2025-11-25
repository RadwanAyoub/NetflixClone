using System;
using NetflixClone.Domain.Common;

namespace NetflixClone.Domain.Entities
{
    public class Genre : EntityBase
    {
        private string _name = string.Empty;
        public string Name
        {
            get => _name;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Genre name cannot be null or empty", nameof(value));
                }

                _name = value;
            }
        }
        public string Description { get; set; } = string.Empty;
        
        // Navigation properties
        public virtual ICollection<Content> Contents { get; set; } = new List<Content>();
        public virtual ICollection<ContentGenre> ContentGenres { get; set; } = new List<ContentGenre>();
        
        public Genre() { }
        
        public Genre(string name, string description)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Genre name cannot be null or empty", nameof(name));
            }

            Name = name;
            Description = description;
            CreatedAt = DateTime.UtcNow;
        }
    }
}