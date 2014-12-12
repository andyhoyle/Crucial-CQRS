using System.Collections.Generic;
using Crucial.DesignPatterns.CQRS.Events;

namespace Crucial.DesignPatterns.CQRS.Domain
{
    public interface IEventProvider
    {
        void LoadsFromHistory(IEnumerable<Event> history);
        IEnumerable<Event> GetUncommittedChanges();
    }
}
