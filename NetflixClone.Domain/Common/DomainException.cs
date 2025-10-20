namespace NetflixClone.Domain.Common
{
    public abstract class DomainException : Exception
    {
        protected DomainException(string message) : base(message)
        {
        }
    }

    public class InvalidRatingException : DomainException
    {
        public InvalidRatingException(int rating) 
            : base($"Rating {rating} is invalid. Rating must be between 1 and 5.")
        {
        }
    }

    public class UserProfileNotFoundException : DomainException
    {
        public UserProfileNotFoundException(int profileId) 
            : base($"User profile with ID {profileId} was not found.")
        {
        }
    }

    public class ContentNotFoundException : DomainException
    {
        public ContentNotFoundException(int contentId) 
            : base($"Content with ID {contentId} was not found.")
        {
        }
    }
}