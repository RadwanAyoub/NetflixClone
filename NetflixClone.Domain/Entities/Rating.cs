using NetflixClone.Domain.Common;

namespace NetflixClone.Domain.Entities
{
    public class Rating : EntityBase
    {
        public int UserProfileId { get; set; }
        public int ContentId { get; set; }
        public int Score { get; set; } // 1-5 stars
        public string? Comment { get; set; }
        
        // Navigation properties
        public virtual UserProfile UserProfile { get; set; } = null!;
        public virtual Content Content { get; set; } = null!;
        
        public Rating() { }
        
        public Rating(int userProfileId, int contentId, int score)
        {
            UserProfileId = userProfileId;
            ContentId = contentId;
            Score = score;
            CreatedAt = DateTime.UtcNow;
        }
    }
}