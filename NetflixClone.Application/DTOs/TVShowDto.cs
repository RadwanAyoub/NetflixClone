using NetflixClone.Domain.Enums;

namespace NetflixClone.Application.DTOs
{
    public class TVShowDto : ContentDto
    {
        public int NumberOfSeasons { get; set; }
        public int NumberOfEpisodes { get; set; }
        public bool IsOngoing { get; set; }
        public DateTime? FirstAirDate { get; set; }
        public DateTime? LastAirDate { get; set; }
        public string CreatedBy { get; set; } = string.Empty;
        public string Network { get; set; } = string.Empty;
        
        // Additional computed properties for TV shows
        public string Status => IsOngoing ? "Ongoing" : "Completed";
        public bool HasNewEpisodes => IsOngoing && LastAirDate >= DateTime.UtcNow.AddMonths(-3);
    }
}