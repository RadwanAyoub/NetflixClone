using System;
using NetflixClone.Domain.Common;

namespace NetflixClone.Domain.Entities
{
    public class Rating : EntityBase
    {
        public int UserProfileId { get; set; }
        public int ContentId { get; set; }
        private int _score;
        public int Score
        {
            get => _score;
            set
            {
                ValidateScore(value);
                _score = value;
            }
        } // 1-5 stars
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

        private static void ValidateScore(int score)
        {
            if (score is < 1 or > 5)
            {
                throw new ArgumentException("Rating score must be between 1 and 5");
            }
        }
    }
}