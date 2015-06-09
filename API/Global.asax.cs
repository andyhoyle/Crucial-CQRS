using Crucial.Framework.DesignPatterns.CQRS.Utils;
using Crucial.Providers.Questions.Data;
using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.SqlClient;
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

            if(ConfigurationManager.AppSettings["RestoreStateOnAppStart"] != null && ConfigurationManager.AppSettings["RestoreStateOnAppStart"].ToUpper() == "TRUE") {
                //Force drop db
                QuestionsDbContext.Drop();

                IStateHelper sh = Crucial.Framework.IoC.StructureMapProvider.DependencyResolver.Container.GetInstance<IStateHelper>();
                sh.RestoreState();
            }
        }
    }
}
