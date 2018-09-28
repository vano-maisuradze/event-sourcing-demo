using App.Core;
using BankAccounts.Domain.Models;
using System;

namespace BankAccounts.Commands
{
    public class DebitMoney : ICommand
    {
        public Guid AccountId { get; set; }
        public decimal Amount { get; set; }
        public Currency Currency { get; set; }
    }
}
