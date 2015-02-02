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
    // Category
    public partial class Category : Crucial.Framework.BaseEntities.ProviderEntityBase
    {
        public int Id { get; set; } // Id (Primary key)
        public int UserId { get; set; } // UserId
        public string Name { get; set; } // Name
        public int Version { get; set; } // Version
    }

}
