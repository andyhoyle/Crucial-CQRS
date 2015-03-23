using System;
using System.Collections.Generic;
using Crucial.Framework.DesignPatterns.CQRS.Commands;
using Crucial.Framework.DesignPatterns.CQRS.Exceptions;
using Crucial.Framework.DesignPatterns.CQRS.Utils;
using System.Threading.Tasks;

namespace Crucial.Framework.DesignPatterns.CQRS.Messaging
{
    public class CommandBus:ICommandBus
    {
        private readonly ICommandHandlerFactory _commandHandlerFactory;

        public CommandBus(ICommandHandlerFactory commandHandlerFactory)
        {
            _commandHandlerFactory = commandHandlerFactory;
        }

        public async Task Send<T>(T command) where T : Command
        {
            var handler = _commandHandlerFactory.GetHandler<T>();
            if (handler != null)
            {
                await handler.Execute(command).ConfigureAwait(false);
            }
            else
            {
                throw new UnregisteredDomainCommandException("no handler registered");
            }
        }        
    }
}
