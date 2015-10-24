using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace BookByText
{
    public class MvcApplication : System.Web.HttpApplication
    {
        public static readonly IBookService BookService = new BookService();
        public static readonly ISmsService SmsService;
        
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
