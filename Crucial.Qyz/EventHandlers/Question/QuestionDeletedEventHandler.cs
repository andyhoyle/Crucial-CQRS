using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crucial.Providers.Questions;
using Crucial.Framework.DesignPatterns.CQRS.Events;
using Crucial.Qyz.Events;

namespace Crucial.Qyz.EventHandlers
{
    public class QuestionDeletedEventHandler : IEventHandler<QuestionDeletedEvent>
    {
        private IQuestionRepository _categoryRepo;

        public QuestionDeletedEventHandler(IQuestionRepository categoryRepo)
        {
            _categoryRepo = categoryRepo;
        }

        public void Handle(QuestionDeletedEvent handle)
        {
            var item = _categoryRepo.FindBy(c => c.Id == handle.AggregateId).FirstOrDefault();

            if (item != null)
            {
                _categoryRepo.Delete(item);
            }
        }
    }
}
