using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.UI;

namespace GET.WebForms
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // Código que é executado na inicialização do aplicativo
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            
            // Configurar ScriptResourceMapping para jQuery (necessário para UnobtrusiveValidationMode)
            ScriptManager.ScriptResourceMapping.AddDefinition("jquery", new ScriptResourceDefinition
            {
                Path = "~/Scripts/jquery-3.4.1.min.js",
                DebugPath = "~/Scripts/jquery-3.4.1.js",
                CdnPath = "https://ajax.aspnetcdn.com/ajax/jQuery/jquery-3.4.1.min.js",
                CdnDebugPath = "https://ajax.aspnetcdn.com/ajax/jQuery/jquery-3.4.1.js",
                CdnSupportsSecureConnection = true,
                LoadSuccessExpression = "window.jQuery"
            });
        }
    }
}