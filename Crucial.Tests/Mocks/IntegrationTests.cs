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

namespace Crucial.Tests.Mocks
{
    /*
    public class FakeDbSet<T> : IDbSet<T>
    where T : class
    {
        HashSet<T> _data;
        IQueryable _query;

        public FakeDbSet()
        {
            _data = new HashSet<T>();
            _query = _data.AsQueryable();
        }

        public virtual T Find(params object[] keyValues)
        {
            throw new NotImplementedException("Derive from FakeDbSet<T> and override Find");
        }
        public void Add(T item)
        {
            _data.Add(item);
        }

        public void Remove(T item)
        {
            _data.Remove(item);
        }

        public void Attach(T item)
        {
            _data.Add(item);
        }
        public void Detach(T item)
        {
            _data.Remove(item);
        }
        Type IQueryable.ElementType
        {
            get { return _query.ElementType; }
        }
        System.Linq.Expressions.Expression IQueryable.Expression
        {
            get { return _query.Expression; }
        }

        IQueryProvider IQueryable.Provider
        {
            get { return _query.Provider; }
        }
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return _data.GetEnumerator();
        }
        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return _data.GetEnumerator();
        }
    }
    */
    public class MockQuestionContextProvider : IQuestionContextProvider
    {
        public DbContext DbContext
        {
            get {
                var context = new Mock<QuestionsDbContext>();

                var data = new List<Category> 
                { 
                    new Category { Id=1, Name = "BBB", UserId = 100, Version = 1 }, 
                    new Category { Id=2, Name = "ZZZ", UserId = 100, Version = 1 }, 
                    new Category { Id=3, Name = "AAA", UserId = 100, Version = 1 }, 
                }.AsQueryable();

                var mockSet = new Mock<DbSet<Category>>();
                mockSet.As<IQueryable<Category>>().Setup(m => m.Provider).Returns(data.Provider);
                mockSet.As<IQueryable<Category>>().Setup(m => m.Expression).Returns(data.Expression);
                mockSet.As<IQueryable<Category>>().Setup(m => m.ElementType).Returns(data.ElementType);
                mockSet.As<IQueryable<Category>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

                context.Setup(c => c.Set<Providers.Questions.Entities.Category>()).Returns(mockSet.Object);
                return context.Object;
            }
        }
    }

    public class MockEventStoreContextProvider : IEventStoreContextProvider
    {
        public DbContext DbContext
        {
            get
            {
                var context = new Mock<EventStoreContext>();

                var data = new List<Providers.EventStore.Entities.Event> 
                { 
                    
                }.AsQueryable();

                var mockSet = new Mock<DbSet<Providers.EventStore.Entities.Event>>();
                mockSet.As<IQueryable<Providers.EventStore.Entities.Event>>().Setup(m => m.Provider).Returns(data.Provider);
                mockSet.As<IQueryable<Providers.EventStore.Entities.Event>>().Setup(m => m.Expression).Returns(data.Expression);
                mockSet.As<IQueryable<Providers.EventStore.Entities.Event>>().Setup(m => m.ElementType).Returns(data.ElementType);
                mockSet.As<IQueryable<Providers.EventStore.Entities.Event>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

                context.Setup(c => c.Set<Providers.EventStore.Entities.Event>()).Returns(mockSet.Object);
                return context.Object;
            }
        }
    }

}
