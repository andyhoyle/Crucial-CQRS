using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crucial.Framework.DesignPatterns.CQRS.Events;
using Crucial.Framework.DesignPatterns.CQRS.Messaging;
using Crucial.Framework.DesignPatterns.CQRS.Utils;

namespace Crucial.Framework.DesignPatterns.CQRS.Messaging
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

        public void Replay(IEnumerable<Event> eventList)
        {
            foreach (dynamic ev in eventList)
            {
                var handlers = _eventHandlerFactory.GetHandlers(ev);

                foreach (var eventHandler in handlers)
                {
                    eventHandler.Handle(ev);
                }
            }
        }
    }
}
