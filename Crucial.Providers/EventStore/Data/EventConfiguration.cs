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
    // Event
    internal class EventConfiguration : EntityTypeConfiguration<Event>
    {
        public EventConfiguration(string schema = "dbo")
        {
            ToTable(schema + ".Event");
            HasKey(x => x.Id);

            Property(x => x.Id).HasColumnName("Id").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.AggregateId).HasColumnName("AggregateId").IsRequired();
            Property(x => x.Data).HasColumnName("Data").IsRequired();
            Property(x => x.Timestamp).HasColumnName("Timestamp").IsRequired();
        }
    }

}
