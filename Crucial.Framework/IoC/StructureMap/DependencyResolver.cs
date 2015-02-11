using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;
namespace Crucial.Framework.IoC.StructureMapProvider
{
    public static class DependencyResolver
    {
        static StructureMap.Container _container;

        public static void Register(Action<StructureMap.ConfigurationExpression> configuration)
        {
            _container = new StructureMap.Container(configuration);
        }

        public static StructureMap.Container Container
        {
            get
            {
                return _container;
            }
        }
    }
}
