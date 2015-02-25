using Crucial.Framework.DesignPatterns.CQRS.Domain;
using Crucial.Framework.Entities.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crucial.EventStore.Mappers
{
    public class AggregateMapper : ProviderEntityMapper<Providers.EventStore.Entities.AggregateRoot, AggregateRoot>
    {

    }
}
