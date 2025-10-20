using NetflixClone.Domain.Common;
using NetflixClone.Domain.Enums;

namespace NetflixClone.Domain.Entities
{
    public class User : EntityBase
    {
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public UserRole Role { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime? LastLogin { get; set; }
        
        // Navigation properties
        public virtual ICollection<UserProfile> UserProfiles { get; set; } = new List<UserProfile>();
        
        public User() { }
        
        public User(string email, string firstName, string lastName, UserRole role = UserRole.Viewer)
        {
            Email = email;
            FirstName = firstName;
            LastName = lastName;
            Role = role;
            CreatedAt = DateTime.UtcNow;
            IsActive = true;
        }
    }
}