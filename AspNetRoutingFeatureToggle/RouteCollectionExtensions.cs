namespace AspNetRoutingFeatureToggle
{
    using System;
    using System.Web.Routing;

    /// <summary>
    /// Set of extension methods used 
    /// </summary>
    public static class RouteCollectionExtensions
    {
        /// <summary>
        /// Provides a way to define feature togglable routes for Web Forms applications.
        /// </summary>
        /// <param name="routes">The route collection on which the extension method applies.</param>
        /// <param name="routeName">The name of the route.</param>
        /// <param name="routeUrl">The URL pattern for the route.</param>
        /// <param name="featureToogle">The feature toggle functer, if true A is rendered, B otherwise.</param>
        /// <param name="physicalFileA">The physical URL for A feature of the route.</param>
        /// <param name="physicalFileB">The physical URL for A feature of the route</param>
        /// <returns>The route that is added to the route collection.</returns>
        public static Route MapFeatureTogglablePageRoute(this RouteCollection routes, string routeName, string routeUrl, Func<RequestContext, bool> featureToogle, string physicalFileA, string physicalFileB)
        {
            return routes.MapFeatureTogglablePageRoute(routeName, routeUrl, featureToogle, physicalFileA, physicalFileB, true, null, null, null);
        }

        /// <summary>
        /// Provides a way to define feature togglable routes for Web Forms applications.
        /// </summary>
        /// <param name="routes">The route collection on which the extension method applies.</param>
        /// <param name="routeName">The name of the route.</param>
        /// <param name="routeUrl">The URL pattern for the route.</param>
        /// <param name="featureToogle">The feature toggle functer, if true A is rendered, B otherwise.</param>
        /// <param name="physicalFileA">The physical URL for A feature of the route.</param>
        /// <param name="physicalFileB">The physical URL for A feature of the route</param>
        /// <param name="checkPhysicalUrlAccess">A value that indicates whether ASP.NET should validate that the user has authority to access the physical URL (the route URL is always checked). This parameter sets the <see cref="P:System.Web.Routing.PageRouteHandler.CheckPhysicalUrlAccess" /> property.</param>
        /// <returns>The route that is added to the route collection.</returns>
        public static Route MapFeatureTogglablePageRoute(this RouteCollection routes, string routeName, string routeUrl, Func<RequestContext, bool> featureToogle, string physicalFileA, string physicalFileB, bool checkPhysicalUrlAccess)
        {
            return routes.MapFeatureTogglablePageRoute(routeName, routeUrl, featureToogle, physicalFileA, physicalFileB, checkPhysicalUrlAccess, null, null, null);
        }

        /// <summary>
        /// Provides a way to define feature togglable routes for Web Forms applications.
        /// </summary>
        /// <param name="routes">The route collection on which the extension method applies.</param>
        /// <param name="routeName">The name of the route.</param>
        /// <param name="routeUrl">The URL pattern for the route.</param>
        /// <param name="featureToogle">The feature toggle functer, if true A is rendered, B otherwise.</param>
        /// <param name="physicalFileA">The physical URL for A feature of the route.</param>
        /// <param name="physicalFileB">The physical URL for A feature of the route</param>
        /// <param name="checkPhysicalUrlAccess">A value that indicates whether ASP.NET should validate that the user has authority to access the physical URL (the route URL is always checked). This parameter sets the <see cref="P:System.Web.Routing.PageRouteHandler.CheckPhysicalUrlAccess" /> property.</param>
        /// <param name="defaults">Default values for the route parameters.</param>
        /// <returns>The route that is added to the route collection.</returns>
        public static Route MapFeatureTogglablePageRoute(this RouteCollection routes, string routeName, string routeUrl, Func<RequestContext, bool> featureToogle, string physicalFileA, string physicalFileB, bool checkPhysicalUrlAccess, RouteValueDictionary defaults)
        {
            return routes.MapFeatureTogglablePageRoute(routeName, routeUrl, featureToogle, physicalFileA, physicalFileB, checkPhysicalUrlAccess, defaults, null, null);
        }

        /// <summary>
        /// Provides a way to define feature togglable routes for Web Forms applications.
        /// </summary>
        /// <param name="routes">The route collection on which the extension method applies.</param>
        /// <param name="routeName">The name of the route.</param>
        /// <param name="routeUrl">The URL pattern for the route.</param>
        /// <param name="featureToogle">The feature toggle functer, if true A is rendered, B otherwise.</param>
        /// <param name="physicalFileA">The physical URL for A feature of the route.</param>
        /// <param name="physicalFileB">The physical URL for A feature of the route</param>
        /// <param name="checkPhysicalUrlAccess">A value that indicates whether ASP.NET should validate that the user has authority to access the physical URL (the route URL is always checked). This parameter sets the <see cref="P:System.Web.Routing.PageRouteHandler.CheckPhysicalUrlAccess" /> property.</param>
        /// <param name="defaults">Default values for the route parameters.</param>
        /// <param name="constraints">Constraints that a URL request must meet in order to be processed as this route.</param>
        /// <returns>The route that is added to the route collection.</returns>
        public static Route MapFeatureTogglablePageRoute(this RouteCollection routes, string routeName, string routeUrl, Func<RequestContext, bool> featureToogle, string physicalFileA, string physicalFileB, bool checkPhysicalUrlAccess, RouteValueDictionary defaults, RouteValueDictionary constraints)
        {
            return routes.MapFeatureTogglablePageRoute(routeName, routeUrl, featureToogle, physicalFileA, physicalFileB, checkPhysicalUrlAccess, defaults, constraints, null);
        }

        /// <summary>
        /// Provides a way to define feature togglable routes for Web Forms applications.
        /// </summary>
        /// <param name="routes">The route collection on which the extension method applies.</param>
        /// <param name="routeName">The name of the route.</param>
        /// <param name="routeUrl">The URL pattern for the route.</param>
        /// <param name="featureToogle">The feature toggle functer, if true A is rendered, B otherwise.</param>
        /// <param name="physicalFileA">The physical URL for A feature of the route.</param>
        /// <param name="physicalFileB">The physical URL for A feature of the route</param>
        /// <param name="checkPhysicalUrlAccess">A value that indicates whether ASP.NET should validate that the user has authority to access the physical URL (the route URL is always checked). This parameter sets the <see cref="P:System.Web.Routing.PageRouteHandler.CheckPhysicalUrlAccess" /> property.</param>
        /// <param name="defaults">Default values for the route parameters.</param>
        /// <param name="constraints">Constraints that a URL request must meet in order to be processed as this route.</param>
        /// <param name="dataTokens">Values that are associated with the route that are not used to determine whether a route matches a URL pattern.</param>
        /// <returns>The route that is added to the route collection.</returns>
        public static Route MapFeatureTogglablePageRoute(this RouteCollection routes, string routeName, string routeUrl, Func<RequestContext, bool> featureToogle, string physicalFileA, string physicalFileB, bool checkPhysicalUrlAccess, RouteValueDictionary defaults, RouteValueDictionary constraints, RouteValueDictionary dataTokens)
        {
            if (routeUrl == null)
            {
                throw new ArgumentNullException("routeUrl");
            }
            if (string.IsNullOrEmpty(routeName))
            {
                throw new ArgumentNullException("routeName");
            }
            var routeHandler = new FeatureToggleRouteHandler(featureToogle,
                new PageRouteHandler(physicalFileA, checkPhysicalUrlAccess),
                new PageRouteHandler(physicalFileB, checkPhysicalUrlAccess));

            var route = new Route(routeUrl, defaults, constraints, dataTokens, routeHandler);
            routes.Add(routeName, route);
            return route;
        }
    }
}
