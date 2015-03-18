using Crucial.Framework.Data.EntityFramework;
using Crucial.Providers.Questions;
using Crucial.Providers.Questions.Data;
using Crucial.Providers.EventStore.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Crucial.EventStore;
using Moq;
using Crucial.Providers.Identity.Data;
using Crucial.Providers.Questions.Entities;
using System.Collections.ObjectModel;
using System.Data.Entity.Infrastructure;
using System.Linq.Expressions;
using Crucial.Tests.Mocks;
using System.Threading;
using Crucial.Framework.Testing.EF;
using Crucial.Providers.EventStore.Entities;

namespace Crucial.Tests.Mocks
{

    public class TestQuestionContext : TestDbContextBase, IQuestionsDbContext
    {
        TestDbSet<Category> _categories;
        TestDbSet<Question> _questions;
        TestDbSet<QuestionAnswer> _questionAnswers;

        public TestQuestionContext()
        {
            this.Categories = new TestDbSet<Category>();
            this.Questions = new TestDbSet<Question>();
            this.QuestionAnswers = new TestDbSet<QuestionAnswer>();
        }

        public IDbSet<Category> Categories {
            get
            {
                return _categories as IDbSet<Category>;
            }
            set
            {
                _categories = value as TestDbSet<Category>;
            }
        }

        public DbSet<TEntity> Set<TEntity>() where TEntity : class
        {
            return this.Categories as TestDbSet<TEntity>;
        }


        public IDbSet<Question> Questions
        {
            get
            {
                return _questions as IDbSet<Question>;
            }
            set
            {
                _questions = value as TestDbSet<Question>;
            }
        }

        public IDbSet<QuestionAnswer> QuestionAnswers
        {
            get
            {
                return _questionAnswers as IDbSet<QuestionAnswer>;
            }
            set
            {
                _questionAnswers = value as TestDbSet<QuestionAnswer>;
            }
        }
    }

    public class TestEventContext : TestDbContextBase, IEventStoreContext
    {
        public TestEventContext()
        {
            this.Events = new TestDbSet<Event>();
            this.AggregateRoots = new TestDbSet<AggregateRoot>();
            this.BaseMementoes = new TestDbSet<BaseMemento>();
        }

        public IDbSet<Event> Events { get; set; }

        public IDbSet<AggregateRoot> AggregateRoots { get; set; }

        public IDbSet<BaseMemento> BaseMementoes { get; set; }
        public DbSet<TEntity> Set<TEntity>() where TEntity : class
        {
            throw new NotImplementedException();
        }
    }
}
