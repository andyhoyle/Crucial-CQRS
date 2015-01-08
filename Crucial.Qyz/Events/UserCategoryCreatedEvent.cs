using Crucial.DesignPatterns.CQRS.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crucial.Qyz.Events
{
    internal class UserCategoryCreatedEvent : Event
    {
        public string Name { get; internal set; }

        public UserCategoryCreatedEvent(int aggregateId, string name)
        {
            AggregateId = aggregateId;
            Name = name;
        }
    }
}
