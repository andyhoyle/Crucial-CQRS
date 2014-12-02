using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.Windsor;

namespace Crucial.Framework.IoC.Windsor
{
    internal sealed class Resolver
    {
        private static readonly object LockObj = new object();

        private static IWindsorContainer container;

        private static Resolver instance = new Resolver();

        private Resolver()
        {
            container = new WindsorContainer();
        }

        internal static IWindsorContainer Container
        {
            get { return container; }

            set
            {
                lock (LockObj)
                {
                    container = value;
                }
            }
        }


        internal static Resolver Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (LockObj)
                    {
                        if (instance == null)
                        {
                            instance = new Resolver();
                        }
                    }
                }

                return instance;
            }
        }

        internal static T Resolve<T>()
        {
            return container.Resolve<T>();
        }

        internal static object Resolve(Type type)
        {
            return container.Resolve(type);
        }
    }
}
