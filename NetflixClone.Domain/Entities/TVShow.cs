using NetflixClone.Domain.Common;
using NetflixClone.Domain.Enums;

namespace NetflixClone.Domain.Entities
{
    public class TVShow : Content
    {
        public int NumberOfSeasons { get; set; }
        public int NumberOfEpisodes { get; set; }
        public bool IsOngoing { get; set; }
        public DateTime? FirstAirDate { get; set; }
        public DateTime? LastAirDate { get; set; }
        public string CreatedBy { get; set; } = string.Empty;
        public string Network { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public string Language { get; set; } = string.Empty;
        
        // TV Show-specific navigation properties
        public virtual ICollection<Season> Seasons { get; set; } = new List<Season>();
        public virtual ICollection<Episode> Episodes { get; set; } = new List<Episode>();
        
        public TVShow() : base()
        {
            ContentType = ContentType.TVShow;
        }
        
        public TVShow(string title, string description) 
            : base(title, description, ContentType.TVShow)
        {
        }
        
        // Business methods
        public void MarkAsOngoing()
        {
            IsOngoing = true;
            UpdatedAt = DateTime.UtcNow;
        }
        
        public void MarkAsCompleted()
        {
            IsOngoing = false;
            LastAirDate = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
        }
        
        public void UpdateEpisodeCount(int totalEpisodes)
        {
            if (totalEpisodes < 0)
                throw new ArgumentException("Episode count cannot be negative");
                
            NumberOfEpisodes = totalEpisodes;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}