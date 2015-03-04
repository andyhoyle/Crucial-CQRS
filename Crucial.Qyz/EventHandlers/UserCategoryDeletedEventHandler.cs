using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crucial.Providers.Questions;
using Crucial.Framework.DesignPatterns.CQRS.Events;
using Crucial.Qyz.Events;

namespace Crucial.Qyz.EventHandlers
{
    public class UserCategoryDeletedEventHandler : IEventHandler<UserCategoryDeletedEvent>
    {
        private ICategoryRepository _categoryRepo;

        public UserCategoryDeletedEventHandler(ICategoryRepository categoryRepo)
        {
            _categoryRepo = categoryRepo;
        }

        public void Handle(UserCategoryDeletedEvent handle)
        {
            var item = _categoryRepo.FindBy(c => c.Id == handle.AggregateId).FirstOrDefault();

            if (item != null)
            {
                _categoryRepo.Delete(item);
            }
        }
    }
}
