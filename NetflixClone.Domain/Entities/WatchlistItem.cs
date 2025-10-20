using NetflixClone.Domain.Common;

namespace NetflixClone.Domain.Entities
{
    public class WatchlistItem : EntityBase
    {
        public int UserProfileId { get; set; }
        public int ContentId { get; set; }
        public DateTime AddedAt { get; set; }
        
        // Navigation properties
        public virtual UserProfile UserProfile { get; set; } = null!;
        public virtual Content Content { get; set; } = null!;
        
        public WatchlistItem() { }
        
        public WatchlistItem(int userProfileId, int contentId)
        {
            UserProfileId = userProfileId;
            ContentId = contentId;
            AddedAt = DateTime.UtcNow;
            CreatedAt = DateTime.UtcNow;
        }
    }
}