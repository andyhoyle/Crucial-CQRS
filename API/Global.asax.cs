using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;

namespace API
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            Bootstrapper.BootstrapStructureMap();
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
