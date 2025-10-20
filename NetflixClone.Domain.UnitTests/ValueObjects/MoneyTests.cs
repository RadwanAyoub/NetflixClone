using FluentAssertions;
using NetflixClone.Domain.ValueObjects;

namespace NetflixClone.Domain.UnitTests.ValueObjects
{
    public class MoneyTests
    {
        [Fact]
        public void Create_WithValidAmountAndCurrency_ShouldSucceed()
        {
            // Arrange & Act
            var money = new Money(29.99m, "USD");

            // Assert
            money.Amount.Should().Be(29.99m);
            money.Currency.Should().Be("USD");
        }

        [Fact]
        public void Create_WithNegativeAmount_ShouldThrowException()
        {
            // Arrange & Act
            Action act = () => new Money(-10m, "USD");

            // Assert
            act.Should().Throw<ArgumentException>()
                .WithMessage("Money amount cannot be negative");
        }

        [Theory]
        [InlineData(10.50, 5.25, 15.75)]
        [InlineData(0, 5.25, 5.25)]
        public void Add_WithSameCurrency_ShouldReturnCorrectAmount(decimal amount1, decimal amount2, decimal expected)
        {
            // Arrange
            var money1 = new Money(amount1, "USD");
            var money2 = new Money(amount2, "USD");

            // Act
            var result = money1 + money2;

            // Assert
            result.Amount.Should().Be(expected);
            result.Currency.Should().Be("USD");
        }

        [Fact]
        public void Add_WithDifferentCurrencies_ShouldThrowException()
        {
            // Arrange
            var money1 = new Money(10m, "USD");
            var money2 = new Money(5m, "EUR");

            // Act
            Action act = () => _ = money1 + money2;

            // Assert
            act.Should().Throw<InvalidOperationException>()
                .WithMessage("Cannot add money in different currencies");
        }
    }
}