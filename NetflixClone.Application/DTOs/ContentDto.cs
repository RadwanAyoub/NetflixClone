using NetflixClone.Domain.Enums;

namespace NetflixClone.Application.DTOs
{
    public class ContentDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Overview { get; set; } = string.Empty;
        public string PosterUrl { get; set; } = string.Empty;
        public string BackdropUrl { get; set; } = string.Empty;
        public string TrailerUrl { get; set; } = string.Empty;
        public DateTime ReleaseDate { get; set; }
        public int Duration { get; set; }
        public decimal Rating { get; set; }
        public int VoteCount { get; set; }
        public ContentType ContentType { get; set; }
        public VideoQuality MaxQuality { get; set; }
        public bool IsFeatured { get; set; }
        public bool IsTrending { get; set; }
        public List<string> Genres { get; set; } = new List<string>();
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        
        // Computed properties
        public bool IsRecentlyReleased => ReleaseDate >= DateTime.UtcNow.AddMonths(-6);
        public string FormattedDuration => $"{Duration / 60}h {Duration % 60}m";
        public string ReleaseYear => ReleaseDate.Year.ToString();
    }
}