using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Crucial.Framework.DesignPatterns.CQRS.Domain;
using Crucial.Framework.DesignPatterns.CQRS.Events;
using Crucial.Framework.DesignPatterns.CQRS.Exceptions;
using Crucial.Framework.DesignPatterns.CQRS.Messaging;
using Crucial.Framework.DesignPatterns.CQRS.Utils;

namespace Crucial.Framework.DesignPatterns.CQRS.Storage
{
    public class InMemoryEventStorage : IEventStorage
    {
        private List<Event> _events;
        private List<BaseMemento> _mementos;

        private readonly IEventBus _eventBus;

        public InMemoryEventStorage(IEventBus eventBus)
        {
            _events = new List<Event>();
            _mementos = new List<BaseMemento>();
            _eventBus = eventBus;
        }

        public async Task<IEnumerable<Event>> GetEvents(int aggregateId)
        {
            var events = await Task.Run(() => _events.Where(p => p.AggregateId == aggregateId).Select(p => p)).ConfigureAwait(false);
            
            if (events.Count() == 0)
            {
                throw new AggregateNotFoundException(string.Format("Aggregate with Id: {0} was not found", aggregateId));
            }

            return events;
        }

        public async Task<IEnumerable<Event>> GetAllEvents()
        {
            var events = await Task.Run(() => _events.Select(p => p)).ConfigureAwait(false);
            return events;
        }

        public async Task Save(AggregateRoot aggregate)
        {
            var uncommittedChanges = aggregate.GetUncommittedChanges();
            var version = aggregate.Version;

            foreach (var @event in uncommittedChanges)
            {
                version++;
                if (version > 2)
                {
                    if (version % 3 == 0)
                    {
                        var originator = (IOriginator)aggregate;
                        var memento = originator.GetMemento();
                        memento.Version = version;
                        await SaveMemento(memento);
                    }
                }
                @event.Version = version;
                _events.Add(@event);
            }

            List<Task> tasks = new List<Task>();

            foreach (var @event in uncommittedChanges)
            {
                var desEvent = Converter.ChangeTo(@event, @event.GetType());
                tasks.Add(_eventBus.Publish(desEvent));
            }

            await Task.WhenAll(tasks);
        }

        public Task<T> GetMemento<T>(int aggregateId) where T : BaseMemento
        {
            return Task.Run(() => GetMementoSyncronous<T>(aggregateId));
        }

        private T GetMementoSyncronous<T>(int aggregateId) where T : BaseMemento
        {
            var memento = _mementos.FirstOrDefault(m => m.Id == aggregateId);
            
            if (memento != null)
                return (T)memento;
            
            return null;
        }

        public Task SaveMemento(BaseMemento memento)
        {
            return Task.Run(() => _mementos.Add(memento));
        }
    }
}
