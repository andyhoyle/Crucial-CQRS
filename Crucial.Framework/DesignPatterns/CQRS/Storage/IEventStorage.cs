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
        IEnumerable<Event> GetEvents(int aggregateId);
        void Save(AggregateRoot aggregate);
        T GetMemento<T>(int aggregateId) where T : BaseMemento;
        void SaveMemento(BaseMemento memento);
        IEnumerable<Event> GetAllEvents();
    }
}
