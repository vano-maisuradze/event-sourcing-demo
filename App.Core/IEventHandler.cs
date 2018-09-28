
namespace App.Core
{
    public interface IEventHandler<T> where T : DomainEvent
    {
        void Handle(T domainEvent);
    }
}
