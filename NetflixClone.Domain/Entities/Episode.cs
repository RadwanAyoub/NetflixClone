using NetflixClone.Domain.Common;

namespace NetflixClone.Domain.Entities
{
    public class Episode : EntityBase
    {
        public int SeasonId { get; set; }
        public int EpisodeNumber { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Overview { get; set; } = string.Empty;
        public string VideoUrl { get; set; } = string.Empty;
        public int Duration { get; set; } // in minutes
        public DateTime AirDate { get; set; }
        
        // Navigation properties
        public virtual Season Season { get; set; } = null!;
        
        public Episode() { }
        
        public Episode(int seasonId, int episodeNumber, string title)
        {
            SeasonId = seasonId;
            EpisodeNumber = episodeNumber;
            Title = title;
            CreatedAt = DateTime.UtcNow;
        }
    }
}