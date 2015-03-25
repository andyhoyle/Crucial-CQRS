// ReSharper disable RedundantUsingDirective
// ReSharper disable DoNotCallOverridableMethodsInConstructor
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable PartialMethodWithSinglePart
// ReSharper disable RedundantNameQualifier

using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using Crucial.Providers.Questions.Entities;
//using DatabaseGeneratedOption = System.ComponentModel.DataAnnotations.DatabaseGeneratedOption;
using Crucial.Framework.Data.EntityFramework;
using System.Data.Common;

namespace Crucial.Providers.Questions.Data
{
    public interface IQuestionsDbContext : IDbContextAsync, IDisposable
    {
        IDbSet<Category> Categories { get; set; } // Category
        IDbSet<Question> Questions { get; set; } // Questions
        IDbSet<QuestionAnswer> QuestionAnswers { get; set; } // QuestionAnswers

    }

}
