namespace AspNetRoutingFeatureToggle.Tests
{
    using System;
    using System.Web.Mvc;
    using System.Web.Routing;
    using NUnit.Framework;

    [TestFixture]
    public class FeatureToggleRouteBuilderTest
    {
        [Test]
        public void FeatureToggleRouteBuilderTest_WithUrlNullOrEmpty_Throws()
        {
            Assert.That(() => FeatureToggleRouteBuilder.WithUrl(null), Throws.InstanceOf<ArgumentNullException>());
            Assert.That(() => FeatureToggleRouteBuilder.WithUrl(string.Empty), Throws.InstanceOf<ArgumentNullException>());
        }

        [Test]
        public void FeatureToggleRouteBuilderTest_WithFeatureToggleNull_Throws()
        {
            Assert.That(() => FeatureToggleRouteBuilder.WithUrl("test").WithFeatureToogle(null), Throws.InstanceOf<ArgumentNullException>());
        }

        [Test]
        public void FeatureToggleRouteBuilderTest_WebFormsToWebForms()
        {
            var defaultsCurrent = new RouteValueDictionary() { { "culture", "en-US" } };
            var defaultsConstraints = new RouteValueDictionary() { { "id", @"\d+" } };
            var defaultDataTokens = new RouteValueDictionary() { { "route-name", "test" } };

            var route = FeatureToggleRouteBuilder.WithUrl("test")
                .WithFeatureToogle((r) => true)
                .WithCurrentPageRoute("~/WebForms1.aspx", false, defaultsCurrent, defaultsConstraints, defaultDataTokens)
                .WithExperimentalPageRoute("~/WebForms2.aspx")
                .Build();

            Assert.That(route, Is.Not.Null);
            Assert.That(route, Is.InstanceOf<FeatureToggleRoute>());

            var ftRoute = (FeatureToggleRoute)route;
            Assert.That(ftRoute.RouteHandler, Is.InstanceOf<FeatureToggleRouteHandler>());

            Assert.That(ftRoute.CurrentRouteProperties.Defaults, Is.EqualTo(defaultsCurrent));
            Assert.That(ftRoute.CurrentRouteProperties.Constraints, Is.EqualTo(defaultsConstraints));
            Assert.That(ftRoute.CurrentRouteProperties.DataTokens, Is.EqualTo(defaultDataTokens));
            Assert.That(ftRoute.CurrentRouteProperties.RouteHandler, Is.InstanceOf<PageRouteHandler>());
            var currentRouteHandler = (PageRouteHandler)ftRoute.CurrentRouteProperties.RouteHandler;
            Assert.That(currentRouteHandler.CheckPhysicalUrlAccess, Is.False);
            Assert.That(currentRouteHandler.VirtualPath, Is.EqualTo("~/WebForms1.aspx"));

            Assert.That(ftRoute.ExperimentalRouteProperties.Defaults, Is.Null);
            Assert.That(ftRoute.ExperimentalRouteProperties.Constraints, Is.Null);
            Assert.That(ftRoute.ExperimentalRouteProperties.DataTokens, Is.Null);
            Assert.That(ftRoute.ExperimentalRouteProperties.RouteHandler, Is.InstanceOf<PageRouteHandler>());
            var experimentalRouteHandler = (PageRouteHandler)ftRoute.ExperimentalRouteProperties.RouteHandler;
            Assert.That(experimentalRouteHandler.CheckPhysicalUrlAccess, Is.True);
            Assert.That(experimentalRouteHandler.VirtualPath, Is.EqualTo("~/WebForms2.aspx"));
        }

        [Test]
        public void FeatureToggleRouteBuilderTest_WebFormsToMvc()
        {
            var defaultsCurrent = new RouteValueDictionary() { { "culture", "en-US" } };
            var defaultsConstraints = new RouteValueDictionary() { { "id", @"\d+" } };
            var defaultDataTokens = new RouteValueDictionary() { { "route-name", "test" } };
            var experimentalDefaults = new RouteValueDictionary() { { "controller", "Home" }, { "action", "Index" } };

            var route = FeatureToggleRouteBuilder.WithUrl("test")
                .WithFeatureToogle((r) => true)
                .WithCurrentPageRoute("~/WebForms1.aspx", false, defaultsCurrent, defaultsConstraints, defaultDataTokens)
                .WithExperimentalMvcRoute(experimentalDefaults)
                .Build();

            Assert.That(route, Is.Not.Null);
            Assert.That(route, Is.InstanceOf<FeatureToggleRoute>());

            var ftRoute = (FeatureToggleRoute)route;
            Assert.That(ftRoute.RouteHandler, Is.InstanceOf<FeatureToggleRouteHandler>());

            Assert.That(ftRoute.CurrentRouteProperties.Defaults, Is.EqualTo(defaultsCurrent));
            Assert.That(ftRoute.CurrentRouteProperties.Constraints, Is.EqualTo(defaultsConstraints));
            Assert.That(ftRoute.CurrentRouteProperties.DataTokens, Is.EqualTo(defaultDataTokens));
            Assert.That(ftRoute.CurrentRouteProperties.RouteHandler, Is.InstanceOf<PageRouteHandler>());
            var currentRouteHandler = (PageRouteHandler)ftRoute.CurrentRouteProperties.RouteHandler;
            Assert.That(currentRouteHandler.CheckPhysicalUrlAccess, Is.False);
            Assert.That(currentRouteHandler.VirtualPath, Is.EqualTo("~/WebForms1.aspx"));

            Assert.That(ftRoute.ExperimentalRouteProperties.Defaults, Is.EqualTo(experimentalDefaults));
            Assert.That(ftRoute.ExperimentalRouteProperties.Constraints, Is.Null);
            Assert.That(ftRoute.ExperimentalRouteProperties.DataTokens, Is.Null);
            Assert.That(ftRoute.ExperimentalRouteProperties.RouteHandler, Is.InstanceOf<MvcRouteHandler>());
        }
        
        [Test]
        public void FeatureToggleRouteBuilderTest_MvcToMvc()
        {
            var defaultsCurrent = new RouteValueDictionary() { { "culture", "en-US" }, { "controller", "Home" }, { "action", "Index" } };
            var defaultsConstraints = new RouteValueDictionary() { { "id", @"\d+" } };
            var defaultDataTokens = new RouteValueDictionary() { { "route-name", "test" } };
            var experimentalDefaults = new RouteValueDictionary() { { "controller", "HomeV2" }, { "action", "Index" } };

            var route = FeatureToggleRouteBuilder.WithUrl("test")
                .WithFeatureToogle((r) => true)
                .WithCurrentMvcRoute(defaultsCurrent, defaultsConstraints, defaultDataTokens)
                .WithExperimentalMvcRoute(experimentalDefaults)
                .Build();

            Assert.That(route, Is.Not.Null);
            Assert.That(route, Is.InstanceOf<FeatureToggleRoute>());

            var ftRoute = (FeatureToggleRoute)route;
            Assert.That(ftRoute.RouteHandler, Is.InstanceOf<FeatureToggleRouteHandler>());

            Assert.That(ftRoute.CurrentRouteProperties.Defaults, Is.EqualTo(defaultsCurrent));
            Assert.That(ftRoute.CurrentRouteProperties.Constraints, Is.EqualTo(defaultsConstraints));
            Assert.That(ftRoute.CurrentRouteProperties.DataTokens, Is.EqualTo(defaultDataTokens));
            Assert.That(ftRoute.CurrentRouteProperties.RouteHandler, Is.InstanceOf<MvcRouteHandler>());

            Assert.That(ftRoute.ExperimentalRouteProperties.Defaults, Is.EqualTo(experimentalDefaults));
            Assert.That(ftRoute.ExperimentalRouteProperties.Constraints, Is.Null);
            Assert.That(ftRoute.ExperimentalRouteProperties.DataTokens, Is.Null);
            Assert.That(ftRoute.ExperimentalRouteProperties.RouteHandler, Is.InstanceOf<MvcRouteHandler>());
        }

        [Test]
        public void FeatureToggleRouteBuilderTest_MvcToWebForms()
        {
            var defaultsCurrent = new RouteValueDictionary() { { "culture", "en-US" }, { "controller", "Home" }, { "action", "Index" } };
            var defaultsConstraints = new RouteValueDictionary() { { "id", @"\d+" } };
            var defaultDataTokens = new RouteValueDictionary() { { "route-name", "test" } };
            var experimentalDefaults = new RouteValueDictionary() { { "controller", "HomeV2" }, { "action", "Index" } };

            var route = FeatureToggleRouteBuilder.WithUrl("test")
                .WithFeatureToogle((r) => true)
                .WithCurrentMvcRoute(defaultsCurrent, defaultsConstraints, defaultDataTokens)
                .WithExperimentalPageRoute("~/WebForms2.aspx", false, experimentalDefaults)
                .Build();

            Assert.That(route, Is.Not.Null);
            Assert.That(route, Is.InstanceOf<FeatureToggleRoute>());

            var ftRoute = (FeatureToggleRoute)route;
            Assert.That(ftRoute.RouteHandler, Is.InstanceOf<FeatureToggleRouteHandler>());

            Assert.That(ftRoute.CurrentRouteProperties.Defaults, Is.EqualTo(defaultsCurrent));
            Assert.That(ftRoute.CurrentRouteProperties.Constraints, Is.EqualTo(defaultsConstraints));
            Assert.That(ftRoute.CurrentRouteProperties.DataTokens, Is.EqualTo(defaultDataTokens));
            Assert.That(ftRoute.CurrentRouteProperties.RouteHandler, Is.InstanceOf<MvcRouteHandler>());

            Assert.That(ftRoute.ExperimentalRouteProperties.Defaults, Is.EqualTo(experimentalDefaults));
            Assert.That(ftRoute.ExperimentalRouteProperties.Constraints, Is.Null);
            Assert.That(ftRoute.ExperimentalRouteProperties.DataTokens, Is.Null);
            Assert.That(ftRoute.ExperimentalRouteProperties.RouteHandler, Is.InstanceOf<PageRouteHandler>());
            var experimentalRouteHandler = (PageRouteHandler)ftRoute.ExperimentalRouteProperties.RouteHandler;
            Assert.That(experimentalRouteHandler.CheckPhysicalUrlAccess, Is.False);
            Assert.That(experimentalRouteHandler.VirtualPath, Is.EqualTo("~/WebForms2.aspx"));
        }
    }
}
