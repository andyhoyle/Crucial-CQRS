using Crucial.Framework.DesignPatterns.CQRS.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crucial.Qyz.Events
{
    [Serializable]
    public class QuestionCreatedEvent : Event
    {
        public string Question { get; internal set; }

        public QuestionCreatedEvent(int aggregateId, string question, DateTime createdDate)
        {
            Timestamp = createdDate;
            AggregateId = aggregateId;
            Question = question;
        }
    }

    [Serializable]
    public class PictureQuestionCreatedEvent : Event
    {
        public byte[] Image { get; set; }

        public string Answer { get; set; }

        public PictureQuestionCreatedEvent(int aggregateId, byte[] image, string answer)
        {
            AggregateId = aggregateId;
            Image = image;
            Answer = answer;
        }
    }

    [Serializable]
    public class MultipleChoiceQuestionCreatedEvent : Event
    {
        public string Question { get; internal set; }

        public Dictionary<string, bool> Answer { get; set; }

        public MultipleChoiceQuestionCreatedEvent(int aggregateId, string question, Dictionary<string, bool> answers)
        {
            AggregateId = aggregateId;
            Answer = answers;
            Question = question;
        }
    }

    [Serializable]
    public class NumericAnswerQuestionCreatedEvent : Event
    {
        public string Question { get; internal set; }

        public int Answer { get; set; }

        public NumericAnswerQuestionCreatedEvent(int aggregateId, string question, int answer)
        {
            AggregateId = aggregateId;
            Question = question;
            Answer = answer;
        }
    }
}
