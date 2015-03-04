// ReSharper disable RedundantUsingDirective
// ReSharper disable DoNotCallOverridableMethodsInConstructor
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable PartialMethodWithSinglePart
// ReSharper disable RedundantNameQualifier

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using Crucial.Providers.EventStore.Entities;
//using DatabaseGeneratedOption = System.ComponentModel.DataAnnotations.DatabaseGeneratedOption;

namespace Crucial.Providers.EventStore.Data
{
    public interface IEventStoreContext : IDisposable
    {
        IDbSet<AggregateRoot> AggregateRoots { get; set; } // AggregateRoots
        IDbSet<BaseMemento> BaseMementoes { get; set; } // BaseMementoes
        IDbSet<Event> Events { get; set; } // Event

        int SaveChanges();
    }

}
