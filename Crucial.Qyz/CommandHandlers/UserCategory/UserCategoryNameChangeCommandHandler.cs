using Crucial.Framework.DesignPatterns.CQRS.Commands;
using Crucial.Framework.DesignPatterns.CQRS.Storage;
using Crucial.Qyz.Commands.UserCategory;
using Crucial.Qyz.Domain;
using System;
using System.Threading.Tasks;


namespace Crucial.Qyz.CommandHandlers
{
    public class UserCategoryNameChangeCommandHandler : ICommandHandler<UserCategoryNameChangeCommand>
    {
        private readonly IRepository<UserCategory> _repository;

        public UserCategoryNameChangeCommandHandler(IRepository<UserCategory> repository)
        {
            _repository = repository;
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

            var aggregate = await _repository.GetById(command.Id).ConfigureAwait(false);

            if (aggregate.Name != command.Name)
                aggregate.ChangeName(command.Name);

            await Task.Run(() => _repository.Save(aggregate, command.Version)).ConfigureAwait(false);
        }
    }
}
