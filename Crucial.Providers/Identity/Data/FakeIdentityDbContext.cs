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
    public class FakeIdentityDbContext : IIdentityDbContext
    {
        public IDbSet<AspNetRole> AspNetRoles { get; set; }
        public IDbSet<AspNetUser> AspNetUsers { get; set; }
        public IDbSet<AspNetUserClaim> AspNetUserClaims { get; set; }
        public IDbSet<AspNetUserLogin> AspNetUserLogins { get; set; }
        public IDbSet<Category> Categories { get; set; }

        public FakeIdentityDbContext()
        {
            AspNetRoles = new FakeDbSet<AspNetRole>();
            AspNetUsers = new FakeDbSet<AspNetUser>();
            AspNetUserClaims = new FakeDbSet<AspNetUserClaim>();
            AspNetUserLogins = new FakeDbSet<AspNetUserLogin>();
            Categories = new FakeDbSet<Category>();
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
