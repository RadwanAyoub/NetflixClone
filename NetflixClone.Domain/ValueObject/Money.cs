using NetflixClone.Domain.Common;

namespace NetflixClone.Domain.ValueObjects
{
    public class Money : ValueObject
    {
        public decimal Amount { get; }
        public string Currency { get; }

        public Money(decimal amount, string currency)
        {
            if (amount < 0)
                throw new ArgumentException("Money amount cannot be negative");
            
            if (string.IsNullOrWhiteSpace(currency))
                throw new ArgumentException("Currency cannot be empty");

            Amount = amount;
            Currency = currency.ToUpper();
        }

        public static Money operator +(Money left, Money right)
        {
            if (left.Currency != right.Currency)
                throw new InvalidOperationException("Cannot add money in different currencies");
            
            return new Money(left.Amount + right.Amount, left.Currency);
        }

        public static Money operator -(Money left, Money right)
        {
            if (left.Currency != right.Currency)
                throw new InvalidOperationException("Cannot subtract money in different currencies");
            
            return new Money(left.Amount - right.Amount, left.Currency);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Amount;
            yield return Currency;
        }

        public override string ToString()
        {
            return $"{Amount} {Currency}";
        }
    }
}