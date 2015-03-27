using Crucial.Framework.DesignPatterns.CQRS.Events;
using Crucial.Providers.Questions;
using Crucial.Providers.Questions.Entities;
using Crucial.Qyz.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Crucial.Framework.Logging;

namespace Crucial.Qyz.EventHandlers
{
    public class UserCategoryCreatedEventHandler : IEventHandler<UserCategoryCreatedEvent>
    {
        private ICategoryRepositoryAsync _categoryRepo;
        private ILogger _logger;

        public UserCategoryCreatedEventHandler(ICategoryRepositoryAsync categoryRepository, ILogger logger)
        {
            _categoryRepo = categoryRepository;
            _logger = logger;
        }

        public async Task Handle(UserCategoryCreatedEvent handle)
        {
            Category category = new Category()
            {
                Id = handle.AggregateId,
                Name = handle.Name,
                Version = handle.Version,
                CreatedDate = handle.Timestamp
            };

            _logger.Trace("UserCategoryCreatedEvent", handle);

            await _categoryRepo.Create(category).ConfigureAwait(false);
        }
    }
}
