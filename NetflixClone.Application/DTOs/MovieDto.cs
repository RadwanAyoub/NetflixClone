namespace NetflixClone.Application.DTOs
{
    public class MovieDto : ContentDto
    {
        // Movie-specific properties
        public string VideoUrl { get; set; } = string.Empty; 
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
        
        // Computed properties
        public bool IsClassic => ReleaseDate <= DateTime.UtcNow.AddYears(-10);
        public decimal Profit => Revenue - Budget;
        public bool IsBlockbuster => Revenue > 100000000; // $100M
        public string BudgetFormatted => Budget.ToString("C0");
        public string RevenueFormatted => Revenue.ToString("C0");
    }
}