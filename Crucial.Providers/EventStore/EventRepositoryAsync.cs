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
    public interface IEventRepositoryAsync : 
        Crucial.Framework.DesignPatterns.Repository.Async.IReadOnlyRepository<Entities.Event>,
        IUpdateRepositoryAsync<Entities.Event>,
        ICreateRepositoryAsync<Entities.Event, Entities.Event>,
        IDeleteRepositoryAsync<Entities.Event>,
        Framework.IoC.IAutoRegister
    {
    }

    public class EventRepositoryAsync : Crucial.Framework.Data.EntityFramework.Async.BaseRepository<IEventStoreContext, Entities.Event, Entities.Event>, IEventRepositoryAsync
    {
        public EventRepositoryAsync(IContextProvider<IEventStoreContext> cp, ILogger logger) : base(cp, logger) {}
    }
}
