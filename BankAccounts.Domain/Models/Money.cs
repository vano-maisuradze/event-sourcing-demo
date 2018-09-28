
using App.Core;
using EnsureThat;

namespace BankAccounts.Domain.Models
{
    public class Money : ValueObject
    {
        public decimal Amount { get; }
        public Currency Currency { get; }
        

        public Money(decimal amount, Currency currency)
        {
            Ensure.That(amount, nameof(amount)).IsGt(-1);

            Amount = amount;
            Currency = currency;
        }

        public Money Add(decimal amount)
        {
            Ensure.That(amount, nameof(amount)).IsGt(0);
            return new Money(Amount + amount, Currency);
        }

        public Money Add(Money money)
        {
            Ensure.That(money.Amount, nameof(money.Amount)).IsGt(0);
            return new Money(Amount + money.Amount, Currency);
        }

        public Money AddFee(decimal feePercent)
        {
            Ensure.That(feePercent, nameof(feePercent)).IsInRange(0, 100);
            return new Money(Amount + (Amount * feePercent) / 100.0M, Currency);
        }

        public override string ToString()
        {
            return $"{Amount} {Currency}";
        }
    }
}
