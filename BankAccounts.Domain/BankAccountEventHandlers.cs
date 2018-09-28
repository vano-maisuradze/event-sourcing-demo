using App.Core;
using BankAccounts.Domain.Repositories;

namespace BankAccounts.Domain.Events
{
    public class BankAccountEventHandlers : 
        IEventHandler<BankAccountCreated>,
        IEventHandler<MoneyCredited>
    {
        private IRepository _repository;

        public BankAccountEventHandlers(IRepository repository)
        {
            _repository = repository;
        }

        public void Handle(BankAccountCreated domainEvent)
        {
            _repository.Append(domainEvent);
        }

        public void Handle(MoneyCredited domainEvent)
        {
            _repository.Append(domainEvent);
        }
    }
}
