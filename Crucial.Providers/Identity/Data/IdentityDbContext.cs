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
    public partial class IdentityDbContext : DbContext, IIdentityDbContext
    {
        public IDbSet<AspNetUser> AspNetUsers { get; set; } // AspNetUsers
        public IDbSet<AspNetUserClaim> AspNetUserClaims { get; set; } // AspNetUserClaims
        public IDbSet<AspNetUserLogin> AspNetUserLogins { get; set; } // AspNetUserLogins
        public IDbSet<AspNetUserRole> AspNetUserRoles { get; set; } // AspNetUserRoles

        static IdentityDbContext()
        {
            Database.SetInitializer<IdentityDbContext>(null);
        }

        public IdentityDbContext()
            : base("Name=DefaultConnection")
        {
        InitializePartial();
        }

        public IdentityDbContext(string connectionString) : base(connectionString)
        {
        InitializePartial();
        }

        public IdentityDbContext(string connectionString, System.Data.Entity.Infrastructure.DbCompiledModel model) : base(connectionString, model)
        {
        InitializePartial();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Configurations.Add(new AspNetUserConfiguration());
            modelBuilder.Configurations.Add(new AspNetUserClaimConfiguration());
            modelBuilder.Configurations.Add(new AspNetUserLoginConfiguration());
            modelBuilder.Configurations.Add(new AspNetUserRoleConfiguration());
        OnModelCreatingPartial(modelBuilder);
        }

        public static DbModelBuilder CreateModel(DbModelBuilder modelBuilder, string schema)
        {
            modelBuilder.Configurations.Add(new AspNetUserConfiguration(schema));
            modelBuilder.Configurations.Add(new AspNetUserClaimConfiguration(schema));
            modelBuilder.Configurations.Add(new AspNetUserLoginConfiguration(schema));
            modelBuilder.Configurations.Add(new AspNetUserRoleConfiguration(schema));
            return modelBuilder;
        }

        partial void InitializePartial();
        partial void OnModelCreatingPartial(DbModelBuilder modelBuilder);
    }
}
