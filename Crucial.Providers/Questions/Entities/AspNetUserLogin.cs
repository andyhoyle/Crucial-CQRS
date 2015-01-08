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

namespace Crucial.Providers.Questions.Entities
{
    // AspNetUserLogins
    public partial class AspNetUserLogin : Crucial.Framework.BaseEntities.ProviderEntityBase
    {
        public string LoginProvider { get; set; } // LoginProvider (Primary key)
        public string ProviderKey { get; set; } // ProviderKey (Primary key)
        public int UserId { get; set; } // UserId (Primary key)

        // Foreign keys
        public virtual AspNetUser AspNetUser { get; set; } // FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId
    }

}
