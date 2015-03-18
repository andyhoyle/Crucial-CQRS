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
    // Questions
    public partial class Question : Crucial.Framework.BaseEntities.ProviderEntityBase
    {
        public int Id { get; set; } // Id (Primary key)
        public int UserId { get; set; } // UserId
        public string Text { get; set; } // Text
        public byte[] Image { get; set; } // Image
        public string AnswerText { get; set; } // AnswerText
        public byte Type { get; set; } // Type
        public int Version { get; set; } // Version
        public DateTime CreatedDate { get; set; } // CreatedDate
        public DateTime? ModifiedDate { get; set; } // ModifiedDate

        // Reverse navigation
        public virtual ICollection<Category> Categories { get; set; } // Many to many mapping
        public virtual ICollection<QuestionAnswer> QuestionAnswers { get; set; } // QuestionAnswers.FK_dbo.QuestionAnswers_dbo.Questions_QuestionId

        public Question()
        {
            QuestionAnswers = new List<QuestionAnswer>();
            Categories = new List<Category>();
            InitializePartial();
        }
        partial void InitializePartial();
    }

}
