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
    public class QuestionsDbContext : DbContext, IQuestionsDbContext
    {
        public IDbSet<Category> Categories { get; set; } // Category
        public IDbSet<Question> Questions { get; set; } // Questions
        public IDbSet<QuestionAnswer> QuestionAnswers { get; set; } // QuestionAnswers
		private readonly bool _avoidDisposeForTesting = false;
        
		private void Configure() 
		{
			base.Configuration.LazyLoadingEnabled = false;
		}

		static QuestionsDbContext()
        {
			Database.SetInitializer<QuestionsDbContext>(new CreateDatabaseIfNotExists<QuestionsDbContext>());
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
			Configure();
        }

        public QuestionsDbContext(string connectionString) : base(connectionString)
        {
			Configure();
        }

        public QuestionsDbContext(string connectionString, System.Data.Entity.Infrastructure.DbCompiledModel model) : base(connectionString, model)
        {
			Configure();
        }

		// For testing fake DB needs to be singleton and not call dispose 
		public QuestionsDbContext(DbConnection connection, bool avoidDisposeForTesting) : base(connection, true)
        {
			Database.SetInitializer(new CreateDatabaseIfNotExists<QuestionsDbContext>());
			_avoidDisposeForTesting = avoidDisposeForTesting;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Configurations.Add(new CategoryConfiguration());
            modelBuilder.Configurations.Add(new QuestionConfiguration());
            modelBuilder.Configurations.Add(new QuestionAnswerConfiguration());
        }

        public static DbModelBuilder CreateModel(DbModelBuilder modelBuilder, string schema)
        {
            modelBuilder.Configurations.Add(new CategoryConfiguration(schema));
            modelBuilder.Configurations.Add(new QuestionConfiguration(schema));
            modelBuilder.Configurations.Add(new QuestionAnswerConfiguration(schema));
            return modelBuilder;
        }
		public void Dispose()
        {
            if (!_avoidDisposeForTesting)
            {
                base.Dispose();
            }
        }

    }
}
