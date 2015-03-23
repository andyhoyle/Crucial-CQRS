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
    // BaseMementoes
    internal class BaseMementoConfiguration : EntityTypeConfiguration<BaseMemento>
    {
        public BaseMementoConfiguration(string schema = "dbo")
        {
            ToTable(schema + ".BaseMementoes");
            HasKey(x => new { x.Id, x.Version });

            Property(x => x.Id).HasColumnName("Id").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(x => x.Version).HasColumnName("Version").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(x => x.Data).HasColumnName("Data").IsRequired();
        }
    }

}
