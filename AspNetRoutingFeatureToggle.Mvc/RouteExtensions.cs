namespace AspNetRoutingFeatureToggle.Mvc
{
    using System;
    using System.Collections.Generic;
    using System.Web.Routing;

    /// <summary>
    /// Extension methods on <see cref="Route"/> class.
    /// </summary>
    internal static class RouteExtensions
    {
        /// <summary>
        /// Validates route constraints.
        /// </summary>
        /// <param name="route">The route on which the extension method applies.</param>
        public static void Validate(this Route route)
        {
            if (route == null)
            {
                throw new ArgumentNullException("route");
            }
            if (route.Constraints == null)
            {
                return;
            }
            foreach (KeyValuePair<string, object> current in route.Constraints)
            {
                if (!(current.Value is string) && !(current.Value is IRouteConstraint))
                {
                    throw new InvalidOperationException(string.Format("The constraint entry '{0}' on the route with route templat '{1}' must have a string value or be of a type which implements '{2}'.", new object[] { current.Key, route.Url, typeof(IRouteConstraint).FullName }));
                }
            }
        }
    }
}
