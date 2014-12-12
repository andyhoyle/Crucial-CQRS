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
using Crucial.Providers.Identity.Entities;
//using DatabaseGeneratedOption = System.ComponentModel.DataAnnotations.DatabaseGeneratedOption;

namespace Crucial.Providers.Identity.Data
{
    // AspNetUserLogins
    internal partial class AspNetUserLoginConfiguration : EntityTypeConfiguration<AspNetUserLogin>
    {
        public AspNetUserLoginConfiguration(string schema = "dbo")
        {
            ToTable(schema + ".AspNetUserLogins");
            HasKey(x => new { x.LoginProvider, x.ProviderKey, x.UserId });

            Property(x => x.LoginProvider).HasColumnName("LoginProvider").IsRequired().HasMaxLength(128).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(x => x.ProviderKey).HasColumnName("ProviderKey").IsRequired().HasMaxLength(128).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(x => x.UserId).HasColumnName("UserId").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Foreign keys
            HasRequired(a => a.AspNetUser).WithMany(b => b.AspNetUserLogins).HasForeignKey(c => c.UserId); // FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId
            InitializePartial();
        }
        partial void InitializePartial();
    }

}
