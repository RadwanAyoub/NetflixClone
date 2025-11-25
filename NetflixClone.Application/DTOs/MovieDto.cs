using NetflixClone.Domain.Enums;

namespace NetflixClone.Application.DTOs
{
    public class MovieDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Overview { get; set; } = string.Empty;
        public string PosterUrl { get; set; } = string.Empty;
        public string BackdropUrl { get; set; } = string.Empty;
        public string TrailerUrl { get; set; } = string.Empty;
        public string VideoUrl { get; set; } = string.Empty;
        public DateTime ReleaseDate { get; set; }
        public int Duration { get; set; }
        public decimal Rating { get; set; }
        public int VoteCount { get; set; }
        public ContentType ContentType { get; set; }
        public VideoQuality MaxQuality { get; set; }
        public bool IsFeatured { get; set; }
        public bool IsTrending { get; set; }
        
        // Movie-specific properties
        public string Director { get; set; } = string.Empty;
        public string Writers { get; set; } = string.Empty;
        public string Cast { get; set; } = string.Empty;
        public string ProductionCompany { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public string Language { get; set; } = string.Empty;
        public decimal Budget { get; set; }
        public decimal Revenue { get; set; }
        public bool HasSubtitles { get; set; }
        public bool HasDubbedAudio { get; set; }
        
        public List<string> Genres { get; set; } = new List<string>();
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        
        // Computed properties
        public bool IsRecentlyReleased => ReleaseDate >= DateTime.UtcNow.AddMonths(-6);
        public bool IsClassic => ReleaseDate <= DateTime.UtcNow.AddYears(-10);
        public string FormattedDuration => $"{Duration / 60}h {Duration % 60}m";

        // Additional computed properties specific to movies
        public decimal Profit => Revenue - Budget;
        public bool IsBlockbuster => Revenue > 100000000; // $100M
        public string BudgetFormatted => Budget.ToString("C0");
        public string RevenueFormatted => Revenue.ToString("C0");
    }
}