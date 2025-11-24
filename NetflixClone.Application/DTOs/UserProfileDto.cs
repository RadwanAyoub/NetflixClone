namespace NetflixClone.Application.DTOs
{
    public class UserProfileDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string ProfileName { get; set; } = string.Empty;
        public string AvatarUrl { get; set; } = string.Empty;
        public bool IsKidsProfile { get; set; }
        public string Language { get; set; } = "en";
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        
        // Computed properties
        public string DisplayName => $"{ProfileName} {(IsKidsProfile ? "👶" : "")}";
    }
}