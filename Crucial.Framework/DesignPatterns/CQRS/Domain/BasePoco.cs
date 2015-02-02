using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crucial.Framework.DesignPatterns.CQRS.Domain
{
    public abstract class BasePoco
    {
        public int Version { get; set; }
    }
}
