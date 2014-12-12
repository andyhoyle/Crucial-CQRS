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
    public interface IIdentityDbContext : IDisposable
    {
        IDbSet<AspNetRole> AspNetRoles { get; set; } // AspNetRoles
        IDbSet<AspNetUser> AspNetUsers { get; set; } // AspNetUsers
        IDbSet<AspNetUserClaim> AspNetUserClaims { get; set; } // AspNetUserClaims
        IDbSet<AspNetUserLogin> AspNetUserLogins { get; set; } // AspNetUserLogins
        IDbSet<Category> Categories { get; set; } // Category

        int SaveChanges();
    }

}
