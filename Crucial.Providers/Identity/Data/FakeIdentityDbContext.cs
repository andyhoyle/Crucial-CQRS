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
        public IDbSet<AspNetUser> AspNetUsers { get; set; }
        public IDbSet<AspNetUserClaim> AspNetUserClaims { get; set; }
        public IDbSet<AspNetUserLogin> AspNetUserLogins { get; set; }
        public IDbSet<AspNetUserRole> AspNetUserRoles { get; set; }

        public FakeIdentityDbContext()
        {
            AspNetUsers = new FakeDbSet<AspNetUser>();
            AspNetUserClaims = new FakeDbSet<AspNetUserClaim>();
            AspNetUserLogins = new FakeDbSet<AspNetUserLogin>();
            AspNetUserRoles = new FakeDbSet<AspNetUserRole>();
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
