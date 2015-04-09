using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Crucial.Framework.DesignPatterns.CQRS.Events;
using Crucial.Framework.DesignPatterns.Repository.Async.Extensions;
using Crucial.Providers.Questions;
using Crucial.Qyz.Events;

namespace Crucial.Qyz.EventHandlers.UserCategory
{
    public class QuestionAddedToCategoryEventHandler : IEventHandler<UserCategoryNameChangedEvent>
    {
        private ICategoryRepositoryAsync _categoryRepo;

        public QuestionAddedToCategoryEventHandler(ICategoryRepositoryAsync categoryRepo)
        {
            _categoryRepo = categoryRepo;
        }

        public async Task Handle(UserCategoryNameChangedEvent handle)
        {
            var items = await _categoryRepo.FindByAsync(c => c.Id == handle.AggregateId).ConfigureAwait(false);

            var item = items.FirstOrDefault();

            if (item != null)
            {
                item.Name = handle.Name;
                item.Version = handle.Version;
                item.ModifiedDate = handle.Timestamp;
            }

            await _categoryRepo.Update(item).ConfigureAwait(false);
        }
    }
}
