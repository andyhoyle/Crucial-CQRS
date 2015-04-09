using Crucial.Framework.DesignPatterns.CQRS.Commands;
using Crucial.Framework.DesignPatterns.CQRS.Storage;
using Crucial.Qyz.Commands.UserCategory;
using Crucial.Qyz.Domain;
using System;
using System.Threading.Tasks;
using Crucial.Framework.Logging;


namespace Crucial.Qyz.CommandHandlers
{
    public class AddQuestionToCategoryCommandHandler : ICommandHandler<AddQuestionToCategoryCommand>
    {
        private readonly IRepository<UserCategory> _repository;
        private readonly ILogger _logger;

        public AddQuestionToCategoryCommandHandler(IRepository<UserCategory> repository, ILogger logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task Execute(AddQuestionToCategoryCommand command)
        {
            if (command == null)
            {
                throw new ArgumentNullException("command");
            }
            if (_repository == null)
            {
                throw new InvalidOperationException("Repository is not initialized.");
            }

            _logger.Trace("AddQuestionToCategoryCommand", command);

            var aggregate = await _repository.GetById(command.Id).ConfigureAwait(false);

            if (!aggregate.Questions.Contains(command.Id))
                aggregate.AddQuestionToCategory(command.QuestionId);

            await Task.Run(() => _repository.Save(aggregate, command.Version)).ConfigureAwait(false);
        }
    }
}
