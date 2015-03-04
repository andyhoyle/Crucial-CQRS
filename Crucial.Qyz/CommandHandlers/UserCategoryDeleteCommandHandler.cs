using Crucial.Framework.DesignPatterns.CQRS.Commands;
using Crucial.Framework.DesignPatterns.CQRS.Storage;
using Crucial.Qyz.Commands;
using Crucial.Qyz.Domain;
using System;


namespace Crucial.Qyz.CommandHandlers
{
    public class UserCategoryDeleteCommandHandler : ICommandHandler<UserCategoryDeleteCommand>
    {
        private readonly IRepository<UserCategory> _repository;

        public UserCategoryDeleteCommandHandler(IRepository<UserCategory> repository)
        {
            _repository = repository;
        }

        public void Execute(UserCategoryDeleteCommand command)
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

            aggregate.Delete();

            _repository.Save(aggregate, command.Version);
        }
    }
}
