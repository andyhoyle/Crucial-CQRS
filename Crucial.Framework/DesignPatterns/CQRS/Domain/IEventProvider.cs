using System.Collections.Generic;
using Crucial.Framework.DesignPatterns.CQRS.Events;
using System.Threading.Tasks;

namespace Crucial.Framework.DesignPatterns.CQRS.Domain
{
    public interface IEventProvider
    {
        Task LoadsFromHistory(IEnumerable<Event> history);
        IEnumerable<Event> GetUncommittedChanges();
    }
}
