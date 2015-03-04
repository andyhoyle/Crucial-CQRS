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
    public class FakeEventStoreContext : IEventStoreContext
    {
        public IDbSet<AggregateRoot> AggregateRoots { get; set; }
        public IDbSet<BaseMemento> BaseMementoes { get; set; }
        public IDbSet<Event> Events { get; set; }

        public FakeEventStoreContext()
        {
            AggregateRoots = new FakeDbSet<AggregateRoot>();
            BaseMementoes = new FakeDbSet<BaseMemento>();
            Events = new FakeDbSet<Event>();
        }

        public int SaveChanges()
        {
            return 0;
        }

        public void Dispose()
        {
            throw new NotImplementedException(); 
        }
    }
}
