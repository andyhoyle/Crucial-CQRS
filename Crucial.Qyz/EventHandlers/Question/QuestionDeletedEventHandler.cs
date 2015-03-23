using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crucial.Providers.Questions;
using Crucial.Framework.DesignPatterns.CQRS.Events;
using Crucial.Qyz.Events;
using System.Threading.Tasks;
using Crucial.Framework.DesignPatterns.Repository.Async.Extensions;

namespace Crucial.Qyz.EventHandlers
{
    public class QuestionDeletedEventHandler : IEventHandler<QuestionDeletedEvent>
    {
        private IQuestionRepositoryAsync _questionRepo;

        public QuestionDeletedEventHandler(IQuestionRepositoryAsync questionRepo)
        {
            _questionRepo = questionRepo;
        }

        public async Task Handle(QuestionDeletedEvent handle)
        {
            var items = await _questionRepo.FindByAsync(c => c.Id == handle.AggregateId).ConfigureAwait(false);

            var item = items.FirstOrDefault();

            if (item != null)
            {
                await _questionRepo.Delete(item).ConfigureAwait(false);
            }
        }
    }
}
