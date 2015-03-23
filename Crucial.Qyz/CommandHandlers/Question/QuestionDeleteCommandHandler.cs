using Crucial.Framework.DesignPatterns.CQRS.Commands;
using Crucial.Framework.DesignPatterns.CQRS.Storage;
using Crucial.Qyz.Commands.Question;
using Crucial.Qyz.Domain;
using System;
using System.Threading.Tasks;


namespace Crucial.Qyz.CommandHandlers
{
    public class QuestionDeleteCommandHandler : ICommandHandler<QuestionDeleteCommand>
    {
        private readonly IRepository<Question> _repository;

        public QuestionDeleteCommandHandler(IRepository<Question> repository)
        {
            _repository = repository;
        }

        public async Task Execute(QuestionDeleteCommand command)
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

            aggregate.Delete();

            await _repository.Save(aggregate, command.Version).ConfigureAwait(false);
        }
    }
}
