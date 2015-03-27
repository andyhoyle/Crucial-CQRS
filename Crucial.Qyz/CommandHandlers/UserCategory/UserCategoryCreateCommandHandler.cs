using Crucial.Framework.DesignPatterns.CQRS.Commands;
using Crucial.Framework.DesignPatterns.CQRS.Storage;
using Crucial.Qyz.Commands.UserCategory;
using Crucial.Qyz.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Crucial.Framework.Logging;

namespace Crucial.Qyz.CommandHandlers
{
    public class UserCategoryCreateCommandHandler : ICommandHandler<UserCategoryCreateCommand>
    {
        private IRepository<UserCategory> _repository;
        private ILogger _logger;

        public UserCategoryCreateCommandHandler(IRepository<UserCategory> repository, ILogger logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task Execute(UserCategoryCreateCommand command)
        {
            if (command == null)
            {
                throw new ArgumentNullException("command");
            }
            
            if (_repository == null)
            {
                throw new InvalidOperationException("Repository is not initialized.");
            }

            _logger.Trace("UserCategoryCreateCommand", command);

            var aggregate = new UserCategory(command.Id, command.Name);
            aggregate.Version = -1;
            await Task.Run(() => _repository.Save(aggregate, command.Version)).ConfigureAwait(false);
        }
    }
}
