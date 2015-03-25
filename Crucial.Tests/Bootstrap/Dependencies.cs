using Crucial.EventStore;
using Crucial.Framework.DesignPatterns.CQRS.Messaging;
using Crucial.Framework.DesignPatterns.CQRS.Storage;
using Crucial.Framework.DesignPatterns.CQRS.Utils;
using Crucial.Framework.IoC.StructureMapProvider;
using Crucial.Providers.EventStore.Data;
using Crucial.Providers.Questions;
using Crucial.Providers.Questions.Data;
using Crucial.Qyz.Domain;
using Crucial.Services.Managers;
using Crucial.Services.Managers.Interfaces;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StructureMap.Graph;
using StructureMap.Configuration.DSL;
using Crucial.Framework.DesignPatterns.CQRS.Commands;
using Crucial.Qyz.CommandHandlers;
using Crucial.Qyz.Commands;
using Crucial.Providers.EventStore;
using System.Data.Common;
using Crucial.Framework.Data.EntityFramework;
using Crucial.Framework.Logging;

namespace Crucial.Tests.Bootstrap
{
    public static class Dependencies
    {
        public static void Setup()
        {
            DependencyResolver.Register(x =>
            {
                // Actual implementation tested by these tests
                x.For(typeof(IContextProvider<>)).Use(typeof(ContextProvider<>));
                x.For(typeof(IRepository<>)).Singleton().Use(typeof(Repository<>));
                x.For<IEventBus>().Use<EventBus>();
                x.For<ICommandHandlerFactory>().Use<StructureMapCommandHandlerFactory>();
                x.For<IEventHandlerFactory>().Use<StructureMapEventHandlerFactory>();
                x.For<ICommandBus>().Use<CommandBus>();
                x.For<IEventBus>().Use<EventBus>();
                x.For<IQuestionManager>().Use<QuestionManager>();
                x.For<ICategoryRepositoryAsync>().Use<CategoryRepositoryAsync>();
                x.For<IQuestionRepositoryAsync>().Use<QuestionRepositoryAsync>();
                x.For<IEventRepositoryAsync>().Use<EventRepositoryAsync>();
                x.For<IAggregateRepositoryAsync>().Use<AggregateRepositoryAsync>();
                x.For<IMementoRepositoryAsync>().Use<MementoRepositoryAsync>();
                x.For<IEventStorage>().Singleton().Use<DatabaseEventStorage>();
                x.For<IStateHelper>().Use<StateHelper>();
                x.For<ILogger>().Use<CrucialLogger>();

                x.Scan(s =>
                {
                    s.AssemblyContainingType<Crucial.Qyz.CommandHandlers.UserCategoryNameChangeCommandHandler>();
                    s.ConnectImplementationsToTypesClosing(typeof(Crucial.Framework.DesignPatterns.CQRS.Commands.ICommandHandler<>));
                    s.ConnectImplementationsToTypesClosing(typeof(Crucial.Framework.DesignPatterns.CQRS.Events.IEventHandler<>));
                });

                // Mocks
                var testEventStore = Effort.DbConnectionFactory.CreateTransient();
                var testQuestionsDb = Effort.DbConnectionFactory.CreateTransient();

                x.For<IEventStoreContext>().Singleton().Use(() => new EventStoreContext(testEventStore));
                x.For<IQuestionsDbContext>().Singleton().Use(() => new QuestionsDbContext(testQuestionsDb));
            });
        }
    }
}
