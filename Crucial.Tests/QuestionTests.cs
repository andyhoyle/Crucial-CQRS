using Microsoft.VisualStudio.TestTools.UnitTesting;
using Crucial.Tests.Bootstrap;
using Crucial.Providers.Questions;
using Crucial.Framework.DesignPatterns.CQRS.Messaging;
using System.Linq;
using Crucial.Framework.IoC.StructureMapProvider;
using System.Threading.Tasks;
using Crucial.Framework.DesignPatterns.Repository.Async.Extensions;
using Crucial.Qyz.Commands.Question;

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
        ICommandBus _commandBus;
        IEventBus _eventBus;

        [TestInitialize]
        public void Setup()
        {
            _questionRepo = DependencyResolver.Container.GetInstance<IQuestionRepositoryAsync>();
            _commandBus = DependencyResolver.Container.GetInstance<ICommandBus>();
            _eventBus = DependencyResolver.Container.GetInstance<IEventBus>();
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
            await _commandBus.Send(command);
            
            var command2 = new QuestionTextChangeCommand(1, "Question Updated", 0);
            await _commandBus.Send(command2);

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
            await _commandBus.Send(command2);

            var questionsAfterDelete = await _questionRepo.FindByAsync(q => q.Id == 1).ConfigureAwait(false);
            var questionAfterDelete = questionsAfterDelete.FirstOrDefault();

            //Act
            Assert.IsNull(questionAfterDelete);
        }
    }
}
