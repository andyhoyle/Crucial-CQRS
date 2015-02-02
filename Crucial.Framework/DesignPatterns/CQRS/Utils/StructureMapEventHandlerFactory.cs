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
            var handlers = GetHandlerType<T>();
            
            var lstHandlers = handlers.Select(handler => (IEventHandler<T>) ObjectFactory.GetInstance(handler)).ToList();
            return lstHandlers;
        }

        private static IEnumerable<Type> GetHandlerType<T>() where T : Event
        {/*
            var handlers = typeof(IEventHandler<T>).Assembly.GetExportedTypes()
                .Where(x => x.GetInterfaces()
                    .Any(a => a.IsGenericType && a.GetGenericTypeDefinition() == typeof(IEventHandler<>)));
            return handlers;*/

            var handlers = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(
                    t => t.GetInterfaces()
                        .Where(i => i.IsGenericType)
                        .Any(i => i.GetGenericTypeDefinition() == typeof(IEventHandler<>) && i.GetGenericArguments().Any(aa => aa == typeof(T)))
                )
                .ToList();

            return handlers;

            //var handlers = typeof(IEventHandler<>).Assembly.GetExportedTypes()
            //    .Where(x => x.GetInterfaces()
            //        .Any(a => a.IsGenericType && a.GetGenericTypeDefinition() == typeof(IEventHandler<>))).Where(h => h.GetInterfaces().Any(ii => ii.GetGenericArguments().Any(aa => aa == typeof(T)))).ToList();


            //return handlers;
        }
    }
}
