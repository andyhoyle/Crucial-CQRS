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
    public partial class QuestionsDbContext : DbContext, IQuestionsDbContext
    {
        public IDbSet<Category> Categories { get; set; } // Category
        public IDbSet<Question> Questions { get; set; } // Questions
        public IDbSet<QuestionAnswer> QuestionAnswers { get; set; } // QuestionAnswers

        static QuestionsDbContext()
        {
            Database.SetInitializer<QuestionsDbContext>(null);
        }

		public static void Drop()
        {
            Database.SetInitializer<QuestionsDbContext>(new DropCreateDatabaseAlways<QuestionsDbContext>());
            QuestionsDbContext ctx = new QuestionsDbContext();
            ctx.Database.Initialize(true);
            ctx.Dispose();
            Database.SetInitializer<QuestionsDbContext>(null);
        }

        public QuestionsDbContext()
            : base("Name=DefaultConnection")
        {
        InitializePartial();
        }

        public QuestionsDbContext(string connectionString) : base(connectionString)
        {
        InitializePartial();
        }

        public QuestionsDbContext(string connectionString, System.Data.Entity.Infrastructure.DbCompiledModel model) : base(connectionString, model)
        {
        InitializePartial();
        }

		public QuestionsDbContext(DbConnection connection) : base(connection, true)
        {
			Database.SetInitializer(new DropCreateDatabaseAlways<QuestionsDbContext>());
        InitializePartial();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Configurations.Add(new CategoryConfiguration());
            modelBuilder.Configurations.Add(new QuestionConfiguration());
            modelBuilder.Configurations.Add(new QuestionAnswerConfiguration());
        OnModelCreatingPartial(modelBuilder);
        }

		public void SetState<TEntity>(TEntity entityItem, EntityState state) where TEntity : Crucial.Framework.BaseEntities.ProviderEntityBase
        {
            Entry<TEntity>(entityItem).State = state;
        }

        public static DbModelBuilder CreateModel(DbModelBuilder modelBuilder, string schema)
        {
            modelBuilder.Configurations.Add(new CategoryConfiguration(schema));
            modelBuilder.Configurations.Add(new QuestionConfiguration(schema));
            modelBuilder.Configurations.Add(new QuestionAnswerConfiguration(schema));
            return modelBuilder;
        }

        partial void InitializePartial();
        partial void OnModelCreatingPartial(DbModelBuilder modelBuilder);
    }
}
