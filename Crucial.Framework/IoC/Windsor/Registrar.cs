using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.ServiceModel;
using System.Web.Mvc;
using Castle.Core;
using Castle.Facilities.WcfIntegration;
using Castle.MicroKernel.Registration;

namespace Crucial.Framework.IoC.Windsor
{
    public static class Registrar
    {
        public static bool wcfEnabled = false;

        public static void Dispose()
        {
            Resolver.Container.Dispose();
        }

        public static T Resolve<T>()
        {
            return Resolver.Container.Resolve<T>();
        }

        public static void RegisterControllerFactory(ControllerBuilder builder, bool performanceLogging = false)
        {
            builder.SetControllerFactory(new Crucial.Framework.IoC.Windsor.WindsorControllerFactory(Resolver.Container, Assembly.GetCallingAssembly(), performanceLogging));
        }

        public static void RegisterControllerFactory(ControllerBuilder builder, List<string> assemblies, bool performanceLogging = false)
        {
            builder.SetControllerFactory(new Crucial.Framework.IoC.Windsor.WindsorControllerFactory(Resolver.Container, Assembly.GetCallingAssembly(), assemblies, performanceLogging));
        }

        public static void RegisterControllerFactory(ControllerBuilder builder, bool allAssemblies, bool performanceLogging = false)
        {
            builder.SetControllerFactory(new Crucial.Framework.IoC.Windsor.WindsorControllerFactory(Resolver.Container, allAssemblies, performanceLogging));
        }

        public static void RegisterSingleton(Type interfaceType, Type implementationType)
        {
            Register(interfaceType, implementationType, CrucialLifestyleType.Singleton);
        }

        public static void Register(Type interfaceType, Type implementationType)
        {
           Register(interfaceType, implementationType, CrucialLifestyleType.PerWebRequest);
        }

        public static void Register(Type interfaceType, Type implementationType, CrucialLifestyleType lifestyleType)
        {
            Resolver.Container.Register(Component.For(interfaceType).ImplementedBy(implementationType).LifeStyle.Is((LifestyleType)lifestyleType));
        }

        public static void RegisterAllFromAssembliesEndingWith<T>(string endsWith, bool performanceLogging = false)
        {
            RegisterAllFromAssembliesEndingWith<T>(CrucialLifestyleType.Transient, endsWith, performanceLogging);
        }

        public static void RegisterAllFromAssembliesEndingWith<T>(CrucialLifestyleType lifestyleType, string endsWith, bool performanceLogging = false)
        {
            Resolver.Container.Register(
                AllTypes.FromAssemblyContaining<T>()
                .Where(t => t.Name.EndsWith(endsWith))
                .WithService.AllInterfaces()
                .Configure(c => c.LifeStyle.Is((LifestyleType)lifestyleType))
                .ConfigureIf(
                    c => performanceLogging, c => c.Interceptors("Crucial.Common.Infra.Interceptors.PerformanceInterceptor")
                )
            );
        }

        public static void RegisterAllFromAssembliesNoInterceptors<T>()
        {
            Resolver.Container.Register(
                AllTypes.FromAssemblyContaining<T>()
                .Where(t => t.Name.EndsWith("Interceptor"))
                .WithService.AllInterfaces()

                .Configure(
                    c => c.LifeStyle.Is(LifestyleType.Transient)
                )
            );
        }

        public static void RegisterAllFromAssemblies<T>(bool performanceLogging = false)
        {
            Resolver.Container.Register(
                AllTypes.FromAssemblyContaining<T>()
                .BasedOn<Crucial.Framework.IoC.IAutoRegister>()
                .WithService.FromInterface()
                .Configure(
                    c => c.LifeStyle.Is(LifestyleType.Transient)
                ).ConfigureIf(
                    c => performanceLogging, c => c.Interceptors("Crucial.Common.Infra.Interceptors.PerformanceInterceptor")
                )
            );
        }

        public static void RegisterAllFromAssemblies<T>(CrucialLifestyleType lifestyleType, bool performanceLogging = false)
        {
            Resolver.Container.Register(
                AllTypes.FromAssemblyContaining<T>()
                .BasedOn<Crucial.Framework.IoC.IAutoRegister>()
                .WithService.FromInterface()
                .Configure(c => c.LifeStyle.Is((LifestyleType)lifestyleType))
                .ConfigureIf(
                    c => performanceLogging, c => c.Interceptors("Crucial.Common.Infra.Interceptors.PerformanceInterceptor")
                )
            );
        }

        public static void RegisterWcfClient<TInterface>(string endpoint)
        {
            Registrar.RegisterWcfClient<TInterface>(endpoint, new BasicHttpBinding());
        }

        public static void RegisterWcfClient<TInterface>(string endpoint, string bindingConfigurationName)
        {
            Registrar.RegisterWcfClient<TInterface>(endpoint, new BasicHttpBinding(bindingConfigurationName));
        }

        public static void RegisterWcfErrorHandler<TErrorHandler>() where TErrorHandler : class 
        {
            Resolver.Container.Register(Component.For<TErrorHandler>().Attribute("scope").Eq(WcfExtensionScope.Services));
        }

        public static void RegisterWcfClient<TInterface>(string endpoint, BasicHttpBinding binding)
        {
            if (!wcfEnabled)
            {
                Resolver.Container.AddFacility<WcfFacility>(f => f.CloseTimeout = TimeSpan.Zero);
                wcfEnabled = true;
            }

            Resolver.Container
                .Register(WcfClient.ForChannels(
                    new DefaultClientModel()
                    {
                        Endpoint = WcfEndpoint.ForContract(typeof(TInterface))
                            .BoundTo(binding ?? new BasicHttpBinding())
                            .At(endpoint)
                    }));
        }

        //TODO: Look at life cycle for WCF items
        public static void Register(Type interfaceType, Type implementationType, string named)
        {
            Register(interfaceType, implementationType, named, CrucialLifestyleType.PerWebRequest);
        }

        public static void Register(Type interfaceType, Type implementationType, string named, CrucialLifestyleType lifestyleType)
        {
            Resolver.Container.Register(Component.For(interfaceType).ImplementedBy(implementationType).Named(named).LifeStyle.Is((LifestyleType)lifestyleType));
        }

        public static void Register<TInterface, TImplementation>(CrucialLifestyleType lifestyleType)
                        where TInterface : class
            where TImplementation : TInterface
        {
            ComponentRegistration<TInterface> componentRegistration = Component.For<TInterface>().ImplementedBy<TImplementation>();
            Resolver.Container.Register(componentRegistration.LifeStyle.Is((LifestyleType)lifestyleType));
        }

        public static void EnableWcfFacility()
        {
            if (!wcfEnabled)
            {
                Resolver.Container.AddFacility<WcfFacility>(f => f.CloseTimeout = TimeSpan.Zero);
                wcfEnabled = true;
            }
        }

        public static void Install(string path)
        {
            Resolver.Container.Install(Castle.Windsor.Installer.Configuration.FromXmlFile(path));
        }

        public static void Register<TInterface, TImplementation>(string named, params KeyValuePair<string, object>[] parameters)
            where TInterface : class
            where TImplementation : TInterface
        {
            if (parameters == null) throw new ArgumentNullException("parameters");
            if (!parameters.Any())
            {
                throw new ArgumentException("Must pass at least one parameter");
            }

            ComponentRegistration<TInterface> componentRegistration = Component.For<TInterface>().ImplementedBy<TImplementation>().Named(named);

            BuildComponentRegistrationWithParameters(ref componentRegistration, parameters);

            Resolver.Container.Register(componentRegistration.LifeStyle.Transient);
        }

        public static void RegisterPerWcfSession<TInterface, TImplementation>()
            where TInterface : class
            where TImplementation : TInterface
        {
            ComponentRegistration<TInterface> componentRegistration =
                Component.For<TInterface>().ImplementedBy<TImplementation>().LifestylePerWcfOperation();

            BuildComponentRegistrationWithParameters(ref componentRegistration);

            Resolver.Container.Register(componentRegistration);
        }

        public static void RegisterAllFromAssembliesPerWcfOperation<T>(bool performanceLogging = false)
        {
            Resolver.Container.Register(
                AllTypes.FromAssemblyContaining<T>()
                .BasedOn<Crucial.Framework.IoC.IAutoRegister>()
                .WithService.FromInterface()
                .LifestylePerWcfOperation()
                .ConfigureIf(
                    c => performanceLogging, c => c.Interceptors("Crucial.Common.Infra.Interceptors.PerformanceInterceptor")
                )
            );
        }
        public static void RegisterAllFromAssembliesEndingWithPerWcfOperation<T>(bool performanceLogging = false)
        {
            Resolver.Container.Register(
                AllTypes.FromAssemblyContaining<T>()
                .Where(t => t.Name.EndsWith("Service"))
                .WithService.AllInterfaces()
                .LifestylePerWcfOperation()
                .ConfigureIf(
                    c => performanceLogging, c => c.Interceptors("Crucial.Common.Infra.Interceptors.PerformanceInterceptor")
                )
            );
        }
        public static void Register<TInterface, TImplementation>(params KeyValuePair<string, object>[] parameters) where TInterface : class
                                                                                                                   where TImplementation : TInterface
        {
            if (parameters == null) throw new ArgumentNullException("parameters");
            if (!parameters.Any())
            {
                throw new ArgumentException("Must pass at least one parameter");
            }

            ComponentRegistration<TInterface> componentRegistration = Component.For<TInterface>().ImplementedBy<TImplementation>();

            BuildComponentRegistrationWithParameters(ref componentRegistration, parameters);

            Resolver.Container.Register(componentRegistration.LifeStyle.Transient);
        }

        public static void Register<TInterface, TImplementation>(CrucialLifestyleType lifestyleType, params KeyValuePair<string, object>[] parameters)
            where TInterface : class
            where TImplementation : TInterface
        {
            if (parameters == null) throw new ArgumentNullException("parameters");
            if (!parameters.Any())
            {
                throw new ArgumentException("Must pass at least one parameter");
            }

            ComponentRegistration<TInterface> componentRegistration =
                Component.For<TInterface>().ImplementedBy<TImplementation>().LifeStyle.Is((LifestyleType)lifestyleType);

            BuildComponentRegistrationWithParameters(ref componentRegistration, parameters);

            Resolver.Container.Register(componentRegistration);
        }

        private static void BuildComponentRegistrationWithParameters<T>(ref ComponentRegistration<T> componentRegistration, params KeyValuePair<string, object>[] parameters) where T : class
        {
            componentRegistration = parameters.Aggregate(componentRegistration, (current, parameter) => current.DependsOn(Property.ForKey(parameter.Key).Eq(parameter.Value)));
        }
    }

    // Summary:
    //     Enumeration used to mark the component's lifestyle.
    public enum CrucialLifestyleType
    {
        // Summary:
        //     No lifestyle specified.
        Undefined = 0,
        //
        // Summary:
        //     Singleton components are instantiated once, and shared between all clients.
        Singleton = 1,
        //
        // Summary:
        //     Thread components have a unique instance per thread.
        Thread = 2,
        //
        // Summary:
        //     Transient components are created on demand.
        Transient = 3,
        //
        // Summary:
        //     Optimization of transient components that keeps instance in a pool instead
        //     of always creating them.
        Pooled = 4,
        //
        // Summary:
        //     PerWebRequest components are created once per Http Request
        PerWebRequest = 5,
        //
        // Summary:
        //     Any other logic to create/release components.
        Custom = 6,
        //
        // Summary:
        //     Instances are reused within the scope provided.
        Scoped = 7,
        //
        // Summary:
        //     Instance lifetime and reuse scope is bound to another component further up
        //     the object graph.  Good scenario for this would be unit of work bound to
        //     a presenter in a two tier MVP application.  When specified in xml a scopeRootBinderType
        //     attribute must be specified pointing to a type having default accessible
        //     constructor and public method matching signature of Func<IHandler[], IHandler>
        //     delegate.
        Bound = 8,
    }
}
