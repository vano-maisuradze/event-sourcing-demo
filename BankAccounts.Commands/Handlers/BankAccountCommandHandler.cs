using App.Core;
using BankAccounts.Domain.Models;
using BankAccounts.Domain;
using BankAccounts.Domain.Repositories;

namespace BankAccounts.Commands.Handlers
{
    public class BankAccountCommandHandler :
        ICommandHandler<CreateBankAccount>,
        ICommandHandler<CreditMoney>,
        ICommandHandler<DebitMoney>
    {
        private IRepository _repository;

        public BankAccountCommandHandler(IRepository repository)
        {
            _repository = repository;
        }

        public void Handle(CreateBankAccount command)
        {
            var bankAccount = new BankAccount(command.UserId, new Iban(command.Iban), command.Currency);
            _repository.Save(bankAccount);
        }

        public void Handle(CreditMoney command)
        {
            var bankAccount = _repository.GetById<BankAccount>(command.AccountId);
            bankAccount.CreditMoney(new Money(command.Amount, command.Currency));
            _repository.Save(bankAccount);
        }

        public void Handle(DebitMoney command)
        {
            var bankAccount = _repository.GetById<BankAccount>(command.AccountId);
            bankAccount.DebitMoney(new Money(command.Amount, command.Currency));
            _repository.Save(bankAccount);
        }

    }
}
