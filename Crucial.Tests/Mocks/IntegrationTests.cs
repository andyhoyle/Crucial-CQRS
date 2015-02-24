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

        public TestQuestionContext()
        {
            this.Categories = new TestDbSet<Category>();
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
            return this.Events as TestDbSet<TEntity>;
        }
    }


    //public class TestDbSet<TEntity> : DbSet<TEntity>, IQueryable, IEnumerable<TEntity>, IDbAsyncEnumerable<TEntity>
    //    where TEntity : class
    //{
    //    ObservableCollection<TEntity> _data;
    //    IQueryable _query;

    //    public TestDbSet()
    //    {
    //        _data = new ObservableCollection<TEntity>();
    //        _query = _data.AsQueryable();
    //    }

    //    public override TEntity Add(TEntity item)
    //    {
    //        _data.Add(item);
    //        return item;
    //    }

    //    public override TEntity Remove(TEntity item)
    //    {
    //        _data.Remove(item);
    //        return item;
    //    }

    //    public override TEntity Attach(TEntity item)
    //    {
    //        _data.Add(item);
    //        return item;
    //    }

    //    public override TEntity Create()
    //    {
    //        return Activator.CreateInstance<TEntity>();
    //    }

    //    public override TDerivedEntity Create<TDerivedEntity>()
    //    {
    //        return Activator.CreateInstance<TDerivedEntity>();
    //    }

    //    public override ObservableCollection<TEntity> Local
    //    {
    //        get { return _data; }
    //    }

    //    Type IQueryable.ElementType
    //    {
    //        get { return _query.ElementType; }
    //    }

    //    Expression IQueryable.Expression
    //    {
    //        get { return _query.Expression; }
    //    }

    //    IQueryProvider IQueryable.Provider
    //    {
    //        get { return new TestDbAsyncQueryProvider<TEntity>(_query.Provider); }
    //    }

    //    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
    //    {
    //        return _data.GetEnumerator();
    //    }

    //    IEnumerator<TEntity> IEnumerable<TEntity>.GetEnumerator()
    //    {
    //        return _data.GetEnumerator();
    //    }

    //    IDbAsyncEnumerator<TEntity> IDbAsyncEnumerable<TEntity>.GetAsyncEnumerator()
    //    {
    //        return new TestDbAsyncEnumerator<TEntity>(_data.GetEnumerator());
    //    }
    //}

    //internal class TestDbAsyncQueryProvider<TEntity> : IDbAsyncQueryProvider
    //{
    //    private readonly IQueryProvider _inner;

    //    internal TestDbAsyncQueryProvider(IQueryProvider inner)
    //    {
    //        _inner = inner;
    //    }

    //    public IQueryable CreateQuery(Expression expression)
    //    {
    //        return new TestDbAsyncEnumerable<TEntity>(expression);
    //    }

    //    public IQueryable<TElement> CreateQuery<TElement>(Expression expression)
    //    {
    //        return new TestDbAsyncEnumerable<TElement>(expression);
    //    }

    //    public object Execute(Expression expression)
    //    {
    //        return _inner.Execute(expression);
    //    }

    //    public TResult Execute<TResult>(Expression expression)
    //    {
    //        return _inner.Execute<TResult>(expression);
    //    }

    //    public Task<object> ExecuteAsync(Expression expression, CancellationToken cancellationToken)
    //    {
    //        return Task.FromResult(Execute(expression));
    //    }

    //    public Task<TResult> ExecuteAsync<TResult>(Expression expression, CancellationToken cancellationToken)
    //    {
    //        return Task.FromResult(Execute<TResult>(expression));
    //    }
    //}

    //internal class TestDbAsyncEnumerable<T> : EnumerableQuery<T>, IDbAsyncEnumerable<T>, IQueryable<T>
    //{
    //    public TestDbAsyncEnumerable(IEnumerable<T> enumerable)
    //        : base(enumerable)
    //    { }

    //    public TestDbAsyncEnumerable(Expression expression)
    //        : base(expression)
    //    { }

    //    public IDbAsyncEnumerator<T> GetAsyncEnumerator()
    //    {
    //        return new TestDbAsyncEnumerator<T>(this.AsEnumerable().GetEnumerator());
    //    }

    //    IDbAsyncEnumerator IDbAsyncEnumerable.GetAsyncEnumerator()
    //    {
    //        return GetAsyncEnumerator();
    //    }

    //    IQueryProvider IQueryable.Provider
    //    {
    //        get { return new TestDbAsyncQueryProvider<T>(this); }
    //    }
    //}

    //internal class TestDbAsyncEnumerator<T> : IDbAsyncEnumerator<T>
    //{
    //    private readonly IEnumerator<T> _inner;

    //    public TestDbAsyncEnumerator(IEnumerator<T> inner)
    //    {
    //        _inner = inner;
    //    }

    //    public void Dispose()
    //    {
    //        _inner.Dispose();
    //    }

    //    public Task<bool> MoveNextAsync(CancellationToken cancellationToken)
    //    {
    //        return Task.FromResult(_inner.MoveNext());
    //    }

    //    public T Current
    //    {
    //        get { return _inner.Current; }
    //    }

    //    object IDbAsyncEnumerator.Current
    //    {
    //        get { return Current; }
    //    }
    //}


    //public class MockQuestionContextProvider : IQuestionContextProvider
    //{
    //    public IQuestionsDbContext DbContext
    //    {
    //        get
    //        {
    //            return new TestQuestionContext();
    //            //var context = new Mock<QuestionsDbContext>();

    //            //var data = new List<Category>
    //            //{
    //            //    //new Category { Id=1, Name = "BBB", UserId = 100, Version = 1 }, 
    //            //    //new Category { Id=2, Name = "ZZZ", UserId = 100, Version = 1 }, 
    //            //    //new Category { Id=3, Name = "AAA", UserId = 100, Version = 1 }, 
    //            //};

    //            //var dataQueryable = data.AsQueryable();

    //            //var mockSet = new Mock<DbSet<Category>>();
    //            //mockSet.As<IQueryable<Category>>().Setup(m => m.Provider).Returns(dataQueryable.Provider);
    //            //mockSet.As<IQueryable<Category>>().Setup(m => m.Expression).Returns(dataQueryable.Expression);
    //            //mockSet.As<IQueryable<Category>>().Setup(m => m.ElementType).Returns(dataQueryable.ElementType);
    //            //mockSet.As<IQueryable<Category>>().Setup(m => m.GetEnumerator()).Returns(dataQueryable.GetEnumerator());
    //            //mockSet.Setup(c => c.Add(It.IsAny<Category>())).Callback((Category cat) => data.Add(cat)).Returns((Category cat) => cat);
    //            //context.Setup(c => c.Set<Providers.Questions.Entities.Category>()).Returns(mockSet.Object);
    //            //return context.Object;
    //        }
    //    }
    //}

    //public class MockEventStoreContextProvider : IEventStoreContextProvider
    //{
    //    public IEventStoreContext DbContext
    //    {
    //        get
    //        {
    //            var context = new Mock<EventStoreContext>();

    //            var data = new List<Providers.EventStore.Entities.Event>
    //            {

    //            }.AsQueryable();

    //            var iDbSet = new Mock<IDbSet<Providers.EventStore.Entities.Event>>();

    //            var mockSet = new Mock<DbSet<Providers.EventStore.Entities.Event>>();
    //            mockSet.As<IQueryable<Providers.EventStore.Entities.Event>>().Setup(m => m.Provider).Returns(data.Provider);
    //            mockSet.As<IQueryable<Providers.EventStore.Entities.Event>>().Setup(m => m.Expression).Returns(data.Expression);
    //            mockSet.As<IQueryable<Providers.EventStore.Entities.Event>>().Setup(m => m.ElementType).Returns(data.ElementType);
    //            mockSet.As<IQueryable<Providers.EventStore.Entities.Event>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());


    //            context.Setup(c => c.Events).Returns(iDbSet.Object);
    //            //context.Setup(c => c.Set<Providers.EventStore.Entities.Event>()).Returns(mockSet.Object);
    //            return context.Object;
    //        }
    //    }
    //}
}



