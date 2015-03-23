using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crucial.Framework.DesignPatterns.CQRS.Events
{
    public interface IEventHandler<TEvent> where TEvent : Event
    {
        Task Handle(TEvent handle);
    }
}
