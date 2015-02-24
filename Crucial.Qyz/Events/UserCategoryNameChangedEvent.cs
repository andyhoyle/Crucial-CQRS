using Crucial.Framework.DesignPatterns.CQRS.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Crucial.Qyz.Events
{
    [Serializable]
    public class UserCategoryNameChangedEvent : Event
    {
        public string Name { get; internal set; }
        public UserCategoryNameChangedEvent(int aggregateId, string name, int version)
        {
			AggregateId = aggregateId;
            Name = name;
            Version = version;
        }
    }
}
