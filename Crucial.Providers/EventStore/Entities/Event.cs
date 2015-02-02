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
    // Event
    public class Event : Crucial.Framework.BaseEntities.ProviderEntityBase
    {
        public int Id { get; set; } // Id (Primary key)
        public int AggregateId { get; set; } // AggregateId
        public byte[] Data { get; set; } // Data
    }

}
