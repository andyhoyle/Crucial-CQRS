using Crucial.Framework.DesignPatterns.CQRS.Domain;
using Crucial.Framework.DesignPatterns.CQRS.Events;
using Crucial.Framework.DesignPatterns.CQRS.Storage;
using Crucial.Qyz.Domain.Mementos;
using Crucial.Qyz.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crucial.Qyz.Domain
{

    public class Question : AggregateRoot,
        IHandle<QuestionCreatedEvent>,
        IHandle<QuestionTextChangedEvent>,
        IHandle<QuestionDeletedEvent>,
        IOriginator
    {
        #region Public Parameterless Constructor

        public Question()
        {

        }

        #endregion

        #region Public properties

        public string QuestionText { get; set; }

        #endregion

        #region Internal command implementations

        internal Question(int id, string text)
        {
            ApplyChange(new QuestionCreatedEvent(id, text, DateTime.UtcNow));
        }

        internal void ChangeQuestionText(string questionText)
        {
            ApplyChange(new QuestionTextChangedEvent(Id, questionText, Version, DateTime.UtcNow));
        }

        internal void Delete()
        {
            ApplyChange(new QuestionDeletedEvent(Id, Version, DateTime.UtcNow));
        }

        #endregion

        #region IOriginator implementation

        public BaseMemento GetMemento()
        {
            return new QuestionMemento(Id, QuestionText, Version);
        }

        public void SetMemento(BaseMemento memento)
        {
            QuestionText = ((QuestionMemento)memento).QuestionText;
            Version = memento.Version;
            Id = memento.Id;
        }

        #endregion

        #region IHandle implementation

        public void Handle(QuestionCreatedEvent e)
        {
            QuestionText = e.Question;
            Version = e.Version;
            Id = e.AggregateId;
        }
        public void Handle(QuestionTextChangedEvent e)
        {
            QuestionText = e.Question;
            Version = e.Version;
            Id = e.AggregateId;
        }

        public void Handle(QuestionDeletedEvent e)
        {
            Version = e.Version;
            Id = e.AggregateId;
        }

        #endregion
    }

    public abstract class QuestionAnswer {
        
    }

    public abstract class QuestionAnswer<TAnswerType> : QuestionAnswer {
        public TAnswerType Answer { get; set; }
    }
}
