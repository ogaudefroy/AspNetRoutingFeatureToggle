namespace AspNetRoutingFeatureToggle.Tests
{
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Routing;
    using Moq;
    using NUnit.Framework;

    [TestFixture]
    public class FeatureToggleRouteTest
    {
        [Test]
        public void FeatureToggleRoute_Constructor_MapsRoutePropertiesValues()
        {
            var currentRouteProperties = new RouteProperties() { RouteHandler = new MvcRouteHandler() };
            var experimentalRouteProperties = new RouteProperties() { RouteHandler = new MvcRouteHandler() };
            var route = new FeatureToggleRoute("test", (r) => true, currentRouteProperties, experimentalRouteProperties);

            Assert.That(route.Url, Is.EqualTo("test"));
            Assert.That(route.Defaults, Is.Null);
            Assert.That(route.DataTokens, Is.Null);
            Assert.That(route.Constraints, Is.Null);
            Assert.That(route.CurrentRouteProperties, Is.EqualTo(currentRouteProperties));
            Assert.That(route.ExperimentalRouteProperties, Is.EqualTo(experimentalRouteProperties));
        }

        [Test]
        public void FeatureToggleRoute_GetRouteData_Test()
        {
            var currentRouteProperties = new RouteProperties()
            {
                Defaults = null,
                Constraints = new { id = @"\d+" }.ToRouteValueDictionary(),
                DataTokens = new { routename = "OfferDetailsWebForms" }.ToRouteValueDictionary(),
                RouteHandler = new PageRouteHandler("~/Pages/Offer/Details.aspx", true)
            };
            var experimentalRouteProperties = new RouteProperties()
            {
                Defaults = new { controller = "Offers", action = "Details"}.ToRouteValueDictionary(), 
                Constraints = new { id = @"\d+"}.ToRouteValueDictionary(),
                DataTokens = new { routename = "OfferDetailsMvc"}.ToRouteValueDictionary(),
                RouteHandler = new MvcRouteHandler()
            };
            bool ftValue = false;

            var route = new FeatureToggleRoute("jobs/job-{title}_{id}", (r) => ftValue, currentRouteProperties, experimentalRouteProperties);
            Assert.That(route.Url, Is.EqualTo("jobs/job-{title}_{id}"));
            Assert.That(route.Defaults, Is.Null);
            Assert.That(route.DataTokens, Is.Null);
            Assert.That(route.Constraints, Is.Null);
            Assert.That(route.CurrentRouteProperties, Is.EqualTo(currentRouteProperties));
            Assert.That(route.ExperimentalRouteProperties, Is.EqualTo(experimentalRouteProperties));

            var mockHttpRequest = new Mock<HttpRequestBase>();
            mockHttpRequest.SetupGet(p => p.AppRelativeCurrentExecutionFilePath).Returns("~/jobs/job-project-manager_1554");
            mockHttpRequest.SetupGet(p => p.PathInfo).Returns("");

            var mockHttpContext = new Mock<HttpContextBase>();
            mockHttpContext.SetupGet(p => p.Request).Returns(mockHttpRequest.Object);

            var routeData = route.GetRouteData(mockHttpContext.Object);

            Assert.That(routeData, Is.Not.Null);
            Assert.That(routeData.RouteHandler, Is.InstanceOf<PageRouteHandler>());
            Assert.That(routeData.Values, Is.Not.Null);
            Assert.That(routeData.Values.ContainsKey("controller"), Is.False);
            Assert.That(routeData.Values.ContainsKey("action"), Is.False);
            Assert.That(routeData.Values["title"], Is.EqualTo("project-manager"));
            Assert.That(routeData.Values["id"], Is.EqualTo("1554"));
            Assert.That(routeData.DataTokens, Is.Not.Null);
            Assert.That(routeData.DataTokens["routename"], Is.EqualTo("OfferDetailsWebForms"));

            Assert.That(route.Url, Is.EqualTo("jobs/job-{title}_{id}"));
            Assert.That(route.Defaults, Is.Null);
            Assert.That(route.DataTokens, Is.Null);
            Assert.That(route.Constraints, Is.Null);
            Assert.That(route.CurrentRouteProperties, Is.EqualTo(currentRouteProperties));
            Assert.That(route.ExperimentalRouteProperties, Is.EqualTo(experimentalRouteProperties));

            ftValue = true;
            routeData = route.GetRouteData(mockHttpContext.Object);

            Assert.That(routeData, Is.Not.Null);
            Assert.That(routeData.RouteHandler, Is.InstanceOf<MvcRouteHandler>());
            Assert.That(routeData.Values, Is.Not.Null);
            Assert.That(routeData.Values["controller"], Is.EqualTo("Offers"));
            Assert.That(routeData.Values["action"], Is.EqualTo("Details"));
            Assert.That(routeData.Values["title"], Is.EqualTo("project-manager"));
            Assert.That(routeData.Values["id"], Is.EqualTo("1554"));
            Assert.That(routeData.DataTokens, Is.Not.Null);
            Assert.That(routeData.DataTokens["routename"], Is.EqualTo("OfferDetailsMvc"));

            Assert.That(route.Url, Is.EqualTo("jobs/job-{title}_{id}"));
            Assert.That(route.Defaults, Is.Null);
            Assert.That(route.DataTokens, Is.Null);
            Assert.That(route.Constraints, Is.Null);
            Assert.That(route.CurrentRouteProperties, Is.EqualTo(currentRouteProperties));
            Assert.That(route.ExperimentalRouteProperties, Is.EqualTo(experimentalRouteProperties));
        }

        [Test]
        public void FeatureToggleRoute_GetVirtualPath_Test()
        {
            var currentRouteProperties = new RouteProperties()
            {
                Defaults = null,
                Constraints = new { id = @"\d+" }.ToRouteValueDictionary(),
                DataTokens = new { routename = "OfferDetailsWebForms" }.ToRouteValueDictionary(),
                RouteHandler = new PageRouteHandler("~/Pages/Offer/Details.aspx", true)
            };
            var experimentalRouteProperties = new RouteProperties()
            {
                Defaults = new { controller = "Offers", action = "Details" }.ToRouteValueDictionary(),
                Constraints = new { id = @"\d+" }.ToRouteValueDictionary(),
                DataTokens = new { routename = "OfferDetailsMvc" }.ToRouteValueDictionary(),
                RouteHandler = new MvcRouteHandler()
            };
            bool ftValue = false;

            var route = new FeatureToggleRoute("jobs/job-{title}_{id}", (r) => ftValue, currentRouteProperties, experimentalRouteProperties);
            Assert.That(route.Url, Is.EqualTo("jobs/job-{title}_{id}"));
            Assert.That(route.Defaults, Is.Null);
            Assert.That(route.DataTokens, Is.Null);
            Assert.That(route.Constraints, Is.Null);
            Assert.That(route.CurrentRouteProperties, Is.EqualTo(currentRouteProperties));
            Assert.That(route.ExperimentalRouteProperties, Is.EqualTo(experimentalRouteProperties));

            var ctx = new RequestContext(new Mock<HttpContextBase>().Object, new RouteData());
            var vpd = route.GetVirtualPath(ctx, new {id = 1554, title = "project-manager"}.ToRouteValueDictionary());

            Assert.That(vpd, Is.Not.Null);
            Assert.That(vpd.DataTokens, Is.Not.Null);
            Assert.That(vpd.DataTokens.Count, Is.EqualTo(1));
            Assert.That(vpd.DataTokens["routename"], Is.EqualTo("OfferDetailsWebForms"));
            Assert.That(vpd.Route, Is.EqualTo(route));
            Assert.That(vpd.VirtualPath, Is.EqualTo("jobs/job-project-manager_1554"));

            Assert.That(route.Url, Is.EqualTo("jobs/job-{title}_{id}"));
            Assert.That(route.Defaults, Is.Null);
            Assert.That(route.DataTokens, Is.Null);
            Assert.That(route.Constraints, Is.Null);
            Assert.That(route.CurrentRouteProperties, Is.EqualTo(currentRouteProperties));
            Assert.That(route.ExperimentalRouteProperties, Is.EqualTo(experimentalRouteProperties));

            ftValue = true;
            vpd = route.GetVirtualPath(ctx, new { id = 1554, title = "project-manager" }.ToRouteValueDictionary());

            Assert.That(vpd, Is.Not.Null);
            Assert.That(vpd.DataTokens, Is.Not.Null);
            Assert.That(vpd.DataTokens.Count, Is.EqualTo(1));
            Assert.That(vpd.DataTokens["routename"], Is.EqualTo("OfferDetailsMvc"));
            Assert.That(vpd.Route, Is.EqualTo(route));
            Assert.That(vpd.VirtualPath, Is.EqualTo("jobs/job-project-manager_1554"));

            Assert.That(route.Url, Is.EqualTo("jobs/job-{title}_{id}"));
            Assert.That(route.Defaults, Is.Null);
            Assert.That(route.DataTokens, Is.Null);
            Assert.That(route.Constraints, Is.Null);
            Assert.That(route.CurrentRouteProperties, Is.EqualTo(currentRouteProperties));
            Assert.That(route.ExperimentalRouteProperties, Is.EqualTo(experimentalRouteProperties));
        }
    }
}
