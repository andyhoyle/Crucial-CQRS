using Crucial.Framework.DesignPatterns.CQRS.Messaging;
using Crucial.Framework.DesignPatterns.CQRS.Storage;
using Crucial.Framework.DesignPatterns.CQRS.Utils;
using Crucial.Providers.Questions;
using Crucial.Services.Managers;
using Crucial.Services.Managers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StructureMap;
using Crucial.EventStore;
using Crucial.Providers.EventStore.Data;

namespace API
{
    static class Bootstrapper
    {
        public static void BootstrapStructureMap()
        {
            ObjectFactory.Initialize(x =>
            {
                x.For(typeof(IRepository<>)).Singleton().Use(typeof(Repository<>));
                x.For<IEventStorage>().Singleton().Use<DatabaseEventStorage>();
                x.For<IEventBus>().Use<EventBus>();
                x.For<ICommandHandlerFactory>().Use<StructureMapCommandHandlerFactory>();
                x.For<IEventHandlerFactory>().Use<StructureMapEventHandlerFactory>();
                x.For<ICommandBus>().Use<CommandBus>();
                x.For<IEventBus>().Use<EventBus>();
                x.For<IQuestionContextProvider>().Use<QuestionContextProvider>();
                x.For<IEventStoreContextProvider>().Use<EventStoreContextProvider>();
                x.For<IQuestionManager>().Use<QuestionManager>();
                x.For<ICategoryRepository>().Use<CategoryRepository>();
            });
        }
    }
}