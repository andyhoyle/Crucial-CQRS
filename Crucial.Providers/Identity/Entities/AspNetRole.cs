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
    // AspNetRoles
    public partial class AspNetRole : Crucial.Framework.BaseEntities.ProviderEntityBase
    {
        public string Id { get; set; } // Id (Primary key)
        public string Name { get; set; } // Name

        // Reverse navigation
        public virtual ICollection<AspNetUser> AspNetUsers { get; set; } // Many to many mapping

        public AspNetRole()
        {
            AspNetUsers = new List<AspNetUser>();
            InitializePartial();
        }
        partial void InitializePartial();
    }

}
