using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crucial.Framework.Entities
{
    public abstract class CqrsViewModel
    {
        public int Version { get; set; }
    }
}
