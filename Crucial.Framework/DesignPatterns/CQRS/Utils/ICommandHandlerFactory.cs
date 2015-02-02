using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crucial.Framework.DesignPatterns.CQRS.Commands;

namespace Crucial.Framework.DesignPatterns.CQRS.Utils
{
    public interface ICommandHandlerFactory
    {
        ICommandHandler<T> GetHandler<T>() where T : Command;
    }
}
