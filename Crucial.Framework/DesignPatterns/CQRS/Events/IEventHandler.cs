using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crucial.DesignPatterns.CQRS.Events
{
    public interface IEventHandler<TEvent> where TEvent : Event
    {
        void Handle(TEvent handle);
    }
}
