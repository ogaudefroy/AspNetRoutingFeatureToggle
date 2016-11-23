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
                .WithCurrentPageRoute("~/Pages/Offers/Details.aspx", true, null, new { id = @"\d+" })
                .WithExperimentalMvcRoute(
                    defaults: new { controller = "Offers" , action = "Details" }, 
                    constraints: new { id = @"\d+" })
                .Build();
            RouteTable.Routes.Add("OfferDetails", webFormsToMvcRoute);

            var mvcToMvcRoute = FeatureToggleRouteBuilder.WithUrl("job/job-list")
                .WithFeatureToogle((r) => r.HttpContext.Request.IsSecureConnection)
                .WithCurrentMvcRoute(new { controller = "Offers", action = "List" })
                .WithExperimentalMvcRoute(new { controller = "OffersV2", action = "List" })
                .Build();
            RouteTable.Routes.Add("OfferList", mvcToMvcRoute);
        }
    }
}