using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Crucial.Providers.Questions.Data;
using Crucial.Services.Managers.Interfaces;
using Crucial.Tests.Bootstrap;
using Crucial.Providers.Questions;
using StructureMap;
using Crucial.Framework.DesignPatterns.CQRS.Messaging;
using Crucial.Qyz.Commands.UserCategory;
using System.Linq;
using Crucial.Framework.IoC.StructureMapProvider;
using Crucial.Framework.Testing.EF;
using Crucial.Providers.Questions.Entities;
using Crucial.Providers.EventStore.Data;
using Crucial.Providers.EventStore.Entities;
using Crucial.Qyz.Events;
using Crucial.EventStore;
using Crucial.Qyz.Domain;
using Crucial.Framework.DesignPatterns.CQRS.Storage;
using Crucial.Qyz.Domain.Mementos;
using Crucial.Framework.DesignPatterns.CQRS.Utils;
using System.Threading.Tasks;
using Crucial.Framework.DesignPatterns.Repository.Async.Extensions;

namespace Crucial.Tests
{
    [TestClass]
    public class CategoryTests
    {
        public CategoryTests()
        {
            Dependencies.Setup();
        }
        
        ICategoryRepositoryAsync _categoryRepo;
        ICommandBus _commandBus;
        IEventBus _eventBus;

        [TestInitialize]
        public void Setup()
        {
            _categoryRepo = DependencyResolver.Container.GetInstance<ICategoryRepositoryAsync>();
            _commandBus = DependencyResolver.Container.GetInstance<ICommandBus>();
            _eventBus = DependencyResolver.Container.GetInstance<IEventBus>();
        }

        [TestMethod]
        public async Task UserCategoryCreateCommandTriggersEventToUpdateQueryDbWithCategory()
        {
            var name = "Test Category 1";

            //Arrange
            var command = new UserCategoryCreateCommand(name);
            await _commandBus.Send(command);

            //Act
            var cats = await _categoryRepo.FindByAsync(q => q.Id == 1).ConfigureAwait(false);
            var cat = cats.FirstOrDefault();

            //Act
            Assert.AreEqual(cat.Name, name);
        }

        [TestMethod]
        public async Task UserCategoryCreateCommandStoresEventInEventStore()
        {
            //Arrange
            var catName = "Test Category 345";
            IEventStoreContext eContext = Crucial.Framework.IoC.StructureMapProvider.DependencyResolver.Container.GetInstance<IEventStoreContext>();
            var command = new UserCategoryCreateCommand(catName);

            //Act
            await _commandBus.Send(command);
            var e = eContext.Events.Where(x => x.AggregateId == 1).FirstOrDefault();

            //Assert
            Assert.IsNotNull(e);
            var desrializedEvent = (UserCategoryCreatedEvent)DatabaseEventStorage.DeSerialize<UserCategoryCreatedEvent>(e.Data);
            Assert.IsNotNull(desrializedEvent);
            Assert.AreEqual(catName, desrializedEvent.Name);
        }

        [TestMethod]
        public async Task UserCategoryUpdateCommandTriggersEventToUpdateQueryDbWithNewCategoryName()
        {
            //Arrange
            string newName = "NewCategoryNameForCategory2";

            IQuestionsDbContext qContext = Crucial.Framework.IoC.StructureMapProvider.DependencyResolver.Container.GetInstance<IQuestionsDbContext>();
            qContext.Categories.Add(new Category { Id = 1, Name = "Category1", Version = 0 });
            qContext.Categories.Add(new Category { Id = 2, Name = "Category2", Version = 0 });
            qContext.Categories.Add(new Category { Id = 3, Name = "Category3", Version = 0 });

            qContext.SaveChanges();

            IEventStoreContext eContext = Crucial.Framework.IoC.StructureMapProvider.DependencyResolver.Container.GetInstance<IEventStoreContext>();
            UserCategoryCreatedEvent e = new UserCategoryCreatedEvent(1, "Category2", DateTime.UtcNow);
            eContext.Events.Add(new Event { Id = 2, AggregateId = 1, Data = DatabaseEventStorage.Serialize<UserCategoryCreatedEvent>(e) });
            
            eContext.AggregateRoots.Add(new AggregateRoot { EventVersion = 0, Id = 1 });

            eContext.SaveChanges();

            //Act
            await _commandBus.Send(new UserCategoryNameChangeCommand(1, newName, 0));
            var cats = await _categoryRepo.FindByAsync(q => q.Id == 1).ConfigureAwait(false);

            var cat = cats.FirstOrDefault();

            //Assert
            Assert.AreEqual(newName, cat.Name);
            Assert.AreEqual(1, cat.Version);
        }

        [TestMethod]
        public async void UserCategoryCreateCommandStoresAggregateRootInEventStore()
        {
            //Arrange
            var catName = "Test Category 345";
            IEventStoreContext eContext = Crucial.Framework.IoC.StructureMapProvider.DependencyResolver.Container.GetInstance<IEventStoreContext>();
            var command = new UserCategoryCreateCommand(catName);

            //Act
            await _commandBus.Send(command);
            var agg = eContext.AggregateRoots.Where(x => x.Id == 1).FirstOrDefault();

            //Assert
            Assert.IsNotNull(agg);
            Assert.AreEqual(0, agg.Version);
        }

        [TestMethod]
        public async Task UserCategoryAggregateRootCanBeConstructedFromMemento()
        {
            IEventStoreContext eContext = Crucial.Framework.IoC.StructureMapProvider.DependencyResolver.Container.GetInstance<IEventStoreContext>();
            // Arrange
            // No version - it's new
            var e1 = new UserCategoryCreateCommand("Category Name");
            // Acting on Version 0
            var e2 = new UserCategoryNameChangeCommand(1, "Category Name Changed Once", 0);
            // Acting on Version 1
            var e3 = new UserCategoryNameChangeCommand(1, "Category Name Changed Twice", 1);
            // Acting on Version 2
            var e4 = new UserCategoryNameChangeCommand(1, "Category Name Changed Three Times", 2);
            // Acting on Version 3
            var e5 = new UserCategoryNameChangeCommand(1, "Category Name Changed Four Times", 3);

            // Act
            await _commandBus.Send<UserCategoryCreateCommand>(e1);
            await _commandBus.Send<UserCategoryNameChangeCommand>(e2);
            await _commandBus.Send<UserCategoryNameChangeCommand>(e3);
            await _commandBus.Send<UserCategoryNameChangeCommand>(e4);
            await _commandBus.Send<UserCategoryNameChangeCommand>(e5);

 
            // Assert
            var m = eContext.BaseMementoes.Where(x => x.Id == 1).FirstOrDefault();
            Assert.IsNotNull(m);
            var memento = DatabaseEventStorage.DeSerialize<UserCategoryMemento>(m.Data);
            UserCategory c = new UserCategory();
            c.SetMemento(memento);
            Assert.AreEqual("Category Name Changed Three Times", c.Name);
        }

        [TestMethod]
        public async Task UserCategoriesCanBeReadFromHistoryWithoutMementos()
        {
            IEventStoreContext eContext = Framework.IoC.StructureMapProvider.DependencyResolver.Container.GetInstance<IEventStoreContext>();
            IRepository<UserCategory> ucr = Framework.IoC.StructureMapProvider.DependencyResolver.Container.GetInstance<IRepository<UserCategory>>();

            eContext.AggregateRoots.Add(new AggregateRoot { EventVersion = 0, Version = 0, Id = 1 });
            eContext.AggregateRoots.Add(new AggregateRoot { EventVersion = 0, Version = 0, Id = 2 });

            UserCategoryCreatedEvent e = new UserCategoryCreatedEvent(1, "Category 1", DateTime.UtcNow);
            eContext.Events.Add(new Event { Id = 1, AggregateId = 1, Data = DatabaseEventStorage.Serialize<UserCategoryCreatedEvent>(e) });

            UserCategoryCreatedEvent e2 = new UserCategoryCreatedEvent(2, "Category 2", DateTime.UtcNow);
            eContext.Events.Add(new Event { Id = 2, AggregateId = 2, Data = DatabaseEventStorage.Serialize<UserCategoryCreatedEvent>(e2) });

            UserCategoryNameChangedEvent e3 = new UserCategoryNameChangedEvent(2, "Category 2 Renamed", 0, DateTime.UtcNow);
            eContext.Events.Add(new Event { Id = 3, AggregateId = 2, Data = DatabaseEventStorage.Serialize<UserCategoryNameChangedEvent>(e3) });

            eContext.SaveChanges();

            var uc1 = await ucr.GetById(1).ConfigureAwait(false);
            var uc2 = await ucr.GetById(2).ConfigureAwait(false);

            Assert.AreEqual("Category 1", uc1.Name);
            Assert.AreEqual("Category 2 Renamed", uc2.Name);
        }

        [TestMethod]
        public async Task UserCategoriesCanBeReadFromHistoryWithMementos()
        {
            IEventStoreContext eContext = Framework.IoC.StructureMapProvider.DependencyResolver.Container.GetInstance<IEventStoreContext>();
            IRepository<UserCategory> ucr = Framework.IoC.StructureMapProvider.DependencyResolver.Container.GetInstance<IRepository<UserCategory>>();

            eContext.AggregateRoots.Add(new AggregateRoot { EventVersion = 0, Version = 0, Id = 1 });

            UserCategoryCreatedEvent e2 = new UserCategoryCreatedEvent(2, "Category 2", DateTime.UtcNow);
            eContext.Events.Add(new Event { Id = 2, AggregateId = 1, Data = DatabaseEventStorage.Serialize<UserCategoryCreatedEvent>(e2) });

            UserCategoryNameChangedEvent e3 = new UserCategoryNameChangedEvent(1, "Category 2 Renamed Once", 0, DateTime.UtcNow);
            eContext.Events.Add(new Event { Id = 3, AggregateId = 1, Data = DatabaseEventStorage.Serialize<UserCategoryNameChangedEvent>(e3) });

            UserCategoryNameChangedEvent e4 = new UserCategoryNameChangedEvent(1, "Category 2 Renamed Twice", 1, DateTime.UtcNow);
            eContext.Events.Add(new Event { Id = 4, AggregateId = 1, Data = DatabaseEventStorage.Serialize<UserCategoryNameChangedEvent>(e4) });

            UserCategoryNameChangedEvent e5 = new UserCategoryNameChangedEvent(1, "Category 2 Renamed Three Times", 2, DateTime.UtcNow);
            eContext.Events.Add(new Event { Id = 5, AggregateId = 1, Data = DatabaseEventStorage.Serialize<UserCategoryNameChangedEvent>(e5) });

            UserCategoryNameChangedEvent e6 = new UserCategoryNameChangedEvent(1, "Category 2 Renamed Four Times", 3, DateTime.UtcNow);
            eContext.Events.Add(new Event { Id = 6, AggregateId = 1, Data = DatabaseEventStorage.Serialize<UserCategoryNameChangedEvent>(e6) });

            UserCategoryNameChangedEvent e7 = new UserCategoryNameChangedEvent(1, "Category 2 Renamed Five Times", 4, DateTime.UtcNow);
            eContext.Events.Add(new Event { Id = 6, AggregateId = 1, Data = DatabaseEventStorage.Serialize<UserCategoryNameChangedEvent>(e7) });

            UserCategoryMemento memento = new UserCategoryMemento(1, "Category 2 Renamed Three Times", 3);
            eContext.BaseMementoes.Add(new BaseMemento { Id = 1, Version = 3, Data = DatabaseEventStorage.Serialize<UserCategoryMemento>(memento) });

            eContext.SaveChanges();

            var uc2 = await ucr.GetById(1).ConfigureAwait(false);

            Assert.AreEqual("Category 2 Renamed Five Times", uc2.Name);
        }

        [TestMethod]
        public async Task UserCategoriesFromHistoryCanPopulateQueryDb()
        {

            IEventStoreContext eContext = Framework.IoC.StructureMapProvider.DependencyResolver.Container.GetInstance<IEventStoreContext>();
            IRepository<UserCategory> ucr = Framework.IoC.StructureMapProvider.DependencyResolver.Container.GetInstance<IRepository<UserCategory>>();
            var _eventHandlerFactory = Framework.IoC.StructureMapProvider.DependencyResolver.Container.GetInstance<IEventHandlerFactory>();

            eContext.AggregateRoots.Add(new AggregateRoot { EventVersion = 0, Version = 0, Id = 1 });

            UserCategoryCreatedEvent e2 = new UserCategoryCreatedEvent(1, "Category 1", DateTime.UtcNow);
            eContext.Events.Add(new Event { Id = 2, AggregateId = 1, Data = DatabaseEventStorage.Serialize<UserCategoryCreatedEvent>(e2) });

            UserCategoryNameChangedEvent e3 = new UserCategoryNameChangedEvent(1, "Category 1 Renamed Once", 0, DateTime.UtcNow);
            eContext.Events.Add(new Event { Id = 3, AggregateId = 1, Data = DatabaseEventStorage.Serialize<UserCategoryNameChangedEvent>(e3) });

            UserCategoryNameChangedEvent e4 = new UserCategoryNameChangedEvent(1, "Category 1 Renamed Twice", 1, DateTime.UtcNow);
            eContext.Events.Add(new Event { Id = 4, AggregateId = 1, Data = DatabaseEventStorage.Serialize<UserCategoryNameChangedEvent>(e4) });

            UserCategoryNameChangedEvent e5 = new UserCategoryNameChangedEvent(1, "Category 1 Renamed Three Times", 2, DateTime.UtcNow);
            eContext.Events.Add(new Event { Id = 5, AggregateId = 1, Data = DatabaseEventStorage.Serialize<UserCategoryNameChangedEvent>(e5) });

            UserCategoryNameChangedEvent e6 = new UserCategoryNameChangedEvent(1, "Category 1 Renamed Four Times", 3, DateTime.UtcNow);
            eContext.Events.Add(new Event { Id = 6, AggregateId = 1, Data = DatabaseEventStorage.Serialize<UserCategoryNameChangedEvent>(e6) });

            UserCategoryNameChangedEvent e7 = new UserCategoryNameChangedEvent(1, "Category 1 Renamed Five Times", 4, DateTime.UtcNow);
            eContext.Events.Add(new Event { Id = 7, AggregateId = 1, Data = DatabaseEventStorage.Serialize<UserCategoryNameChangedEvent>(e7) });

            eContext.SaveChanges();

            foreach (var @event in eContext.Events.ToList())
            {
                var e = DatabaseEventStorage.DeSerialize(@event.Data);
                
                if (e is UserCategoryNameChangedEvent)
                {
                    var handlers = _eventHandlerFactory.GetHandlers<UserCategoryNameChangedEvent>();
                    foreach (var eventHandler in handlers)
                    {
                        eventHandler.Handle(e);
                    }
                }

                if (e is UserCategoryCreatedEvent)
                {
                    var handlers = _eventHandlerFactory.GetHandlers<UserCategoryCreatedEvent>();
                    foreach (var eventHandler in handlers)
                    {
                        eventHandler.Handle(e);
                    }
                }
            }

            var cats = await _categoryRepo.FindByAsync(q => q.Id == 1).ConfigureAwait(false);

            var cat = cats.FirstOrDefault();

            Assert.AreEqual("Category 1 Renamed Five Times", cat.Name);
        }

        [TestMethod]
        public async Task UserCategoriesFromHistoryCanPopulateQueryDbUsingGenericHandlerResolver()
        {

            IEventStoreContext eContext = Framework.IoC.StructureMapProvider.DependencyResolver.Container.GetInstance<IEventStoreContext>();
            IRepository<UserCategory> ucr = Framework.IoC.StructureMapProvider.DependencyResolver.Container.GetInstance<IRepository<UserCategory>>();
            IStateHelper sh = Crucial.Framework.IoC.StructureMapProvider.DependencyResolver.Container.GetInstance<IStateHelper>();

            eContext.AggregateRoots.Add(new AggregateRoot { EventVersion = 0, Version = 0, Id = 2 });

            UserCategoryCreatedEvent e2 = new UserCategoryCreatedEvent(2, "Category 2", DateTime.UtcNow);
            eContext.Events.Add(new Event { Id = 2, AggregateId = 2, Data = DatabaseEventStorage.Serialize<UserCategoryCreatedEvent>(e2) });

            UserCategoryNameChangedEvent e3 = new UserCategoryNameChangedEvent(2, "Category 2 Renamed Once", 0, DateTime.UtcNow);
            eContext.Events.Add(new Event { Id = 3, AggregateId = 2, Data = DatabaseEventStorage.Serialize<UserCategoryNameChangedEvent>(e3) });

            UserCategoryNameChangedEvent e4 = new UserCategoryNameChangedEvent(2, "Category 2 Renamed Twice", 1, DateTime.UtcNow);
            eContext.Events.Add(new Event { Id = 4, AggregateId = 2, Data = DatabaseEventStorage.Serialize<UserCategoryNameChangedEvent>(e4) });

            UserCategoryNameChangedEvent e5 = new UserCategoryNameChangedEvent(2, "Category 2 Renamed Three Times", 2, DateTime.UtcNow);
            eContext.Events.Add(new Event { Id = 5, AggregateId = 2, Data = DatabaseEventStorage.Serialize<UserCategoryNameChangedEvent>(e5) });

            UserCategoryNameChangedEvent e6 = new UserCategoryNameChangedEvent(2, "Category 2 Renamed Four Times", 3, DateTime.UtcNow);
            eContext.Events.Add(new Event { Id = 6, AggregateId = 2, Data = DatabaseEventStorage.Serialize<UserCategoryNameChangedEvent>(e6) });

            UserCategoryNameChangedEvent e7 = new UserCategoryNameChangedEvent(2, "Category 2 Renamed Five Times", 4, DateTime.UtcNow);
            eContext.Events.Add(new Event { Id = 6, AggregateId = 2, Data = DatabaseEventStorage.Serialize<UserCategoryNameChangedEvent>(e7) });

            eContext.SaveChanges();

            await sh.RestoreState();

            var cats = await _categoryRepo.FindByAsync(q => q.Id == 2).ConfigureAwait(false);
            var cat = cats.FirstOrDefault();
            Assert.AreEqual("Category 2 Renamed Five Times", cat.Name);
        }
    }
}
