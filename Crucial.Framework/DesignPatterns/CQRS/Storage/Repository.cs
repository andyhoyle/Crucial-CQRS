using System;
using System.Linq;
using System.Collections.Generic;
using Crucial.Framework.DesignPatterns.CQRS.Domain;
using Crucial.Framework.DesignPatterns.CQRS.Exceptions;
using Crucial.Framework.DesignPatterns.CQRS.Events;
using System.Threading.Tasks;
using System.Threading;
using Crucial.Framework.Threading;

namespace Crucial.Framework.DesignPatterns.CQRS.Storage
{
    public class Repository<T> : IRepository<T> where T : AggregateRoot, new()
    {
        private readonly IEventStorage _storage;
        private static readonly NamedLocker _locker = new NamedLocker();

        public Repository(IEventStorage storage)
        {
            _storage = storage;
        }

        public void Save(AggregateRoot aggregate, int expectedVersion)
        {
            if (aggregate.GetUncommittedChanges().Any())
            {
                lock (_locker.GetLock(aggregate.Id.ToString()))
                {
                    var item = new T();

                    if (expectedVersion != -1)
                    {
                        item = GetById(aggregate.Id).Result;

                        if (item.Version != expectedVersion)
                        {
                            throw new ConcurrencyException(string.Format("Aggregate {0} has been previously modified", item.Id));
                        }
                    }

                    _storage.Save(aggregate).Wait();
                }
            }
        }

        public async Task<T> GetById(int id)
        {
            IEnumerable<Event> events;
            var memento = await _storage.GetMemento<BaseMemento>(id).ConfigureAwait(false);
            if (memento != null)
            {
                var list = await _storage.GetEvents(id).ConfigureAwait(false);
                events = list.Where(e => e.Version >= memento.Version);
            }
            else
            {
                events = await _storage.GetEvents(id).ConfigureAwait(false);
            }

            var obj = new T();

            if (memento != null)
            {
                ((IOriginator) obj).SetMemento(memento);
            }

            await Task.Run(() => obj.LoadsFromHistory(events)).ConfigureAwait(false);
            return obj;
        }
    }
}
