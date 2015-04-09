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
using Crucial.Framework.Data.EntityFramework;
using Crucial.Providers.EventStore.Data;
using Crucial.Framework.IoC.StructureMapProvider;
using Crucial.Framework.Logging;
using Crucial.Providers.Questions.Data;
using StructureMap.Pipeline;

namespace API
{
    static class Bootstrapper
    {
        public static void BootstrapStructureMap()
        {
            DependencyResolver.Register(x =>
            {
                x.For(typeof(IContextProvider<>)).Use(typeof(ContextProvider<>));
                x.For(typeof(IRepository<>)).Use(typeof(Repository<>));
                x.For<IEventStorage>().Use<DatabaseEventStorage>();
                x.For<IEventBus>().Use<EventBus>();
                x.For<ICommandHandlerFactory>().Use<StructureMapCommandHandlerFactory>();
                x.For<IEventHandlerFactory>().Use<StructureMapEventHandlerFactory>();
                x.For<ICommandBus>().Use<CommandBus>();
                x.For<IEventBus>().Use<EventBus>();
                x.For<IQuestionsDbContext>().Use(() => new QuestionsDbContext());
                x.For<IEventStoreContext>().Use(() => new EventStoreContext());
                x.For<IQuestionManager>().Use<QuestionManager>();
                x.For<ICategoryManager>().Use<QuestionManager>();
                x.For<IStateHelper>().Use<StateHelper>();
                x.For<ILogger>().Use<CrucialLogger>();

                x.Scan(scanner =>
                {
                    scanner.AssemblyContainingType(typeof(CategoryRepositoryAsync));
                    scanner.WithDefaultConventions();
                });

                x.Scan(s =>
                {
                    s.AssemblyContainingType<Crucial.Qyz.CommandHandlers.UserCategoryNameChangeCommandHandler>();
                    s.AssemblyContainingType<API.Controllers.BroadcastUserCategoryCreatedEventHandler>();
                    s.ConnectImplementationsToTypesClosing(typeof(Crucial.Framework.DesignPatterns.CQRS.Commands.ICommandHandler<>));
                    s.ConnectImplementationsToTypesClosing(typeof(Crucial.Framework.DesignPatterns.CQRS.Events.IEventHandler<>));
                });
            });
        }
    }
}