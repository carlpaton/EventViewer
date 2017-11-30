using System.Globalization;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace LogViewWebApplication
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_BeginRequest()
        {
            //MVC DatePattern for validations
            //var ci = new CultureInfo(System.Threading.Thread.CurrentThread.CurrentCulture.ToString());
            //ci.DateTimeFormat.ShortDatePattern = "dd/MM/yyyy";
            //System.Threading.Thread.CurrentThread.CurrentCulture = ci;
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
