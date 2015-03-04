using Crucial.Framework.DesignPatterns.CQRS.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Crucial.Qyz.Events
{
    [Serializable]
    public class UserCategoryDeletedEvent : Event
    {
        public UserCategoryDeletedEvent(int aggregateId, int version)
        {
			AggregateId = aggregateId;
            Version = version;
        }
    }
}
