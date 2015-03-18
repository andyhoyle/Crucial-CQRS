using Crucial.Framework.DesignPatterns.CQRS.Commands;
using Crucial.Framework.DesignPatterns.CQRS.Storage;
using Crucial.Qyz.Commands.Question;
using Crucial.Qyz.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crucial.Qyz.CommandHandlers
{
    public class QuestionCreateCommandHandler : ICommandHandler<QuestionCreateCommand>
    {
        private IRepository<Question> _repository;

        public QuestionCreateCommandHandler(IRepository<Question> repository)
        {
            _repository = repository;
        }

        public void Execute(QuestionCreateCommand command)
        {
            if (command == null)
            {
                throw new ArgumentNullException("command");
            }
            
            if (_repository == null)
            {
                throw new InvalidOperationException("Repository is not initialized.");
            }

            var aggregate = new Question(command.Id, command.QuestionText);
            aggregate.Version = -1;
            _repository.Save(aggregate, aggregate.Version);
        }
    }
}
