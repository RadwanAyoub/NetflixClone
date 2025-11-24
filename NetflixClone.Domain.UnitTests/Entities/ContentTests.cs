using FluentAssertions;
using NetflixClone.Domain.Entities;
using NetflixClone.Domain.Enums;

namespace NetflixClone.Domain.UnitTests.Entities
{
    public class ContentTests
    {
        [Fact]
        public void Movie_Constructor_WithValidParameters_ShouldCreateMovie()
        {
            // Arrange & Act
            var movie = new Movie("The Matrix", "Sci-Fi movie")
            {
                Overview = "A computer hacker learns about the true nature of reality",
                Duration = 136,
                Rating = 8.7m,
                Director = "Lana Wachowski, Lilly Wachowski",
                Language = "English"
            };

            // Assert
            movie.Title.Should().Be("The Matrix");
            movie.Description.Should().Be("Sci-Fi movie");
            movie.ContentType.Should().Be(ContentType.Movie);
            movie.Duration.Should().Be(136);
            movie.Rating.Should().Be(8.7m);
            movie.Director.Should().Be("Lana Wachowski, Lilly Wachowski");
            movie.Language.Should().Be("English");
            movie.CreatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void Movie_Constructor_WithInvalidTitle_ShouldThrowException(string invalidTitle)
        {
            // Arrange & Act
            Action act = () => new Movie(invalidTitle, "Description");

            // Assert
            act.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void Movie_MarkAsTrending_ShouldSetIsTrendingToTrue()
        {
            // Arrange
            var movie = new Movie("Test Movie", "Description");

            // Act
            movie.MarkAsTrending();

            // Assert
            movie.IsTrending.Should().BeTrue();
            movie.UpdatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
        }

        [Fact]
        public void Movie_RemoveFromTrending_ShouldSetIsTrendingToFalse()
        {
            // Arrange
            var movie = new Movie("Test Movie", "Description");
            movie.MarkAsTrending(); // First mark as trending

            // Act
            movie.RemoveFromTrending();

            // Assert
            movie.IsTrending.Should().BeFalse();
            movie.UpdatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
        }

        [Fact]
        public void Movie_UpdateRating_WithValidValues_ShouldUpdateRating()
        {
            // Arrange
            var movie = new Movie("Test Movie", "Description")
            {
                Rating = 7.5m,
                VoteCount = 100
            };

            // Act
            movie.UpdateRating(8.2m, 150);

            // Assert
            movie.Rating.Should().Be(8.2m);
            movie.VoteCount.Should().Be(150);
            movie.UpdatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(11)]
        public void Movie_UpdateRating_WithInvalidRating_ShouldThrowException(decimal invalidRating)
        {
            // Arrange
            var movie = new Movie("Test Movie", "Description");

            // Act
            Action act = () => movie.UpdateRating(invalidRating, 100);

            // Assert
            act.Should().Throw<ArgumentException>()
                .WithMessage("Rating must be between 0 and 10");
        }

        [Fact]
        public void Movie_UpdateRating_WithNegativeVoteCount_ShouldThrowException()
        {
            // Arrange
            var movie = new Movie("Test Movie", "Description");

            // Act
            Action act = () => movie.UpdateRating(8.0m, -1);

            // Assert
            act.Should().Throw<ArgumentException>()
                .WithMessage("Vote count cannot be negative");
        }

        [Fact]
        public void Movie_IsRecentlyReleased_WithRecentMovie_ShouldReturnTrue()
        {
            // Arrange
            var movie = new Movie("Recent Movie", "Description")
            {
                ReleaseDate = DateTime.UtcNow.AddMonths(-3) // 3 months ago
            };

            // Act & Assert
            movie.IsRecentlyReleased().Should().BeTrue();
        }

        [Fact]
        public void Movie_IsRecentlyReleased_WithOldMovie_ShouldReturnFalse()
        {
            // Arrange
            var movie = new Movie("Old Movie", "Description")
            {
                ReleaseDate = DateTime.UtcNow.AddMonths(-8) // 8 months ago
            };

            // Act & Assert
            movie.IsRecentlyReleased().Should().BeFalse();
        }

        [Fact]
        public void Movie_IsClassic_WithOldMovie_ShouldReturnTrue()
        {
            // Arrange
            var movie = new Movie("Classic Movie", "Description")
            {
                ReleaseDate = DateTime.UtcNow.AddYears(-15) // 15 years ago
            };

            // Act & Assert
            movie.IsClassic().Should().BeTrue();
        }

        [Fact]
        public void Movie_IsClassic_WithNewMovie_ShouldReturnFalse()
        {
            // Arrange
            var movie = new Movie("New Movie", "Description")
            {
                ReleaseDate = DateTime.UtcNow.AddYears(-5) // 5 years ago
            };

            // Act & Assert
            movie.IsClassic().Should().BeFalse();
        }

        [Fact]
        public void Movie_AddToFeatured_ShouldSetIsFeaturedToTrue()
        {
            // Arrange
            var movie = new Movie("Test Movie", "Description");

            // Act
            movie.AddToFeatured();

            // Assert
            movie.IsFeatured.Should().BeTrue();
            movie.UpdatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
        }

        [Fact]
        public void Movie_RemoveFromFeatured_ShouldSetIsFeaturedToFalse()
        {
            // Arrange
            var movie = new Movie("Test Movie", "Description");
            movie.AddToFeatured(); // First add to featured

            // Act
            movie.RemoveFromFeatured();

            // Assert
            movie.IsFeatured.Should().BeFalse();
            movie.UpdatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
        }
    }

    public class TVShowTests
    {
        [Fact]
        public void TVShow_Constructor_WithValidParameters_ShouldCreateTVShow()
        {
            // Arrange & Act
            var tvShow = new TVShow("Stranger Things", "Supernatural series")
            {
                NumberOfSeasons = 4,
                NumberOfEpisodes = 34,
                IsOngoing = true,
                FirstAirDate = new DateTime(2016, 7, 15)
            };

            // Assert
            tvShow.Title.Should().Be("Stranger Things");
            tvShow.ContentType.Should().Be(ContentType.TVShow);
            tvShow.NumberOfSeasons.Should().Be(4);
            tvShow.NumberOfEpisodes.Should().Be(34);
            tvShow.IsOngoing.Should().BeTrue();
            tvShow.FirstAirDate.Should().Be(new DateTime(2016, 7, 15));
        }

        [Fact]
        public void TVShow_MarkAsOngoing_ShouldSetIsOngoingToTrue()
        {
            // Arrange
            var tvShow = new TVShow("Test Show", "Description")
            {
                IsOngoing = false
            };

            // Act
            tvShow.MarkAsOngoing();

            // Assert
            tvShow.IsOngoing.Should().BeTrue();
            tvShow.UpdatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
        }

        [Fact]
        public void TVShow_MarkAsCompleted_ShouldSetIsOngoingToFalse()
        {
            // Arrange
            var tvShow = new TVShow("Test Show", "Description")
            {
                IsOngoing = true
            };

            // Act
            tvShow.MarkAsCompleted();

            // Assert
            tvShow.IsOngoing.Should().BeFalse();
            tvShow.LastAirDate.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
            tvShow.UpdatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
        }

        [Fact]
        public void TVShow_UpdateEpisodeCount_WithValidCount_ShouldUpdateEpisodeCount()
        {
            // Arrange
            var tvShow = new TVShow("Test Show", "Description")
            {
                NumberOfEpisodes = 10
            };

            // Act
            tvShow.UpdateEpisodeCount(15);

            // Assert
            tvShow.NumberOfEpisodes.Should().Be(15);
            tvShow.UpdatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
        }

        [Fact]
        public void TVShow_UpdateEpisodeCount_WithNegativeCount_ShouldThrowException()
        {
            // Arrange
            var tvShow = new TVShow("Test Show", "Description");

            // Act
            Action act = () => tvShow.UpdateEpisodeCount(-5);

            // Assert
            act.Should().Throw<ArgumentException>()
                .WithMessage("Episode count cannot be negative");
        }
    }

    public class RatingTests
    {
        [Fact]
        public void Rating_Constructor_WithValidParameters_ShouldCreateRating()
        {
            // Arrange & Act
            var rating = new Rating(1, 1, 4)
            {
                Comment = "Great movie!"
            };

            // Assert
            rating.UserProfileId.Should().Be(1);
            rating.ContentId.Should().Be(1);
            rating.Score.Should().Be(4);
            rating.Comment.Should().Be("Great movie!");
            rating.CreatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(6)]
        public void Rating_Constructor_WithInvalidScore_ShouldThrowException(int invalidScore)
        {
            // Arrange & Act
            Action act = () => new Rating(1, 1, invalidScore);

            // Assert
            act.Should().Throw<ArgumentException>()
                .WithMessage("Rating score must be between 1 and 5");
        }
    }

    public class GenreTests
    {
        [Fact]
        public void Genre_Constructor_WithValidParameters_ShouldCreateGenre()
        {
            // Arrange & Act
            var genre = new Genre("Science Fiction", "Futuristic and technology-based stories");

            // Assert
            genre.Name.Should().Be("Science Fiction");
            genre.Description.Should().Be("Futuristic and technology-based stories");
            genre.CreatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void Genre_Constructor_WithInvalidName_ShouldThrowException(string invalidName)
        {
            // Arrange & Act
            Action act = () => new Genre(invalidName, "Description");

            // Assert
            act.Should().Throw<ArgumentException>();
        }
    }

    public class UserTests
    {
        [Fact]
        public void User_Constructor_WithValidParameters_ShouldCreateUser()
        {
            // Arrange & Act
            var user = new User("john@example.com", "John", "Doe", UserRole.Viewer);

            // Assert
            user.Email.Should().Be("john@example.com");
            user.FirstName.Should().Be("John");
            user.LastName.Should().Be("Doe");
            user.Role.Should().Be(UserRole.Viewer);
            user.IsActive.Should().BeTrue();
            user.CreatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
        }

        [Fact]
        public void UserProfile_Constructor_WithValidParameters_ShouldCreateUserProfile()
        {
            // Arrange & Act
            var userProfile = new UserProfile(1, "Kids Profile", true);

            // Assert
            userProfile.UserId.Should().Be(1);
            userProfile.ProfileName.Should().Be("Kids Profile");
            userProfile.IsKidsProfile.Should().BeTrue();
            userProfile.IsActive.Should().BeTrue();
            userProfile.Language.Should().Be("en");
            userProfile.CreatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
        }
    }
}