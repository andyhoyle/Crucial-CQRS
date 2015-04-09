using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Crucial.Framework.DesignPatterns.CQRS.Events;

namespace Crucial.Qyz.Events
{

    [Serializable]
    public class QuestionAddedToCategoryEvent : Event
    {
        public int QuestionId { get; internal set; }
        public QuestionAddedToCategoryEvent(int questionId, int aggregateId, int version, DateTime createdDate)
        {
            Timestamp = createdDate;
			AggregateId = aggregateId;
            QuestionId = questionId;
            Version = version;
        }
    }
}
