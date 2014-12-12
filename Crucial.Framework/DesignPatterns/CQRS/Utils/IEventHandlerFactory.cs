using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crucial.DesignPatterns.CQRS.Events;

namespace Crucial.DesignPatterns.CQRS.Utils
{
    public interface IEventHandlerFactory
    {
        IEnumerable<IEventHandler<T>> GetHandlers<T>() where T : Event;
    }
}
