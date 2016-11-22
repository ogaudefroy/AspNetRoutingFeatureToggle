namespace AspNetRoutingFeatureToggle.Tests
{
    using System;
    using System.Web;
    using System.Web.Routing;
    using Moq;
    using NUnit.Framework;

    [TestFixture]
    public class FeatureToggleRouteHandlerTest
    {
        [Test]
        public void FeatureToggleRouteHandler_Constructor_WithNullFeatureToggle_Throws()
        {
            Assert.That(() => new FeatureToggleRouteHandler(null, new Mock<IRouteHandler>().Object, new Mock<IRouteHandler>().Object), Throws.InstanceOf<ArgumentNullException>());
        }

        [Test]
        public void FeatureToggleRouteHandler_Constructor_WithNullCurrentHandler_Throws()
        {
            Assert.That(() => new FeatureToggleRouteHandler((r) => true, null, new Mock<IRouteHandler>().Object), Throws.InstanceOf<ArgumentNullException>());
        }

        [Test]
        public void FeatureToggleRouteHandler_Constructor_WithNullExperimentalHandler_Throws()
        {
            Assert.That(() => new FeatureToggleRouteHandler((r) => true, new Mock<IRouteHandler>().Object, null), Throws.InstanceOf<ArgumentNullException>());
        }

        [Test]
        public void FeatureToggleRouteHandler_GetHttpHandler_WithNullRequestContext_Throws()
        {
            var routeHandler = new FeatureToggleRouteHandler((r) => true, new Mock<IRouteHandler>().Object, new Mock<IRouteHandler>().Object);
            Assert.That(() => routeHandler.GetHttpHandler(null), Throws.InstanceOf<ArgumentNullException>()); 
        }

        [Test]
        public void FeatureToggleRouteHandler_GetHttpHandler_TrueFuncter_Returns_ExperimentalHandler()
        {
            var mockCurrentHandler = new Mock<IRouteHandler>();
            var experimentalHttpHandler = new Mock<IHttpHandler>().Object;
            var experimentalMockHandler = new Mock<IRouteHandler>();
            experimentalMockHandler.Setup(p => p.GetHttpHandler(It.IsAny<RequestContext>())).Returns(experimentalHttpHandler);

            var routeHandler = new FeatureToggleRouteHandler((r) => true, mockCurrentHandler.Object, experimentalMockHandler.Object);
            var handler = routeHandler.GetHttpHandler(new RequestContext());

            Assert.That(handler, Is.Not.Null);
            Assert.That(handler, Is.EqualTo(experimentalHttpHandler));
            experimentalMockHandler.Verify(p => p.GetHttpHandler(It.IsAny<RequestContext>()), Times.Once);
        }

        [Test]
        public void FeatureToggleRouteHandler_GetHttpHandler_FalseFuncter_Returns_CurrentHandler()
        {
            var mockExperimentalHandler = new Mock<IRouteHandler>();
            var currentHttpHandler = new Mock<IHttpHandler>().Object;
            var mockCurrentHandler = new Mock<IRouteHandler>();
            mockCurrentHandler.Setup(p => p.GetHttpHandler(It.IsAny<RequestContext>())).Returns(currentHttpHandler);

            var routeHandler = new FeatureToggleRouteHandler((r) => false, mockCurrentHandler.Object, mockExperimentalHandler.Object);
            var handler = routeHandler.GetHttpHandler(new RequestContext());

            Assert.That(handler, Is.Not.Null);
            Assert.That(handler, Is.EqualTo(currentHttpHandler));
            mockCurrentHandler.Verify(p => p.GetHttpHandler(It.IsAny<RequestContext>()), Times.Once);
        }
    }
}
