using System;
using Crucial.Framework.DesignPatterns.CQRS.Events;
using System.Collections.Generic;

namespace Crucial.Framework.DesignPatterns.CQRS.Messaging
{
    public interface IEventBus
    {
        void Publish<T>(T @event) where T : Event;
        void Replay(IEnumerable<Event> eventList);
    }
}
