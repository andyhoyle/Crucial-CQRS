using Crucial.Framework.DesignPatterns.CQRS.Commands;
using Crucial.Framework.DesignPatterns.CQRS.Storage;
using Crucial.Qyz.Commands.UserCategory;
using Crucial.Qyz.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crucial.Qyz.CommandHandlers
{
    public class UserCategoryCreateCommandHandler : ICommandHandler<UserCategoryCreateCommand>
    {
        private IRepository<UserCategory> _repository;

        public UserCategoryCreateCommandHandler(IRepository<UserCategory> repository)
        {
            _repository = repository;
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

            var aggregate = new UserCategory(command.Id, command.Name);
            aggregate.Version = -1;
            await Task.Run(() => _repository.Save(aggregate, command.Version)).ConfigureAwait(false);
        }
    }
}
