using Crucial.Framework.DesignPatterns.CQRS.Commands;
using Crucial.Framework.DesignPatterns.CQRS.Storage;
using Crucial.Qyz.Commands.Question;
using Crucial.Qyz.Domain;
using System;
using System.Threading.Tasks;


namespace Crucial.Qyz.CommandHandlers
{
    public class QuestionTextChangeCommandHandler : ICommandHandler<QuestionTextChangeCommand>
    {
        private readonly IRepository<Question> _repository;

        public QuestionTextChangeCommandHandler(IRepository<Question> repository)
        {
            _repository = repository;
        }

        public async Task Execute(QuestionTextChangeCommand command)
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

            if (aggregate.QuestionText != command.QuestionText)
                aggregate.ChangeQuestionText(command.QuestionText);

            await Task.Run(() => _repository.Save(aggregate, command.Version)).ConfigureAwait(false);
        }
    }
}
