using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crucial.Framework.DesignPatterns.CQRS.Events;
using System.Collections;

namespace Crucial.Framework.DesignPatterns.CQRS.Utils
{
    public interface IEventHandlerFactory
    {
        IEnumerable<IEventHandler<T>> GetHandlers<T>() where T : Event;

        IEnumerable<IEventHandler<T>> GetHandlers<T>(T type) where T : Event;
    }
}
