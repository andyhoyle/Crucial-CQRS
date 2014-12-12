using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Crucial.DesignPatterns.CQRS.Domain;
using Crucial.DesignPatterns.CQRS.Events;

namespace Crucial.DesignPatterns.CQRS.Storage
{
    public interface IEventStorage
    {
        IEnumerable<Event> GetEvents(int aggregateId);
        void Save(AggregateRoot aggregate);
        T GetMemento<T>(int aggregateId) where T : BaseMemento;
        void SaveMemento(BaseMemento memento);
    }
}
