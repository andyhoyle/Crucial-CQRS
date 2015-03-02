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
using Crucial.Framework.Data.EntityFramework;
using Crucial.Providers.EventStore.Data;
using Crucial.EventStore.Mappers;

namespace Crucial.EventStore
{
    public class DatabaseEventStorage : IEventStorage
    {
        private readonly IEventBus _eventBus;
        private Providers.EventStore.Data.IEventStoreContext _eventStoreContext;
        private EventMapper _eventMapper;
        private MementoMapper _mementoMapper;
        private AggregateMapper _aggregateMapper;

        public DatabaseEventStorage(ContextProvider<IEventStoreContext> contextProvider, IEventBus eventBus)
        {
            _eventStoreContext = contextProvider.DbContext;
            _eventBus = eventBus;
            _eventMapper = new EventMapper();
            _mementoMapper = new MementoMapper();
            _aggregateMapper = new AggregateMapper();
        }

        public IEnumerable<Event> GetEvents(int aggregateId)
        {
            return _eventStoreContext.Events.Where(e => e.AggregateId == aggregateId).Select(s => _eventMapper.ToThirdPartyEntity(s)).ToList();
        }

        public IEnumerable<Event> GetAllEvents()
        {
            return _eventStoreContext.Events.Select(s => _eventMapper.ToThirdPartyEntity(s)).ToList();
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
                aggregate.Version = version;

                if (version == 0)
                {
                    _eventStoreContext.AggregateRoots.Add(_aggregateMapper.ToProviderEntity(aggregate));
                }
                else
                {
                    var aggregateRoot = _eventStoreContext.AggregateRoots.Where(x => x.Id == aggregate.Id).FirstOrDefault();
                    
                    if (aggregateRoot == null)
                    {
                        throw new NullReferenceException();
                    }

                    aggregateRoot.Version = version;
                }

                _eventStoreContext.Events.Add(_eventMapper.ToProviderEntity(@event));
                _eventStoreContext.SaveChanges();
            }

            foreach (var @event in uncommittedChanges)
            {
                var desEvent = Converter.ChangeTo(@event, @event.GetType());
                _eventBus.Publish(desEvent);
            }
        }

        public static byte[] Serialize<T>(T e)
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

        public static dynamic DeSerialize(byte[] data)
        {
            var formatter = new BinaryFormatter();
            dynamic e;

            using (var ms = new MemoryStream(data))
            {
                e = formatter.Deserialize(ms);
            }

            return e;
        }

        public static T DeSerialize<T>(byte[] data)
        {
            T e;
            var formatter = new BinaryFormatter();
            using (var ms = new MemoryStream(data))
            {
                 e = (T)formatter.Deserialize(ms);
            }

            return e;
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
            _eventStoreContext.SaveChanges();
        }
    }
}
