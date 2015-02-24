using System;
using System.Collections.Generic;
using System.Linq;
using StructureMap;
using Crucial.Framework.DesignPatterns.CQRS.Events;

namespace Crucial.Framework.DesignPatterns.CQRS.Utils
{
    public class StructureMapEventHandlerFactory : IEventHandlerFactory
    {
        public IEnumerable<IEventHandler<T>> GetHandlers<T>() where T : Event
        {
            return Crucial.Framework.IoC.StructureMapProvider.DependencyResolver.Container.GetAllInstances<IEventHandler<T>>();
        }
    }
}
