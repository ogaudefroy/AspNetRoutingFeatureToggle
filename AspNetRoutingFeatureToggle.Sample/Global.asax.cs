namespace AspNetRoutingFeatureToggle.Sample
{
    using System;
    using System.Web.Routing;

    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            RouteTable.Routes.MapFeatureTogglablePageRoute("Default", "test", (r) => r.HttpContext.Request.IsSecureConnection, "~/WebForm1.aspx", "~/WebForm2.aspx");
        }
    }
}