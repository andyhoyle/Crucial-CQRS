using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crucial.Services.ServiceEntities
{
    public class Category : Framework.BaseEntities.ServiceEntityBase
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
