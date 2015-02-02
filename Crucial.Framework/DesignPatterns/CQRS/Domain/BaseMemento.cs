using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crucial.Framework.DesignPatterns.CQRS.Domain
{
    public class BaseMemento
    {
        public int Id { get; set; }
        public int Version { get; set; }
    }
}
