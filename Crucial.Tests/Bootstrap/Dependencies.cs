using Crucial.EventStore;
using Crucial.Framework.DesignPatterns.CQRS.Messaging;
using Crucial.Framework.DesignPatterns.CQRS.Storage;
using Crucial.Framework.DesignPatterns.CQRS.Utils;
using Crucial.Framework.IoC.StructureMapProvider;
using Crucial.Providers.Questions;
using Crucial.Qyz.Domain;
using Crucial.Services.Managers;
using Crucial.Services.Managers.Interfaces;
using Crucial.Tests.Mocks;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crucial.Tests.Bootstrap
{
    public static class Dependencies {
        public static void Setup()
        {
            DependencyResolver.Register(x =>
            {
                // Actual implementation tested by these tests
                x.For(typeof(IRepository<>)).Singleton().Use(typeof(Repository<>));
                x.For<IEventBus>().Use<EventBus>();
                x.For<ICommandHandlerFactory>().Use<StructureMapCommandHandlerFactory>();
                x.For<IEventHandlerFactory>().Use<StructureMapEventHandlerFactory>();
                x.For<ICommandBus>().Use<CommandBus>();
                x.For<IEventBus>().Use<EventBus>();
                x.For<IQuestionManager>().Use<QuestionManager>();
                x.For<ICategoryRepository>().Use<CategoryRepository>();
                x.For<IEventStorage>().Singleton().Use<DatabaseEventStorage>();
                
                // Mocks
                x.For<IEventStoreContextProvider>().Use<MockEventStoreContextProvider>();
                x.For<IQuestionContextProvider>().Use<MockQuestionContextProvider>();
            });
        }
    }
}
