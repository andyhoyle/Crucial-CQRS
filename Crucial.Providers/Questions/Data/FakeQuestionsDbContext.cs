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
    public class FakeQuestionsDbContext : TestDbContextBase, IQuestionsDbContext
    {

        public IDbSet<Category> Categories { get; set; }
        public IDbSet<Question> Questions { get; set; }
        public IDbSet<QuestionAnswer> QuestionAnswers { get; set; }

        public FakeQuestionsDbContext()
        {
            Categories = new TestDbSet<Category>();
            Questions = new TestDbSet<Question>();
            QuestionAnswers = new TestDbSet<QuestionAnswer>();
        }

        public int SaveChanges()
        {
            return 0;
        }

        public void Dispose()
        {
            throw new NotImplementedException(); 
        }

		public DbSet<TEntity> Set<TEntity>() where TEntity : class
        {
            throw new NotImplementedException();
        }
    }
}
