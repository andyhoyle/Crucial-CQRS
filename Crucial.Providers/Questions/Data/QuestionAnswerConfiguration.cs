// ReSharper disable RedundantUsingDirective
// ReSharper disable DoNotCallOverridableMethodsInConstructor
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable PartialMethodWithSinglePart
// ReSharper disable RedundantNameQualifier

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using Crucial.Providers.Questions.Entities;
//using DatabaseGeneratedOption = System.ComponentModel.DataAnnotations.DatabaseGeneratedOption;
using Crucial.Framework.Data.EntityFramework;
using Crucial.Framework.Testing.EF;

namespace Crucial.Providers.Questions.Data
{
    // QuestionAnswers
    internal partial class QuestionAnswerConfiguration : EntityTypeConfiguration<QuestionAnswer>
    {
        public QuestionAnswerConfiguration(string schema = "dbo")
        {
            ToTable(schema + ".QuestionAnswers");
            HasKey(x => x.Id);

            Property(x => x.Id).HasColumnName("Id").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.QuestionId).HasColumnName("QuestionId").IsRequired();
            Property(x => x.Text).HasColumnName("Text").IsOptional();
            Property(x => x.Image).HasColumnName("Image").IsOptional();
            Property(x => x.CorrectAnswer).HasColumnName("CorrectAnswer").IsRequired();

            // Foreign keys
            HasRequired(a => a.Question).WithMany(b => b.QuestionAnswers).HasForeignKey(c => c.QuestionId); // FK_dbo.QuestionAnswers_dbo.Questions_QuestionId
            InitializePartial();
        }
        partial void InitializePartial();
    }

}
