using Crucial.Framework.DesignPatterns.CQRS.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Crucial.Qyz.Events
{
    [Serializable]
    public class QuestionTextChangedEvent : Event
    {
        public string Question { get; internal set; }
        public QuestionTextChangedEvent(int aggregateId, string questionText, int version, DateTime createdDate)
        {
            Timestamp = createdDate;
			AggregateId = aggregateId;
            Question = questionText;
            Version = version;
        }
    }
}
