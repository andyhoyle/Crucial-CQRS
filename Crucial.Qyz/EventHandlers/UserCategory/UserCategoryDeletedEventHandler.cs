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
    public class UserCategoryDeletedEventHandler : IEventHandler<UserCategoryDeletedEvent>
    {
        private ICategoryRepositoryAsync _categoryRepo;

        public UserCategoryDeletedEventHandler(ICategoryRepositoryAsync categoryRepo)
        {
            _categoryRepo = categoryRepo;
        }

        public async Task Handle(UserCategoryDeletedEvent handle)
        {
            var items = await _categoryRepo.FindByAsync(c => c.Id == handle.AggregateId).ConfigureAwait(false);

            var item = items.FirstOrDefault();

            if (item != null)
            {
                await _categoryRepo.Delete(item).ConfigureAwait(false);
            }
        }
    }
}
