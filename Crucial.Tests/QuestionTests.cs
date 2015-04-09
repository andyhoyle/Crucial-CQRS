using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Crucial.Tests.Bootstrap;
using Crucial.Providers.Questions;
using Crucial.Framework.DesignPatterns.CQRS.Messaging;
using System.Linq;
using Crucial.Framework.IoC.StructureMapProvider;
using System.Threading.Tasks;
using Crucial.Framework.DesignPatterns.Repository.Async.Extensions;
using Crucial.Providers.EventStore;
using Crucial.Providers.EventStore.Data;
using Crucial.Qyz.Commands.Question;
using NUnit.Framework;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace Crucial.Tests
{
    [TestClass]
    public class QuestionTests
    {
        public QuestionTests()
        {
            Dependencies.Setup();
        }
        
        IQuestionRepositoryAsync _questionRepo;
        IAggregateRepositoryAsync _aggregateRepo;
        ICommandBus _commandBus;
        IEventBus _eventBus;

        [TestInitialize]
        public async void Setup()
        {
            _questionRepo = DependencyResolver.Container.GetInstance<IQuestionRepositoryAsync>();
            _commandBus = DependencyResolver.Container.GetInstance<ICommandBus>();
            _eventBus = DependencyResolver.Container.GetInstance<IEventBus>();
            _aggregateRepo = DependencyResolver.Container.GetInstance<IAggregateRepositoryAsync>();

            await _questionRepo.GetAllAsync();
            await _aggregateRepo.GetAllAsync();
        }

        [TestMethod]
        public async Task QuestionCreateCommandTriggersEventToUpdateQueryDbWithQuestion()
        {
            var questionText = "Test Question 1";

            //Arrange
            var command = new QuestionCreateCommand(questionText);
            await _commandBus.Send(command);

            //Act
            var questions = await _questionRepo.FindByAsync(q => q.Id == 1).ConfigureAwait(false);
            var question = questions.FirstOrDefault();

            //Act
            Assert.IsNotNull(question);
            Assert.AreEqual(questionText, question.Text);
        }
        
        [TestMethod]
        public async Task QuestionUpdateCommandTriggersEventToUpdateQueryDbWithQuestion()
        {
            //Arrange
            var command = new QuestionCreateCommand("Test Question 1");
            var c1 = _commandBus.Send(command);
            
            var command2 = new QuestionTextChangeCommand(1, "Question Updated", 0);
            var c2 = _commandBus.Send(command2);

            await c1;
            await c2;

            //Act
            var questions = await _questionRepo.FindByAsync(q => q.Id == 1).ConfigureAwait(false);
            var question = questions.FirstOrDefault();

            //Act
            Assert.IsNotNull(question);
            Assert.AreEqual("Question Updated", question.Text);
        }

        [TestMethod]
        public async Task QuestionDeleteCommandTriggersEventToDeleteQuestionFromQueryDb()
        {
            //Arrange
            var command = new QuestionCreateCommand("Test Question 1");
            await _commandBus.Send(command);

            var questions = await _questionRepo.FindByAsync(q => q.Id == 1).ConfigureAwait(false);
            var question = questions.FirstOrDefault();

            //Act
            Assert.IsNotNull(question);

            var command2 = new QuestionDeleteCommand(1, 0);
            await _commandBus.Send(command2).ConfigureAwait(false);

            var questionAfterDelete = (await _questionRepo.FindByAsync(q => q.Id == 1).ConfigureAwait(false)).FirstOrDefault();

            //Act
            Assert.IsNull(questionAfterDelete);
        }
    }
}
