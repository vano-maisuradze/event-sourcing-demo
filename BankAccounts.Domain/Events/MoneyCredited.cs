using App.Core;
using BankAccounts.Domain.Models;
using System;

namespace BankAccounts.Domain.Events
{
    public class MoneyCredited : DomainEvent
    {
        public decimal Amount { get; set; }
        public Currency Currency { get; set; }

        public MoneyCredited(Guid bankAccountId, decimal amount, Currency currency)
        {
            AggregateRootId = bankAccountId;
            Amount = amount;
            Currency = currency;
        }
    }
}
