using NetflixClone.Domain.Common;
using NetflixClone.Domain.Entities;

namespace NetflixClone.Infrastructure.Data
{
    public class EntityBaseVerificationTests
    {
        [Fact]
        public void EntityBase_ShouldBeAccessible_FromInfrastructureProject()
        {
            // Arrange
            var movie = new Movie("Test Movie", "Test Description");

            // Act & Assert
            // These should all work if EntityBase is properly referenced
            var id = movie.Id;
            var createdAt = movie.CreatedAt;
            var updatedAt = movie.UpdatedAt;
            var isDeleted = movie.IsDeleted;

            Assert.IsType<Movie>(movie);
            Assert.IsAssignableFrom<EntityBase>(movie);
        }

        [Fact]
        public void AllEntities_ShouldInheritFromEntityBase()
        {
            // Arrange & Act & Assert
            Assert.True(typeof(Movie).IsSubclassOf(typeof(EntityBase)));
            Assert.True(typeof(TVShow).IsSubclassOf(typeof(EntityBase)));
            Assert.True(typeof(Genre).IsSubclassOf(typeof(EntityBase)));
            Assert.True(typeof(User).IsSubclassOf(typeof(EntityBase)));
            Assert.True(typeof(UserProfile).IsSubclassOf(typeof(EntityBase)));
            Assert.True(typeof(Rating).IsSubclassOf(typeof(EntityBase)));
            Assert.True(typeof(WatchlistItem).IsSubclassOf(typeof(EntityBase)));
            Assert.True(typeof(ContentGenre).IsSubclassOf(typeof(EntityBase)));
            Assert.True(typeof(Season).IsSubclassOf(typeof(EntityBase)));
            Assert.True(typeof(Episode).IsSubclassOf(typeof(EntityBase)));
        }
    }
}