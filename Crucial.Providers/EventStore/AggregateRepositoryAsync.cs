using Crucial.Framework.DesignPatterns.Repository.Async;
using Crucial.Providers.EventStore.Data;
using Crucial.Providers.EventStore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Crucial.Framework.Data.EntityFramework;
using Crucial.Framework.Logging;

namespace Crucial.Providers.EventStore
{
    public interface IAggregateRepositoryAsync : 
        Crucial.Framework.DesignPatterns.Repository.Async.IReadOnlyRepository<Entities.AggregateRoot>,
        IUpdateRepositoryAsync<Entities.AggregateRoot>,
        ICreateRepositoryAsync<Entities.AggregateRoot, Entities.AggregateRoot>,
        IDeleteRepositoryAsync<Entities.AggregateRoot>,
        Framework.IoC.IAutoRegister
    {
    }

    public class AggregateRepositoryAsync : Crucial.Framework.Data.EntityFramework.Async.BaseRepository<IEventStoreContext, Entities.AggregateRoot, Entities.AggregateRoot>, IAggregateRepositoryAsync
    {
        public AggregateRepositoryAsync(IContextProvider<IEventStoreContext> cp, ILogger logger) : base(cp, logger) { }
    }
}
