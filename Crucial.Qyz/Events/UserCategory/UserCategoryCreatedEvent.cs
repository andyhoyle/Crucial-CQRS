using Crucial.Framework.DesignPatterns.CQRS.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crucial.Qyz.Events
{
    [Serializable]
    public class UserCategoryCreatedEvent : Event
    {
        public string Name { get; internal set; }

        public UserCategoryCreatedEvent(int aggregateId, string name, DateTime createdDate)
        {
            Timestamp = createdDate;
            AggregateId = aggregateId;
            Name = name;
        }
    }
}
