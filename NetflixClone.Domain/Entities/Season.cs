using NetflixClone.Domain.Common;

namespace NetflixClone.Domain.Entities
{
    public class Season : EntityBase
    {
        public int TVShowId { get; set; }
        public int SeasonNumber { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Overview { get; set; } = string.Empty;
        public string PosterUrl { get; set; } = string.Empty;
        public DateTime AirDate { get; set; }
        public int EpisodeCount { get; set; }
        
        // Navigation properties
        public virtual TVShow TVShow { get; set; } = null!;
        public virtual ICollection<Episode> Episodes { get; set; } = new List<Episode>();
        
        public Season() { }
        
        public Season(int tvShowId, int seasonNumber, string name)
        {
            TVShowId = tvShowId;
            SeasonNumber = seasonNumber;
            Name = name;
            CreatedAt = DateTime.UtcNow;
        }
    }
}