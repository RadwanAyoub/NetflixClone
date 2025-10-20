using NetflixClone.Domain.Common;

namespace NetflixClone.Domain.Entities
{
    public class UserProfile : EntityBase
    {
        public int UserId { get; set; }
        public string ProfileName { get; set; } = string.Empty;
        public string AvatarUrl { get; set; } = string.Empty;
        public bool IsKidsProfile { get; set; }
        public string Language { get; set; } = "en";
        public bool IsActive { get; set; } = true;
        
        // Navigation properties
        public virtual User User { get; set; } = null!;
        public virtual ICollection<Content> WatchHistory { get; set; } = new List<Content>();
        public virtual ICollection<WatchlistItem> Watchlist { get; set; } = new List<WatchlistItem>();
        
        public UserProfile() { }
        
        public UserProfile(int userId, string profileName, bool isKidsProfile = false)
        {
            UserId = userId;
            ProfileName = profileName;
            IsKidsProfile = isKidsProfile;
            CreatedAt = DateTime.UtcNow;
            IsActive = true;
        }
    }
}