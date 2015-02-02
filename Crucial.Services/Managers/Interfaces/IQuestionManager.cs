using System;
using System.Collections.Generic;
namespace Crucial.Services.Managers.Interfaces
{
    public interface IQuestionManager
    {
        Crucial.Services.ServiceEntities.Category CreateCategory(string name);

        IEnumerable<Crucial.Services.ServiceEntities.Category> GetUserCategories();
    }
}
