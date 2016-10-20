namespace AspNetRoutingFeatureToggle.Sample
{
    using System;
    using System.Web.Routing;
    using AspNetRoutingFeatureToggle.Mvc;

    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            var webFormsToWebFormsRoute = FeatureToggleRouteBuilder.WithUrl("test")
                .WithFeatureToogle((r) => r.HttpContext.Request.IsSecureConnection)
                .WithCurrentPageRoute("~/WebForm1.aspx")
                .WithExperimentPageRoute("~/WebForm2.aspx")
                .Build();
            RouteTable.Routes.Add(webFormsToWebFormsRoute);

            var webFormsToMvcRoute = FeatureToggleRouteBuilder.WithUrl("offers/offer-{title}_{id}")
                .WithConstraints(new RouteValueDictionary() { { "id", @"\d+" } })
                .WithDefaults(new RouteValueDictionary() { { "culture", "en-US" } })
                .WithFeatureToogle((r) => r.HttpContext.Request.IsSecureConnection)
                .WithCurrentPageRoute("~/Pages/Offers/Details.aspx")
                .WithExperimentMvcRoute()
                .Build();
            RouteTable.Routes.Add("OfferDetails", webFormsToMvcRoute);
        }
    }
}