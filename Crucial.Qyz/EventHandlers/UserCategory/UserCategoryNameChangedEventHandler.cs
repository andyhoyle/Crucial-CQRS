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
    public class UserCategoryNameChangedEventHandler : IEventHandler<UserCategoryNameChangedEvent>
    {
        private ICategoryRepositoryAsync _categoryRepo;

        public UserCategoryNameChangedEventHandler(ICategoryRepositoryAsync categoryRepo)
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
