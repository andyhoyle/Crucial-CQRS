using System;
using Crucial.DesignPatterns.CQRS.Events;

namespace Crucial.DesignPatterns.CQRS.Messaging
{
    public interface IEventBus
    {
        void Publish<T>(T @event) where T : Event;
    }
}
