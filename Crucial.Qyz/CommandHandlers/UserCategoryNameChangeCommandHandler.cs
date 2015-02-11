using Crucial.Framework.DesignPatterns.CQRS.Commands;
using Crucial.Framework.DesignPatterns.CQRS.Storage;
using Crucial.Qyz.Commands;
using Crucial.Qyz.Domain;
using System;


namespace Crucial.Qyz.CommandHandlers
{
    public class UserCategoryNameChangeCommandHandler : ICommandHandler<UserCategoryNameChangeCommand>
    {
        private readonly IRepository<UserCategory> _repository;

        public UserCategoryNameChangeCommandHandler(IRepository<UserCategory> repository)
        {
            _repository = repository;
        }

        public void Execute(UserCategoryNameChangeCommand command)
        {
            if (command == null)
            {
                throw new ArgumentNullException("command");
            }
            if (_repository == null)
            {
                throw new InvalidOperationException("Repository is not initialized.");
            }

            var aggregate = _repository.GetById(command.Id);

            if (aggregate.Name != command.Name)
                aggregate.ChangeName(command.Name);

            _repository.Save(aggregate, command.Version);
        }
    }
}
