using System;
using Crucial.Framework.DesignPatterns.CQRS.Events;

namespace Crucial.Framework.DesignPatterns.CQRS.Messaging
{
    public interface IEventBus
    {
        void Publish<T>(T @event) where T : Event;
    }
}
