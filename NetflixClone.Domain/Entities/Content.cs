using NetflixClone.Domain.Common;
using NetflixClone.Domain.Enums;

namespace NetflixClone.Domain.Entities
{
    public abstract class Content : EntityBase
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Overview { get; set; } = string.Empty;
        public string PosterUrl { get; set; } = string.Empty;
        public string BackdropUrl { get; set; } = string.Empty;
        public string TrailerUrl { get; set; } = string.Empty;
        public string VideoUrl { get; set; } = string.Empty;
        public DateTime ReleaseDate { get; set; }
        public int Duration { get; set; } // in minutes
        public decimal Rating { get; set; }
        public int VoteCount { get; set; }
        public ContentType ContentType { get; set; }
        public VideoQuality MaxQuality { get; set; }
        public bool IsFeatured { get; set; }
        public bool IsTrending { get; set; }
        public string ExternalId { get; set; } = string.Empty; // For Strapi/TMDB integration

        // Navigation properties
        public virtual ICollection<ContentGenre> ContentGenres { get; set; } = new List<ContentGenre>();
        public virtual ICollection<UserProfile> UserProfiles { get; set; } = new List<UserProfile>();
        public virtual ICollection<Rating> Ratings { get; set; } = new List<Rating>();

        public Content() { }

        public Content(string title, string description, ContentType contentType)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException($"{nameof(Content)}: Title cannot be null or empty.", nameof(title));

            Title = title;
            Description = description;
            ContentType = contentType;
            CreatedAt = DateTime.UtcNow;
            Rating = 0;
            VoteCount = 0;
        }
    }
}