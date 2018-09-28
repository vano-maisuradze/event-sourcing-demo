using App.Core;
using BankAccounts.Domain.Models;
using System;

namespace BankAccounts.Commands
{
    public class CreateBankAccount : ICommand
    {
        public Guid UserId { get; set; }
        public string Iban { get; set; }
        public Currency Currency { get; set; }
    }
}
