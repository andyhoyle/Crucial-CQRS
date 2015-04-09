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
using Crucial.Providers.EventStore;
using Crucial.Framework.DesignPatterns.Repository.Async.Extensions;

namespace Crucial.EventStore
{
    public class DatabaseEventStorage : IEventStorage
    {
        private readonly IEventBus _eventBus;
        
        private IEventRepositoryAsync _eventRepository;
        private IAggregateRepositoryAsync _aggregateRepository;
        private IMementoRepositoryAsync _mementoRepository;

        private EventMapper _eventMapper;
        private MementoMapper _mementoMapper;
        private AggregateMapper _aggregateMapper;

        public DatabaseEventStorage(
            IEventRepositoryAsync eventRepository, 
            IAggregateRepositoryAsync aggregateRepository,
            IMementoRepositoryAsync mementoRepository,
            IEventBus eventBus)
        {
            _eventRepository = eventRepository;
            _aggregateRepository = aggregateRepository;
            _mementoRepository = mementoRepository;
            _eventBus = eventBus;
            _eventMapper = new EventMapper();
            _mementoMapper = new MementoMapper();
            _aggregateMapper = new AggregateMapper();
        }

        public async Task<IEnumerable<Event>> GetEvents(int aggregateId)
        {
            var events = await _eventRepository.FindByAsync(e => e.AggregateId == aggregateId).ConfigureAwait(false);
            return events.Select(_eventMapper.ToAnyEntity);
        }

        public async Task<IEnumerable<Event>> GetAllEvents()
        {
            var events = await _eventRepository.GetAllAsync().ConfigureAwait(false);
            return events.Select(_eventMapper.ToAnyEntity);
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
                        await SaveMemento(memento).ConfigureAwait(false);
                    }
                }

                @event.Version = version;
                aggregate.Version = version;

                if (version == 0)
                {
                    var ag = await _aggregateRepository.Create(_aggregateMapper.ToProviderEntity(aggregate)).ConfigureAwait(false);
                    @event.AggregateId = ag.Id;
                }
                else
                {
                    var aggregateRoots = await _aggregateRepository.FindByAsync(x => x.Id == aggregate.Id).ConfigureAwait(false);
                    var aggregateRoot = aggregateRoots.FirstOrDefault();
                    
                    if (aggregateRoot == null)
                    {
                        throw new NullReferenceException();
                    }

                    aggregateRoot.Version = version;
                    await _aggregateRepository.Update(aggregateRoot).ConfigureAwait(false);
                }

                await _eventRepository.Create(_eventMapper.ToProviderEntity(@event)).ConfigureAwait(false);
            }

            foreach (var @event in uncommittedChanges)
            {
                var desEvent = Converter.ChangeTo(@event, @event.GetType());
                await _eventBus.Publish(desEvent).ConfigureAwait(false);
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

        public async Task<T> GetMemento<T>(int aggregateId) where T : BaseMemento
        {
            var mementos = await _mementoRepository.FindByAsync(m => m.Id == aggregateId).ConfigureAwait(false);
            var memento = mementos.LastOrDefault();

            if (memento != null)
                return (T)_mementoMapper.ToAnyEntity(memento);
            
            return null;
        }

        public async Task SaveMemento(Framework.DesignPatterns.CQRS.Domain.BaseMemento memento)
        {
            await _mementoRepository.Create(_mementoMapper.ToProviderEntity(memento)).ConfigureAwait(false);
        }
    }
}
