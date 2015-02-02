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
    // AspNetUserRoles
    public partial class AspNetUserRole : Crucial.Framework.BaseEntities.ProviderEntityBase
    {
        public int UserId { get; set; } // UserId (Primary key)
        public string RoleId { get; set; } // RoleId (Primary key)

        // Foreign keys
        public virtual AspNetUser AspNetUser { get; set; } // FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId
    }

}
