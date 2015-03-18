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
        private ICategoryRepository _categoryRepo;

        public UserCategoryCreatedEventHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepo = categoryRepository;
        }

        public void Handle(UserCategoryCreatedEvent handle)
        {
            Category category = new Category()
            {
                Id = handle.AggregateId,
                Name = handle.Name,
                Version = handle.Version,
                CreatedDate = handle.Timestamp
            };

            _categoryRepo.Create(category);
        }
    }
}
