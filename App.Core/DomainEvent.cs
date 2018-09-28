using System;

namespace App.Core
{
    public abstract class DomainEvent
    {
        public Guid AggregateRootId { get; set; }

        public DomainEvent()
        {

        }

        public DomainEvent(Guid aggregateRootId)
        {
            AggregateRootId = aggregateRootId;
        }

    }
}
