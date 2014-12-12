using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crucial.DesignPatterns.CQRS.Events;
using Crucial.DesignPatterns.CQRS.Messaging;
using Crucial.DesignPatterns.CQRS.Utils;

namespace Crucial.DesignPatterns.CQRS.Messaging
{
    public class EventBus:IEventBus
    {
        private IEventHandlerFactory _eventHandlerFactory;

        public EventBus(IEventHandlerFactory eventHandlerFactory)
        {
            _eventHandlerFactory = eventHandlerFactory;
        }
        
        public void Publish<T>(T @event) where T : Event
        {
            var handlers = _eventHandlerFactory.GetHandlers<T>();
            foreach (var eventHandler in handlers)
            {
                eventHandler.Handle(@event);
            }
        }
    }
}
