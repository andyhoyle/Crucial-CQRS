using Crucial.Framework.DesignPatterns.CQRS.Events;
using Crucial.Providers.Questions;
using Crucial.Providers.Questions.Entities;
using Crucial.Qyz.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crucial.Qyz.EventHandlers
{
    public class UserCategoryCreatedEventHandler : IEventHandler<UserCategoryCreatedEvent>
    {
        private ICategoryRepositoryAsync _categoryRepo;

        public UserCategoryCreatedEventHandler(ICategoryRepositoryAsync categoryRepository)
        {
            _categoryRepo = categoryRepository;
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

            await _categoryRepo.Create(category).ConfigureAwait(false);
        }
    }
}
