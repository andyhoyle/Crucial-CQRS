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
using Crucial.Framework.Testing.EF;
using Crucial.Framework.Data.EntityFramework;
using System.Data.Common;

namespace Crucial.Providers.Questions.Data
{
    // Questions
    internal partial class QuestionConfiguration : EntityTypeConfiguration<Question>
    {
        public QuestionConfiguration(string schema = "dbo")
        {
            ToTable(schema + ".Questions");
            HasKey(x => x.Id);

            Property(x => x.Id).HasColumnName("Id").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(x => x.UserId).HasColumnName("UserId").IsRequired();
            Property(x => x.Text).HasColumnName("Text").IsOptional();
            Property(x => x.Image).HasColumnName("Image").IsOptional();
            Property(x => x.AnswerText).HasColumnName("AnswerText").IsOptional();
            Property(x => x.Type).HasColumnName("Type").IsRequired();
            Property(x => x.Version).HasColumnName("Version").IsRequired();
            Property(x => x.CreatedDate).HasColumnName("CreatedDate").IsRequired();
            Property(x => x.ModifiedDate).HasColumnName("ModifiedDate").IsOptional();
            InitializePartial();
        }
        partial void InitializePartial();
    }

}
