using FluentAssertions;
using NetflixClone.Domain.Entities;
using NetflixClone.Domain.Enums;

namespace NetflixClone.Domain.UnitTests.Entities
{
    public class ContentTests
    {
        [Fact]
        public void Constructor_WithValidParameters_ShouldCreateContent()
        {
            // Arrange & Act
            var content = new Content("The Matrix", "Sci-Fi movie", ContentType.Movie)
            {
                Overview = "A computer hacker learns about the true nature of reality",
                Duration = 136,
                Rating = 8.7m
            };

            // Assert
            content.Title.Should().Be("The Matrix");
            content.ContentType.Should().Be(ContentType.Movie);
            content.Duration.Should().Be(136);
            content.Rating.Should().Be(8.7m);
            content.CreatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
        }

        [Theory]
        [InlineData("")]
        public void Constructor_WithInvalidTitle_ShouldThrowException(string invalidTitle)
        {
            // Arrange & Act
            Action act = () => new Content(invalidTitle, "Description", ContentType.Movie);

            // Assert
            act.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void MarkAsTrending_ShouldSetIsTrendingToTrue()
        {
            // Arrange
            var content = new Content("Test Movie", "Description", ContentType.Movie);

            // Act
            content.IsTrending = true;

            // Assert
            content.IsTrending.Should().BeTrue();
        }
    }
}