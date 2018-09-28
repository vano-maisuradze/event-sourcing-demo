using App.Core;
using BankAccounts.Domain.Repositories;
using EventStore.ClientAPI;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace BankAccounts.Commands.Repository
{
    public class EventStoreRepository : IRepository
    {
        private readonly string _streamPrefix = "bank_account_";

        private JsonSerializerSettings _jsonSettings = new JsonSerializerSettings();

        public EventStoreRepository()
        {
            _jsonSettings.TypeNameHandling = TypeNameHandling.Objects;
        }

        public void Append(DomainEvent domainEvent)
        {
            var connection = CreateConnection();
            var streamName = $"{_streamPrefix}{domainEvent.AggregateRootId}";

            var eventId = Guid.NewGuid();
            var eventType = domainEvent.GetType().Name;
            var isJson = true;
            var data = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(domainEvent, _jsonSettings));
            var metadata = Encoding.UTF8.GetBytes(domainEvent.AggregateRootId.ToString());
            var eventData = new EventData(eventId, eventType, isJson, data, metadata);

            connection.AppendToStreamAsync(streamName, ExpectedVersion.Any, eventData).Wait();
        }

        public void Save(AggregateRoot aggregateRoot)
        {
            foreach (var domainEvent in aggregateRoot.PendingEvents)
            {
                Append(domainEvent);
            }
        }

        public T GetById<T>(Guid id, long startPosition = 0) where T : AggregateRoot
        {
            var connection = CreateConnection();

            var streamName = $"{_streamPrefix}{id}";
            var streamEvents = new List<ResolvedEvent>();

            StreamEventsSlice currentSlice;
            do
            {
                currentSlice = connection.ReadStreamEventsForwardAsync(streamName, startPosition, 100, false).Result;
                startPosition = currentSlice.NextEventNumber;

                streamEvents.AddRange(currentSlice.Events);
            } while (!currentSlice.IsEndOfStream);

            dynamic aggregate = Activator.CreateInstance<T>();

            foreach (var evt in streamEvents)
            {
                dynamic domainEvent = JsonConvert.DeserializeObject(Encoding.UTF8.GetString(evt.Event.Data), _jsonSettings);
                aggregate.ApplyEvent(domainEvent);
            }

            return aggregate;
        }

        private static IEventStoreConnection CreateConnection()
        {
            var connection = EventStoreConnection.Create(new IPEndPoint(IPAddress.Loopback, 1113));
            connection.ConnectAsync().Wait();
            return connection;
        }
    }
}
