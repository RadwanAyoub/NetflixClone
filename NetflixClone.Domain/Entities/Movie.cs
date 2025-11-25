using NetflixClone.Domain.Common;
using NetflixClone.Domain.Enums;

namespace NetflixClone.Domain.Entities
{
    public class Movie : Content
    {
        public string Director { get; set; } = string.Empty;
        public string Writers { get; set; } = string.Empty; // Comma-separated list
        public string Cast { get; set; } = string.Empty; // Comma-separated list
        public string ProductionCompany { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public string Language { get; set; } = string.Empty;
        public decimal Budget { get; set; }
        public decimal Revenue { get; set; }
        public bool HasSubtitles { get; set; }
        public bool HasDubbedAudio { get; set; }
        
        // Movie-specific properties
        public int? SequelToId { get; set; } // Reference to previous movie
        public virtual Movie? SequelTo { get; set; }
        public virtual ICollection<Movie> Sequels { get; set; } = new List<Movie>();
        
        public Movie() : base()
        {
            ContentType = ContentType.Movie;
        }
        
        public Movie(string title, string description) 
            : base(title, description, ContentType.Movie)
        {
        }
        
        public Movie(string title, string description, string director, string language) 
            : this(title, description)
        {
            Director = director;
            Language = language;
        }
        
        // Business methods
        public void MarkAsTrending()
        {
            IsTrending = true;
            UpdatedAt = DateTime.UtcNow;
        }
        
        public void RemoveFromTrending()
        {
            IsTrending = false;
            UpdatedAt = DateTime.UtcNow;
        }
        
        public void UpdateRating(decimal newRating, int newVoteCount)
        {
            if (newRating < 0 || newRating > 10)
                throw new ArgumentException("Rating must be between 0 and 10");
                
            if (newVoteCount < 0)
                throw new ArgumentException("Vote count cannot be negative");
                
            Rating = newRating;
            VoteCount = newVoteCount;
            UpdatedAt = DateTime.UtcNow;
        }
        
        public void AddToFeatured()
        {
            IsFeatured = true;
            UpdatedAt = DateTime.UtcNow;
        }
        
        public void RemoveFromFeatured()
        {
            IsFeatured = false;
            UpdatedAt = DateTime.UtcNow;
        }
        
        public bool IsRecentlyReleased()
        {
            return ReleaseDate >= DateTime.UtcNow.AddMonths(-6);
        }
        
        public bool IsClassic()
        {
            return ReleaseDate <= DateTime.UtcNow.AddYears(-10);
        }
    }
}