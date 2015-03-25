// ReSharper disable RedundantUsingDirective
// ReSharper disable DoNotCallOverridableMethodsInConstructor
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable PartialMethodWithSinglePart
// ReSharper disable RedundantNameQualifier

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Common;

namespace Crucial.Providers.Questions.Entities
{
    // QuestionAnswers
    public partial class QuestionAnswer : Crucial.Framework.BaseEntities.ProviderEntityBase
    {
        public int Id { get; set; } // Id (Primary key)
        public int QuestionId { get; set; } // QuestionId
        public string Text { get; set; } // Text
        public byte[] Image { get; set; } // Image
        public bool CorrectAnswer { get; set; } // CorrectAnswer

        // Foreign keys
        public virtual Question Question { get; set; } // FK_dbo.QuestionAnswers_dbo.Questions_QuestionId
    }

}
