using Crucial.Framework.DesignPatterns.CQRS.Commands;
using Crucial.Framework.DesignPatterns.CQRS.Storage;
using Crucial.Qyz.Commands.UserCategory;
using Crucial.Qyz.Domain;
using System;
using System.Threading.Tasks;
using Crucial.Framework.Logging;


namespace Crucial.Qyz.CommandHandlers
{
    public class UserCategoryNameChangeCommandHandler : ICommandHandler<UserCategoryNameChangeCommand>
    {
        private readonly IRepository<UserCategory> _repository;
        private ILogger _logger;

        public UserCategoryNameChangeCommandHandler(IRepository<UserCategory> repository, ILogger logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task Execute(UserCategoryNameChangeCommand command)
        {
            if (command == null)
            {
                throw new ArgumentNullException("command");
            }
            if (_repository == null)
            {
                throw new InvalidOperationException("Repository is not initialized.");
            }

            _logger.Trace("UserCategoryNameChangeCommand", command);

            var aggregate = await _repository.GetById(command.Id).ConfigureAwait(false);

            if (aggregate.Name != command.Name)
                aggregate.ChangeName(command.Name);

            await Task.Run(() => _repository.Save(aggregate, command.Version)).ConfigureAwait(false);
        }
    }
}
