using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crucial.DesignPatterns.CQRS.Events
{
    [Serializable]
    public class Event : IEvent
    {
        public int Version;
        public int AggregateId { get; set; }
        public int Id { get; private set; }
    }
}