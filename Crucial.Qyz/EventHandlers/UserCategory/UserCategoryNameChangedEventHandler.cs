using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crucial.Providers.Questions;
using Crucial.Framework.DesignPatterns.CQRS.Events;
using Crucial.Qyz.Events;

namespace Crucial.Qyz.EventHandlers
{
    public class UserCategoryNameChangedEventHandler : IEventHandler<UserCategoryNameChangedEvent>
    {
        private ICategoryRepository _categoryRepo;

        public UserCategoryNameChangedEventHandler(ICategoryRepository categoryRepo)
        {
            _categoryRepo = categoryRepo;
        }

        public void Handle(UserCategoryNameChangedEvent handle)
        {
            var item = _categoryRepo.FindBy(c => c.Id == handle.AggregateId).FirstOrDefault();

            if (item != null)
            {
                item.Name = handle.Name;
                item.Version = handle.Version;
                item.ModifiedDate = handle.Timestamp;
            }

            _categoryRepo.Update(item);
        }
    }
}
