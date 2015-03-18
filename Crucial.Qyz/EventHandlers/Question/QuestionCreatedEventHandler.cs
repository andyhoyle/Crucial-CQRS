using Crucial.Framework.DesignPatterns.CQRS.Events;
using Crucial.Providers.Questions;
using Crucial.Providers.Questions.Entities;
using Crucial.Qyz.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Crucial.Qyz.Events;

namespace Crucial.Qyz.EventHandlers
{
    public class QuestionCreatedEventHandler : 
        IEventHandler<QuestionCreatedEvent>
    {
        private IQuestionRepository _questionRepo;

        public QuestionCreatedEventHandler(IQuestionRepository questionRepository)
        {
            _questionRepo = questionRepository;
        }

        public void Handle(QuestionCreatedEvent handle)
        {
            Question question = new Question()
            {
                Id = handle.AggregateId,
                Text = handle.Question,
                Version = handle.Version,
                CreatedDate = handle.Timestamp
            };

            _questionRepo.Create(question);
        }
    }
}
