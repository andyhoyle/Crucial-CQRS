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
using Crucial.Framework.Data.EntityFramework;
using System.Data.Common;

namespace Crucial.Providers.EventStore.Data
{
    public class EventStoreContext : DbContext, IEventStoreContext
    {
        public IDbSet<AggregateRoot> AggregateRoots { get; set; } // AggregateRoots
        public IDbSet<BaseMemento> BaseMementoes { get; set; } // BaseMementoes
        public IDbSet<Event> Events { get; set; } // Event
		private readonly bool _avoidDisposeForTesting = false;
        
		private void Configure() 
		{
			base.Configuration.LazyLoadingEnabled = false;
		}

		static EventStoreContext()
        {
			Database.SetInitializer<EventStoreContext>(new CreateDatabaseIfNotExists<EventStoreContext>());
        }


        public EventStoreContext()
            : base("Name=EventStore")
        {
			Configure();
        }

        public EventStoreContext(string connectionString) : base(connectionString)
        {
			Configure();
        }

        public EventStoreContext(string connectionString, System.Data.Entity.Infrastructure.DbCompiledModel model) : base(connectionString, model)
        {
			Configure();
        }

		// For testing fake DB needs to be singleton and not call dispose 
		public EventStoreContext(DbConnection connection, bool avoidDisposeForTesting) : base(connection, true)
        {
			Database.SetInitializer(new CreateDatabaseIfNotExists<EventStoreContext>());
			_avoidDisposeForTesting = avoidDisposeForTesting;
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
		public void Dispose()
        {
            if (!_avoidDisposeForTesting)
            {
                base.Dispose();
            }
        }

    }
}
