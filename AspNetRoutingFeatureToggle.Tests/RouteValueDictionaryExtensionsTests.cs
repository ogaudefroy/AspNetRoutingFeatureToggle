namespace AspNetRoutingFeatureToggle.Tests
{
    using System.Collections.Generic;
    using NUnit.Framework;

    [TestFixture]
    public class RouteValueDictionaryExtensionsTests
    {
        [Test]
        public void RouteValueDictionaryExtensions_ToRouteValueDictionary_NullValue_Returns_Null()
        {
            object value = null;
            Assert.That(() => value.ToRouteValueDictionary(), Is.Null);
        }

        [Test]
        public void RouteValueDictionaryExtensions_ToRouteValueDictionary_Dictionary()
        {
            var values = new Dictionary<string, object>()
            {
                { "controller", "Offer" },
                { "action", "List" }
            };

            var routeValues = values.ToRouteValueDictionary();
            Assert.That(routeValues, Is.Not.Null);
            Assert.That(routeValues["controller"], Is.EqualTo("Offer"));
            Assert.That(routeValues["action"], Is.EqualTo("List"));
        }

        [Test]
        public void RouteValueDictionaryExtensions_ToRouteValueDictionary_AnonymousObject()
        {
            var values = new {controller = "Offer", action = "List"};

            var routeValues = values.ToRouteValueDictionary();
            Assert.That(routeValues, Is.Not.Null);
            Assert.That(routeValues["controller"], Is.EqualTo("Offer"));
            Assert.That(routeValues["action"], Is.EqualTo("List"));
        }
    }
}
