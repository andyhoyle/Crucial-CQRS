using Crucial.Framework.DesignPatterns.CQRS.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Crucial.Qyz.Events
{
    [Serializable]
    public class QuestionDeletedEvent : Event
    {
        public QuestionDeletedEvent(int aggregateId, int version, DateTime createdDate)
        {
            Timestamp = createdDate;
			AggregateId = aggregateId;
            Version = version;
        }
    }
}
