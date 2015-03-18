using Crucial.Framework.DesignPatterns.CQRS.Events;
using Crucial.Framework.Entities.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crucial.EventStore.Mappers
{
    public class EventMapper : ProviderEntityMapper<Providers.EventStore.Entities.Event, Event>
    {
        public override Providers.EventStore.Entities.Event ToProviderEntity(Event source)
        {
            var target = base.ToProviderEntity(source);
            target.Data = DatabaseEventStorage.Serialize<Event>(source);
            return target;
        }

        public override Event ToAnyEntity(Providers.EventStore.Entities.Event source)
        {
            var target = DatabaseEventStorage.DeSerialize<Event>(source.Data);
            return target;
        }
    }
}
