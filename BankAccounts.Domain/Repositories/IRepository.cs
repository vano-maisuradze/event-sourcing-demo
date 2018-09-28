
using App.Core;
using System;

namespace BankAccounts.Domain.Repositories
{
    public interface IRepository
    {
        void Save(AggregateRoot aggregateRoot);
        void Append(DomainEvent domainEvent);
        T GetById<T>(Guid id, long startPosition = 0) where T : AggregateRoot;
    }
}
