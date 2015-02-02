using Crucial.Framework.DesignPatterns.CQRS.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Crucial.Framework.DesignPatterns.CQRS.Events;
using Crucial.Framework.DesignPatterns.CQRS.Domain;
using Crucial.Framework.DesignPatterns.CQRS.Utils;
using Crucial.Framework.DesignPatterns.CQRS.Messaging;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using Crucial.Framework.Entities.Mappers;

namespace Crucial.EventStore
{
    public class EventStoreContextProvider : Framework.Data.EntityFramework.ContextProvider<Crucial.Providers.EventStore.Data.EventStoreContext>, IEventStoreContextProvider
    {
    }

    public interface IEventStoreContextProvider : Crucial.Framework.Data.EntityFramework.IDatabaseContextProvider
    {

    }

    public class EventMapper : ProviderEntityMapper<Providers.EventStore.Entities.Event, Event>
    {
        public override Providers.EventStore.Entities.Event ToProviderEntity(Event source)
        {
            var target = base.ToProviderEntity(source);
            target.Data = DatabaseEventStorage.SerializeEvent(source);
            return target;
        }

        public override Event ToThirdPartyEntity(Providers.EventStore.Entities.Event source)
        {
            return base.ToThirdPartyEntity(source);
        }
    }

    public class MementoMapper : ProviderEntityMapper<Providers.EventStore.Entities.BaseMemento, BaseMemento>
    {

    }

    public class DatabaseEventStorage : IEventStorage
    {
        private readonly IEventBus _eventBus;

        private Providers.EventStore.Data.EventStoreContext _eventStoreContext;

        private EventMapper _eventMapper;
        private MementoMapper _mementoMapper;
        private IEventStoreContextProvider _contextProvider;

        public DatabaseEventStorage(IEventStoreContextProvider contextProvider, IEventBus eventBus)
        {
            _eventStoreContext = contextProvider.DbContext as Providers.EventStore.Data.EventStoreContext;
            _eventBus = eventBus;
            _eventMapper = new EventMapper();
            _mementoMapper = new MementoMapper();
        }

        public IEnumerable<Event> GetEvents(int aggregateId)
        {
            return _eventStoreContext.Events.Where(e => e.AggregateId == aggregateId).Select(s => _eventMapper.ToThirdPartyEntity(s)).ToList();
        }

        public void Save(AggregateRoot aggregate)
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
                        SaveMemento(memento);
                    }
                }

                @event.Version = version;
                _eventStoreContext.Events.Add(_eventMapper.ToProviderEntity(@event));
                _eventStoreContext.SaveChangesAsync();
            }

            foreach (var @event in uncommittedChanges)
            {
                var desEvent = Converter.ChangeTo(@event, @event.GetType());
                _eventBus.Publish(desEvent);
            }
        }

        public static byte[] SerializeEvent(Event e)
        {
            var desEvent = Converter.ChangeTo(e, e.GetType());
            byte[] bytes;
            IFormatter formatter = new BinaryFormatter();
            
            using (MemoryStream stream = new MemoryStream())
            {
                formatter.Serialize(stream, desEvent);
                bytes = stream.ToArray();
            }

            return bytes;
        }

        public T GetMemento<T>(int aggregateId) where T : BaseMemento
        {
            var memento = _eventStoreContext.BaseMementoes.Where(m => m.Id == aggregateId).Select(m => _mementoMapper.ToThirdPartyEntity(m)).LastOrDefault();
            if (memento != null)
                return (T)memento;
            return null;
        }

        public void SaveMemento(Framework.DesignPatterns.CQRS.Domain.BaseMemento memento)
        {
            _eventStoreContext.BaseMementoes.Add(_mementoMapper.ToProviderEntity(memento));
            _eventStoreContext.SaveChangesAsync();

        }


    }
}
