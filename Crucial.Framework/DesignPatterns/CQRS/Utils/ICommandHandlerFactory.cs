using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crucial.DesignPatterns.CQRS.Commands;

namespace Crucial.DesignPatterns.CQRS.Utils
{
    public interface ICommandHandlerFactory
    {
        ICommandHandler<T> GetHandler<T>() where T : Command;
    }
}
