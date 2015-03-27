using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crucial.Providers.Questions;
using Crucial.Framework.DesignPatterns.CQRS.Events;
using Crucial.Qyz.Events;
using System.Threading.Tasks;
using Crucial.Framework.DesignPatterns.Repository.Async.Extensions;
using Crucial.Framework.Logging;

namespace Crucial.Qyz.EventHandlers
{
    public class UserCategoryDeletedEventHandler : IEventHandler<UserCategoryDeletedEvent>
    {
        private ICategoryRepositoryAsync _categoryRepo;
        private ILogger _logger;

        public UserCategoryDeletedEventHandler(ICategoryRepositoryAsync categoryRepo, ILogger logger)
        {
            _categoryRepo = categoryRepo;
            _logger = logger;
        }

        public async Task Handle(UserCategoryDeletedEvent handle)
        {
            var items = await _categoryRepo.FindByAsync(c => c.Id == handle.AggregateId).ConfigureAwait(false);

            var item = items.FirstOrDefault();

            _logger.Trace("UserCategoryDeletedEvent", handle.AggregateId);
            
            if (item != null)
            {
                await _categoryRepo.Delete(item).ConfigureAwait(false);
            }
        }
    }
}
