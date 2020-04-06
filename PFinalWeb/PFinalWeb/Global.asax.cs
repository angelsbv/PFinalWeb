using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using PFinalWeb.Controllers;

namespace PFinalWeb
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            Exception ex = Server.GetLastError();
            Response.Clear();
            HttpException httpex = ex as HttpException;
            RouteData data = new RouteData();
            data.Values.Add("Controller", "Error");

            if (httpex == null)
            {
                data.Values.Add("Action", "Index");
            }
            else
            {
                switch (httpex.GetHttpCode())
                {
                    case 404:
                        data.Values.Add("Action", "Error404");
                        break;

                    case 405:
                        data.Values.Add("Action", "Error405");
                        break;

                    case 500:
                        data.Values.Add("Action", "Error500");
                        break;

                    default:
                        data.Values.Add("Action", "General");
                        break;
                }
                Server.ClearError();
                Response.TrySkipIisCustomErrors = true;
            }

            IController controller = new ErrorController();
            controller.Execute(new RequestContext(new HttpContextWrapper(Context), data));
        }
    }
}
