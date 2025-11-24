using NetflixClone.Domain.Enums;

namespace NetflixClone.Application.DTOs
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public UserRole Role { get; set; }
        public bool IsActive { get; set; }
        public DateTime? LastLogin { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<UserProfileDto> Profiles { get; set; } = new List<UserProfileDto>();
        
        // Computed properties
        public string FullName => $"{FirstName} {LastName}";
        public bool IsAdmin => Role == UserRole.Admin;
    }
}