using Crucial.Framework.DesignPatterns.CQRS.Domain;
using Crucial.Framework.Entities.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crucial.EventStore.Mappers
{
    public class MementoMapper : ProviderEntityMapper<Providers.EventStore.Entities.BaseMemento, BaseMemento>
    {
        public override Providers.EventStore.Entities.BaseMemento ToProviderEntity(BaseMemento source)
        {
            var target = base.ToProviderEntity(source);
            target.Data = DatabaseEventStorage.Serialize<BaseMemento>(source);
            return target;
        }

        public override BaseMemento ToAnyEntity(Providers.EventStore.Entities.BaseMemento source)
        {
            var target = DatabaseEventStorage.DeSerialize<BaseMemento>(source.Data);
            return target;
        }
    }
}
