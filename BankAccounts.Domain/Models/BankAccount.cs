using App.Core;
using BankAccounts.Domain.Events;
using System;

namespace BankAccounts.Domain.Models
{
    public class BankAccount : AggregateRoot
    {
        public Guid UserId { get; set; }
        public Iban Iban { get; set; }
        public Currency Currency { get; set; }
        public decimal Balance { get; set; }
        public DateTimeOffset CreateDate { get; set; }

        public BankAccount()
        {

        }

        public BankAccount(Guid userId, Iban iban, Currency currency)
        {
            Id = Guid.NewGuid();
            UserId = userId;
            Iban = iban;
            Currency = currency;
            Balance = 0.0M;
            CreateDate = DateTimeOffset.Now;

            HandleEvent(new BankAccountCreated(Id, userId, iban, currency, Balance, CreateDate));
        }

        public void CreditMoney(Money money)
        {
            if (Currency != money.Currency)
            {
                throw new DomainException("Currencies does not match.");
            }

            if (money.Amount <= 0)
            {
                throw new DomainException("Invalid amount.");
            }
            Balance += money.Amount;
            HandleEvent(new MoneyCredited(Id, money.Amount, money.Currency));
        }

        public void DebitMoney(Money money)
        {
            if (Currency != money.Currency)
            {
                throw new DomainException("Currencies does not match.");
            }
            if (Balance - money.Amount < 0)
            {
                throw new DomainException("Not enough available amount.");
            }
            Balance -= money.Amount;
            HandleEvent(new MoneyDebited(Id, money.Amount, money.Currency));
        }

        public void ApplyEvent(BankAccountCreated @event)
        {
            Id = @event.AggregateRootId;
            UserId = @event.UserId;
            Iban = @event.Iban;
            Currency = @event.Currency;
            Balance = @event.Balance;
            CreateDate = @event.CreateDate;
        }

        public void ApplyEvent(MoneyCredited @event)
        {
            Balance += @event.Amount;
        }

        public void ApplyEvent(MoneyDebited @event)
        {
            Balance -= @event.Amount;
        }
    }
}
