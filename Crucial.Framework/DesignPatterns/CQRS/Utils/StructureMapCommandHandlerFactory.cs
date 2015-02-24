using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crucial.Framework.DesignPatterns.CQRS.Commands;

namespace Crucial.Framework.DesignPatterns.CQRS.Utils
{
    public class StructureMapCommandHandlerFactory : ICommandHandlerFactory
    {
        public ICommandHandler<T> GetHandler<T>() where T : Command
        {
            return Crucial.Framework.IoC.StructureMapProvider.DependencyResolver.Container.GetInstance<ICommandHandler<T>>();
        }
    }
}
