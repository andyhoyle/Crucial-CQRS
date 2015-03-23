using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Crucial.Framework.DesignPatterns.CQRS.Domain;
using Crucial.Framework.DesignPatterns.CQRS.Events;

namespace Crucial.Framework.DesignPatterns.CQRS.Storage
{
    public interface IEventStorage
    {
        Task<IEnumerable<Event>> GetEvents(int aggregateId);
        Task Save(AggregateRoot aggregate);
        Task<T> GetMemento<T>(int aggregateId) where T : BaseMemento;
        Task SaveMemento(BaseMemento memento);
        Task<IEnumerable<Event>> GetAllEvents();
    }
}
