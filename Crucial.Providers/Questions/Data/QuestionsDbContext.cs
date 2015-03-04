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
    public partial class QuestionsDbContext : DbContext, IQuestionsDbContext
    {
        public IDbSet<Category> Categories { get; set; } // Category

        static QuestionsDbContext()
        {
            Database.SetInitializer<QuestionsDbContext>(new DropCreateDatabaseAlways<QuestionsDbContext>());
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

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Configurations.Add(new CategoryConfiguration());
        OnModelCreatingPartial(modelBuilder);
        }

        public static DbModelBuilder CreateModel(DbModelBuilder modelBuilder, string schema)
        {
            modelBuilder.Configurations.Add(new CategoryConfiguration(schema));
            return modelBuilder;
        }

        partial void InitializePartial();
        partial void OnModelCreatingPartial(DbModelBuilder modelBuilder);
		public void SetState<TEntity>(TEntity entityItem, EntityState state) where TEntity : Crucial.Framework.BaseEntities.ProviderEntityBase
        {
            Entry<TEntity>(entityItem).State = state;
        }
    }
}
