using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StructureMap;
using Crucial.Framework.DesignPatterns.CQRS.Commands;

namespace Crucial.Framework.DesignPatterns.CQRS.Utils
{
    public class StructureMapCommandHandlerFactory : ICommandHandlerFactory
    {
        public ICommandHandler<T> GetHandler<T>() where T : Command
        {
            var handlers = GetHandlerTypes<T>().ToList();

            var cmdHandler = handlers.Select(handler =>
                (ICommandHandler<T>)ObjectFactory.GetInstance(handler)).FirstOrDefault();

            return cmdHandler;
        }

        private IEnumerable<Type> GetHandlerTypes<T>() where T : Command
        {
            var handlers = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(
                    t => t.GetInterfaces()
                        .Where(i => i.IsGenericType)
                        .Any(i => i.GetGenericTypeDefinition() == typeof(ICommandHandler<>) && i.GetGenericArguments().Any(aa => aa == typeof(T)))
                )
                .ToList();

            return handlers;
        }

    }
}
