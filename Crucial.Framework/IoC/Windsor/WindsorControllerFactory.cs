using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.Windsor;
using Castle.MicroKernel;
using Castle.MicroKernel.Registration;
using System.Reflection;
using Castle.Core;
using System.Web.Mvc;
using System.Web;
using System.Web.Routing;

namespace Crucial.Framework.IoC.Windsor
{
    public class WindsorControllerFactory : DefaultControllerFactory
    {
        #region private properties

        /// <summary>
        /// Private instance of a Windsor Container object used to resolve Controller dependencies.
        /// </summary>
        private readonly IWindsorContainer container;

        #endregion

        #region constructors

        public WindsorControllerFactory(IWindsorContainer container, Assembly ass, bool performanceLogging = false)
        {
            // Retain a private copy of the kernel.
            this.container = container;

            container.Register(AllTypes.FromAssembly(ass)
                            .BasedOn<IController>()
                            .If(t => t.Name.EndsWith("Controller"))
                            .Configure(c => c.LifeStyle.Is(LifestyleType.Transient))
                            .ConfigureIf(
                                c => performanceLogging, c => c.Interceptors("Crucial.Common.Infra.Interceptors.PerformanceInterceptor")
                            )
            );
        }

        public WindsorControllerFactory(IWindsorContainer container, Assembly ass, List<string> additionalAssemblies, bool performanceLogging = false)
        {
            // Retain a private copy of the kernel.
            this.container = container;

            container.Register(AllTypes.FromAssembly(ass)
                            .BasedOn<IController>()
                            .If(t => t.Name.EndsWith("Controller"))
                            .Configure(c => c.LifeStyle.Is(LifestyleType.Transient))
                            .ConfigureIf(
                                c => performanceLogging, c => c.Interceptors("Crucial.Common.Infra.Interceptors.PerformanceInterceptor")
                            )
            );

            //now register all controllers from the additional assembly list
            foreach (string assemblyName in additionalAssemblies)
            {
                string path = HttpContext.Current.Server.MapPath("") + "\\bin\\" + assemblyName + ".dll";

                container.Register(AllTypes.FromAssembly(Assembly.LoadFrom(path))
                         .BasedOn<IController>()
                         .If(t => t.Name.EndsWith("Controller"))
                         .Configure(c => c.LifeStyle.Is(LifestyleType.Transient))
                         .ConfigureIf(
                            c => performanceLogging, c => c.Interceptors("Crucial.Common.Infra.Interceptors.PerformanceInterceptor")
                         )
                );
            }
        }

        /// <summary>
        /// Constructs an instance of a WindsorControllerFactory class. 
        /// </summary>
        /// <remarks>This constructor registers all controller types as components.
        /// We need to set up the config, and instruct ASP.Net MVC to use this new controller factory
        /// by calling <code>SetControllerFactory()</code> inside the <code>Application_Start</code> handler in 
        /// Global.asax.cs</remarks>
        /// <param name="container">A Windsor Container instance holding the castle configuration settings.</param>
        public WindsorControllerFactory(IWindsorContainer container, bool performanceLogging = false)
        {
            // Retain a private copy of the kernel.
            this.container = container;

            // new method for MVC3 - used when this class is not in the same assembly as the contollers, controller are assumed to be in the calling assembly...
            container.Register(AllTypes.FromAssembly(Assembly.GetCallingAssembly())
                            .BasedOn<IController>()
                            .If(t => t.Name.EndsWith("Controller"))
                            .Configure(c => c.LifeStyle.Is(LifestyleType.Transient))
                            .ConfigureIf(
                                c => performanceLogging, c => c.Interceptors("Crucial.Common.Infra.Interceptors.PerformanceInterceptor")
                            )
            );
        }

        /// <summary>
        /// Constructs an instance of a WindsorControllerFactory class. (First overload)
        /// </summary>
        /// <param name="container">A Windsor Container instance holding the castle configuration settings.</param>
        /// <param name="additionalAssemblies">A list of assembly names that contain additional controllers.</param>
        public WindsorControllerFactory(IWindsorContainer container, bool allAssemblies, bool performanceLogging = false): this (container)
        {
            if (allAssemblies)
            {
                container.Register(
                    AllTypes.FromAssemblyInDirectory(
                        new AssemblyFilter(HttpRuntime.BinDirectory)
                    )
                    .BasedOn<IController>()
                    .Configure(c => c.LifeStyle.Is(LifestyleType.Transient))
                    .ConfigureIf(
                        c => performanceLogging, c => c.Interceptors("Crucial.Framework.Interceptors.PerformanceInterceptor")
                    )
                );
            }
        }

        #endregion

        /// <summary>
        /// Constructs the controller instance needed to service each request.
        /// </summary>
        /// <param name="requestContext">The context of the current request.</param>
        /// <param name="controllerType">The type of the controller to be resolved.</param>
        /// <returns>A Controller.</returns>
        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            if (controllerType == null)
            {
                return null;
            }

            Controller controller = null;
            try
            {
                controller = container.Resolve(controllerType) as Controller;
            }
            catch (ComponentNotFoundException)
            {
                throw new HttpException(404, "The requested controller was not found " + controllerType.ToString());
            }


            /*  The Crucial.WS.Utilities.MVC.Windsor.WindsorActionInvoker type that should be invoked (thru' IoC) has bugs and throws
             *  errors at runtime. This section has been commented out as we are not yet using DI into action filters.
             */

            //if (controller != null)
            //{
            //    try
            //    {
            //        /* Use Windsor itself to resolve the IActionInvoker dependency.
            //         * The custom IActionInvoker we supply allows property injection to take place
            //         * on ActionFilters */
            //        controller.ActionInvoker = container.Resolve<IActionInvoker>();
            //    }
            //    catch (ComponentNotFoundException)
            //    {
            //        // Retain the default ActionInvoker in this case.
            //    }
            //}

            return controller;
        }

        public override void ReleaseController(IController controller)
        {
            this.container.Release(controller);
        }
    }
}
