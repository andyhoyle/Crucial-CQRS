// ReSharper disable RedundantUsingDirective
// ReSharper disable DoNotCallOverridableMethodsInConstructor
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable PartialMethodWithSinglePart
// ReSharper disable RedundantNameQualifier

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace Crucial.Providers.Identity.Entities
{
    // AspNetUsers
    public partial class AspNetUser : Crucial.Framework.BaseEntities.ProviderEntityBase
    {
        public int Id { get; set; } // Id (Primary key)
        public string Email { get; set; } // Email
        public bool EmailConfirmed { get; set; } // EmailConfirmed
        public string PasswordHash { get; set; } // PasswordHash
        public string SecurityStamp { get; set; } // SecurityStamp
        public string PhoneNumber { get; set; } // PhoneNumber
        public bool PhoneNumberConfirmed { get; set; } // PhoneNumberConfirmed
        public bool TwoFactorEnabled { get; set; } // TwoFactorEnabled
        public DateTime? LockoutEndDateUtc { get; set; } // LockoutEndDateUtc
        public bool LockoutEnabled { get; set; } // LockoutEnabled
        public int AccessFailedCount { get; set; } // AccessFailedCount
        public string UserName { get; set; } // UserName

        // Reverse navigation
        public virtual ICollection<AspNetRole> AspNetRoles { get; set; } // Many to many mapping
        public virtual ICollection<AspNetUserClaim> AspNetUserClaims { get; set; } // AspNetUserClaims.FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId
        public virtual ICollection<AspNetUserLogin> AspNetUserLogins { get; set; } // Many to many mapping

        public AspNetUser()
        {
            AspNetUserClaims = new List<AspNetUserClaim>();
            AspNetUserLogins = new List<AspNetUserLogin>();
            AspNetRoles = new List<AspNetRole>();
            InitializePartial();
        }
        partial void InitializePartial();
    }

}
