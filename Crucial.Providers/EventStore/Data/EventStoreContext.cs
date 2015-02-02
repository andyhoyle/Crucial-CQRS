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
    public class EventStoreContext : DbContext, IEventStoreContext
    {
        public IDbSet<AggregateRoot> AggregateRoots { get; set; } // AggregateRoots
        public IDbSet<BaseMemento> BaseMementoes { get; set; } // BaseMementoes
        public IDbSet<Event> Events { get; set; } // Event

        static EventStoreContext()
        {
            Database.SetInitializer<EventStoreContext>(null);
        }

        public EventStoreContext()
            : base("Name=EventStore")
        {
        }

        public EventStoreContext(string connectionString) : base(connectionString)
        {
        }

        public EventStoreContext(string connectionString, System.Data.Entity.Infrastructure.DbCompiledModel model) : base(connectionString, model)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Configurations.Add(new AggregateRootConfiguration());
            modelBuilder.Configurations.Add(new BaseMementoConfiguration());
            modelBuilder.Configurations.Add(new EventConfiguration());
        }

        public static DbModelBuilder CreateModel(DbModelBuilder modelBuilder, string schema)
        {
            modelBuilder.Configurations.Add(new AggregateRootConfiguration(schema));
            modelBuilder.Configurations.Add(new BaseMementoConfiguration(schema));
            modelBuilder.Configurations.Add(new EventConfiguration(schema));
            return modelBuilder;
        }
    }
}
