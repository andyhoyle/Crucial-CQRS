using Crucial.Framework.DesignPatterns.Repository.Async;
using Crucial.Providers.EventStore.Data;
using Crucial.Providers.EventStore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crucial.Providers.EventStore
{
    public interface IMementoRepositoryAsync : 
        Crucial.Framework.DesignPatterns.Repository.Async.IReadOnlyRepository<Entities.BaseMemento>,
        IUpdateRepositoryAsync<Entities.BaseMemento>,
        ICreateRepositoryAsync<Entities.BaseMemento, Entities.BaseMemento>,
        IDeleteRepositoryAsync<Entities.BaseMemento>,
        Framework.IoC.IAutoRegister
    {
    }

    public class MementoRepositoryAsync : Crucial.Framework.Data.EntityFramework.Async.BaseRepository<IEventStoreContext, Entities.BaseMemento, Entities.BaseMemento>, IMementoRepositoryAsync
    {
    }
}
