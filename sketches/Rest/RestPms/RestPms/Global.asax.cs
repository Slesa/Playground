using System;
using System.ServiceModel.Activation;
using System.Web;
using System.Web.Routing;

namespace RestPms
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            RegisterRoutes();
        }

        private void RegisterRoutes()
        {
            // Edit the base address of CurrencyService by replacing the "CurrencyService" string below
            RouteTable.Routes.Add(new ServiceRoute("Currencies", new WebServiceHostFactory(), typeof(CurrencyService)));
            RouteTable.Routes.Add(new ServiceRoute("Discounts", new WebServiceHostFactory(), typeof(DiscountService)));
        }
    }
}
