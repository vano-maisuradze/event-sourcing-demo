
using App.Core;
using BankAccounts.Domain.Models;
using System;

namespace BankAccounts.Domain.Events
{
    public class BankAccountCreated : DomainEvent
    {
        public Guid UserId { get; set; }
        public Iban Iban { get; set; }
        public Currency Currency { get; set; }
        public decimal Balance { get; set; }
        public DateTimeOffset CreateDate { get; set; }

        public BankAccountCreated()
        {

        }

        public BankAccountCreated(Guid bankAccountId, Guid userId, Iban iban, Currency currency, decimal balance, DateTimeOffset createDate)
        {
            AggregateRootId = bankAccountId;
            UserId = userId;
            Iban = iban;
            Currency = currency;
            Balance = balance;
            CreateDate = createDate;
        }
    }
}
