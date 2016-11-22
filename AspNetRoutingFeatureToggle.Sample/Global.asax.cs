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
                .WithFeatureToogle((r) => r.HttpContext.Request.IsSecureConnection)
                .WithCurrentPageRoute("~/Pages/Offers/Details.aspx", true, null, new RouteValueDictionary() { { "id", @"\d+" } })
                .WithExperimentalMvcRoute(new RouteValueDictionary() { { "controller", "Offers" }, { "action", "Details" } }, new RouteValueDictionary() { { "id", @"\d+" } })
                .Build();
            RouteTable.Routes.Add("OfferDetails", webFormsToMvcRoute);

            var mvcToMvcRoute = FeatureToggleRouteBuilder.WithUrl("job/job-list")
                .WithFeatureToogle((r) => r.HttpContext.Request.IsSecureConnection)
                .WithCurrentMvcRoute(new RouteValueDictionary() { { "controller", "Offers" }, { "action", "List" } })
                .WithExperimentalMvcRoute(new RouteValueDictionary() { { "controller", "OffersV2" }, { "action", "List" } })
                .Build();
            RouteTable.Routes.Add("OfferList", mvcToMvcRoute);
        }
    }
}