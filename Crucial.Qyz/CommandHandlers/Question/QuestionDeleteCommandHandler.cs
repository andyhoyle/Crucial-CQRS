using Crucial.Framework.DesignPatterns.CQRS.Commands;
using Crucial.Framework.DesignPatterns.CQRS.Storage;
using Crucial.Qyz.Commands.Question;
using Crucial.Qyz.Domain;
using System;


namespace Crucial.Qyz.CommandHandlers
{
    public class QuestionDeleteCommandHandler : ICommandHandler<QuestionDeleteCommand>
    {
        private readonly IRepository<Question> _repository;

        public QuestionDeleteCommandHandler(IRepository<Question> repository)
        {
            _repository = repository;
        }

        public void Execute(QuestionDeleteCommand command)
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
