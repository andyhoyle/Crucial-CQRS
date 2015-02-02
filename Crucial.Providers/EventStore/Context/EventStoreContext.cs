//using Crucial.Framework.DesignPatterns.CQRS.Domain;
//using Crucial.Framework.DesignPatterns.CQRS.Events;
//using System;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations.Schema;
//using System.Data.Entity;
//using System.Data.Entity.ModelConfiguration;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Crucial.Providers.EventStore.Context
//{
//    public partial class EventStoreContext : DbContext
//    {
//        public EventStoreContext() : base("Name=EventStore")
//        {

//        }
        
//        public DbSet<Event> Events { get; set; }
//        public DbSet<BaseMemento> Mementos { get; set; }
//        public DbSet<AggregateRoot> AggregateRoots { get; set; }

//        protected override void OnModelCreating(DbModelBuilder modelBuilder)
//        {
//            base.OnModelCreating(modelBuilder);

//            modelBuilder.Configurations.Add(new EventConfiguration());
//            OnModelCreatingPartial(modelBuilder);
//        }

//        public static DbModelBuilder CreateModel(DbModelBuilder modelBuilder, string schema)
//        {
//            modelBuilder.Configurations.Add(new EventConfiguration(schema));
//            return modelBuilder;
//        }

//        partial void InitializePartial();
//        partial void OnModelCreatingPartial(DbModelBuilder modelBuilder);
//    }

//    internal partial class EventConfiguration : EntityTypeConfiguration<Event>
//    {
//        public EventConfiguration(string schema = "dbo")
//        {
//            ToTable(schema + ".Event");
//            HasKey(x => x.Id);

//            Property(x => x.Id).HasColumnName("Id").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
//            Property(x => x.AggregateId).HasColumnName("AggregateId").IsRequired();
//            Property(x => x.Data).HasColumnName("Data").IsRequired();
//            InitializePartial();
//        }
//        partial void InitializePartial();
//    }
//}
