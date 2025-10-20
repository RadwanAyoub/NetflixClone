using Moq;

namespace NetflixClone.Domain.UnitTests
{
    public abstract class TestBase
    {
        protected Mock<T> CreateMock<T>() where T : class
        {
            return new Mock<T>();
        }

        protected static DateTime CreateTestDateTime(int year = 2024, int month = 1, int day = 1)
        {
            return new DateTime(year, month, day, 0, 0, 0, DateTimeKind.Utc);
        }
    }
}