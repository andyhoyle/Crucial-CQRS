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

namespace Crucial.Providers.EventStore.Entities
{
    // AggregateRoots
    public class AggregateRoot : Crucial.Framework.BaseEntities.ProviderEntityBase
    {
        public int Id { get; set; } // Id (Primary key)
        public int Version { get; set; } // Version
        public int EventVersion { get; set; } // EventVersion
    }

}
