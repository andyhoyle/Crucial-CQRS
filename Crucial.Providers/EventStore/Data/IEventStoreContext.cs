// ReSharper disable RedundantUsingDirective
// ReSharper disable DoNotCallOverridableMethodsInConstructor
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable PartialMethodWithSinglePart
// ReSharper disable RedundantNameQualifier

using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using Crucial.Providers.EventStore.Entities;
//using DatabaseGeneratedOption = System.ComponentModel.DataAnnotations.DatabaseGeneratedOption;
using Crucial.Framework.Testing.EF;
using Crucial.Framework.Data.EntityFramework;
using System.Data.Common;

namespace Crucial.Providers.EventStore.Data
{
    public interface IEventStoreContext : IDbContextAsync, IDisposable
    {
        IDbSet<AggregateRoot> AggregateRoots { get; set; } // AggregateRoots
        IDbSet<BaseMemento> BaseMementoes { get; set; } // BaseMementoes
        IDbSet<Event> Events { get; set; } // Event

        //int SaveChanges();
    }

}
