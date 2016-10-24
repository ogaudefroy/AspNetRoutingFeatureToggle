namespace AspNetRoutingFeatureToggle.Sample
{
    using System;
    using System.Web.Routing;

    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            var webFormsToWebFormsRoute = FeatureToggleRouteBuilder.WithUrl("test")
                .WithFeatureToogle((r) => r.HttpContext.Request.IsSecureConnection)
                .WithCurrentPageRoute("~/WebForm1.aspx")
                .WithExperimentalPageRoute("~/WebForm2.aspx")
                .Build();
            RouteTable.Routes.Add(webFormsToWebFormsRoute);

            var webFormsToMvcRoute = FeatureToggleRouteBuilder.WithUrl("offers/offer-{title}_{id}")
                .WithConstraints(new RouteValueDictionary() { { "id", @"\d+" } })
                .WithDefaults(new RouteValueDictionary() { { "culture", "en-US" }, { "controller", "Offers" }, { "action", "Details" } })
                .WithFeatureToogle((r) => r.HttpContext.Request.IsSecureConnection)
                .WithCurrentPageRoute("~/Pages/Offers/Details.aspx")
                .WithExperimentalMvcRoute()
                .Build();
            RouteTable.Routes.Add("OfferDetails", webFormsToMvcRoute);
        }
    }
}