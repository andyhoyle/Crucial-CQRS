using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crucial.Services.Managers.Interfaces
{
    public interface ICategoryManager
    {
        IEnumerable<Crucial.Services.ServiceEntities.Category> GetUserCategories();

        ServiceEntities.Category GetUserCategory(int categoryId);
    }
}
