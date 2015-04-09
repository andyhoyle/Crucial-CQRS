using System;
using Crucial.Framework.DesignPatterns.CQRS.Events;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Crucial.Framework.DesignPatterns.CQRS.Messaging
{
    public interface IEventBus
    {
        Task Publish<T>(T @event) where T : Event;
        Task Replay(IEnumerable<Event> eventList);
    }
}
