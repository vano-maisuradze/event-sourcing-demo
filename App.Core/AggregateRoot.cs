using Newtonsoft.Json;
using System.Collections.Generic;

namespace App.Core
{
    public abstract class AggregateRoot : Entity
    {
        private readonly List<DomainEvent> _pendingEvents = new List<DomainEvent>();

        [JsonIgnore]
        public virtual IReadOnlyList<DomainEvent> PendingEvents => _pendingEvents;

        public virtual void HandleEvent(DomainEvent newEvent)
        {
            _pendingEvents.Add(newEvent);
        }

        public virtual void ClearEvents()
        {
            _pendingEvents.Clear();
        }
    }
}
