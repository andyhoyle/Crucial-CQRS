using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crucial.Providers.Questions;
using Crucial.Framework.DesignPatterns.CQRS.Events;
using Crucial.Qyz.Events;

namespace Crucial.Qyz.EventHandlers
{
    public class QuestionTextChangedEventHandler : IEventHandler<QuestionTextChangedEvent>
    {
        private IQuestionRepository _categoryRepo;

        public QuestionTextChangedEventHandler(IQuestionRepository categoryRepo)
        {
            _categoryRepo = categoryRepo;
        }

        public void Handle(QuestionTextChangedEvent handle)
        {
            var item = _categoryRepo.FindBy(c => c.Id == handle.AggregateId).FirstOrDefault();

            if (item != null)
            {
                item.Text = handle.Question;
                item.Version = handle.Version;
                item.ModifiedDate = handle.Timestamp;
            }

            _categoryRepo.Update(item);
        }
    }
}
