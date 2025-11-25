namespace NetflixClone.Application.DTOs
{
    public class RatingDto
    {
        public int Id { get; set; }
        public int UserProfileId { get; set; }
        public int ContentId { get; set; }
        public int Score { get; set; } // 1-5 stars
        public string? Comment { get; set; }
        public DateTime CreatedAt { get; set; }
        
        // Navigation properties (optional, for detailed views)
        public string? UserProfileName { get; set; }
        public string? ContentTitle { get; set; }
        
        // Computed properties
        public string StarRating => new string('⭐', Score);
        public bool HasComment => !string.IsNullOrEmpty(Comment);
    }
}