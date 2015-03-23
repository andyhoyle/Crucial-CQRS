using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crucial.Framework.DesignPatterns.CQRS.Commands
{
    public interface ICommandHandler<TCommand> where TCommand : Command
    {
        Task Execute(TCommand command);
    }
}
