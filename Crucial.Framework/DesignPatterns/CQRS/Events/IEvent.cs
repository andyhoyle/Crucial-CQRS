using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crucial.Framework.DesignPatterns.CQRS.Events
{
    public interface IEvent
    {
        int Version { get; set; }
        int AggregateId { get; set; }
        int Id { get; set; }
        DateTime Timestamp { get; set; }
    }
}
