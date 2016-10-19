namespace AspNetRoutingFeatureToggle.Mvc
{
    using System;
    using System.Web.Mvc;
    using System.Web.Routing;

    /// <summary>
    /// Set of extension methods on <see cref="RouteCollection"/> to add feature toggled routes.
    /// </summary>
    public static class RouteCollectionExtensions
    {
        /// <summary>
        /// Provides a way to define feature togglable routes for WebForms to MVC transition in applications.
        /// </summary>
        /// <param name="routes">The route collection on which the extension method applies.</param>
        /// <param name="routeName">The name of the route.</param>
        /// <param name="routeUrl">The URL pattern for the route.</param>
        /// <param name="featureToogle">The feature toggle functer, if true A is rendered, B otherwise.</param>
        /// <param name="physicalFile">The physical URL for A feature of the route.</param>
        /// <returns>The route that is added to the route collection.</returns>
        public static Route MapFeatureToggledRoute(this RouteCollection routes, string routeName, string routeUrl, Func<RequestContext, bool> featureToogle, string physicalFile)
        {
            return routes.MapFeatureToggledRoute(routeName, routeUrl, featureToogle, physicalFile, true, null, null, null);
        }

        /// <summary>
        /// Provides a way to define feature togglable routes for WebForms to MVC transition in applications.
        /// </summary>
        /// <param name="routes">The route collection on which the extension method applies.</param>
        /// <param name="routeName">The name of the route.</param>
        /// <param name="routeUrl">The URL pattern for the route.</param>
        /// <param name="featureToogle">The feature toggle functer, if true A is rendered, B otherwise.</param>
        /// <param name="physicalFile">The physical URL for A feature of the route.</param>
        /// <param name="checkPhysicalUrlAccess">A value that indicates whether ASP.NET should validate that the user has authority to access the physical URL (the route URL is always checked). This parameter sets the <see cref="P:System.Web.Routing.PageRouteHandler.CheckPhysicalUrlAccess" /> property.</param>
        /// <returns>The route that is added to the route collection.</returns>
        public static Route MapFeatureToggledRoute(this RouteCollection routes, string routeName, string routeUrl, Func<RequestContext, bool> featureToogle, string physicalFile, bool checkPhysicalUrlAccess)
        {
            return routes.MapFeatureToggledRoute(routeName, routeUrl, featureToogle, physicalFile, checkPhysicalUrlAccess, null, null, null);
        }

        /// <summary>
        /// Provides a way to define feature togglable routes for WebForms to MVC transition in applications.
        /// </summary>
        /// <param name="routes">The route collection on which the extension method applies.</param>
        /// <param name="routeName">The name of the route.</param>
        /// <param name="routeUrl">The URL pattern for the route.</param>
        /// <param name="featureToogle">The feature toggle functer, if true A is rendered, B otherwise.</param>
        /// <param name="physicalFile">The physical URL for A feature of the route.</param>
        /// <param name="checkPhysicalUrlAccess">A value that indicates whether ASP.NET should validate that the user has authority to access the physical URL (the route URL is always checked). This parameter sets the <see cref="P:System.Web.Routing.PageRouteHandler.CheckPhysicalUrlAccess" /> property.</param>
        /// <param name="defaults">An object that contains default route values.</param>
        /// <returns>The route that is added to the route collection.</returns>
        public static Route MapFeatureToggledRoute(this RouteCollection routes, string routeName, string routeUrl, Func<RequestContext, bool> featureToogle, string physicalFile, bool checkPhysicalUrlAccess, RouteValueDictionary defaults)
        {
            return routes.MapFeatureToggledRoute(routeName, routeUrl, featureToogle, physicalFile, checkPhysicalUrlAccess, defaults, null, null);
        }

        /// <summary>
        /// Provides a way to define feature togglable routes for WebForms to MVC transition in applications.
        /// </summary>
        /// <param name="routes">The route collection on which the extension method applies.</param>
        /// <param name="routeName">The name of the route.</param>
        /// <param name="routeUrl">The URL pattern for the route.</param>
        /// <param name="featureToogle">The feature toggle functer, if true A is rendered, B otherwise.</param>
        /// <param name="physicalFile">The physical URL for A feature of the route.</param>
        /// <param name="checkPhysicalUrlAccess">A value that indicates whether ASP.NET should validate that the user has authority to access the physical URL (the route URL is always checked). This parameter sets the <see cref="P:System.Web.Routing.PageRouteHandler.CheckPhysicalUrlAccess" /> property.</param>
        /// <param name="defaults">An object that contains default route values.</param>
        /// <param name="constraints">A set of expressions that specify values for the <paramref name="routeUrl" /> parameter.</param>
        /// <returns>The route that is added to the route collection.</returns>
        public static Route MapFeatureToggledRoute(this RouteCollection routes, string routeName, string routeUrl, Func<RequestContext, bool> featureToogle, string physicalFile, bool checkPhysicalUrlAccess, RouteValueDictionary defaults, RouteValueDictionary constraints)
        {
            return routes.MapFeatureToggledRoute(routeName, routeUrl, featureToogle, physicalFile, checkPhysicalUrlAccess, defaults, constraints, null);

        }

        /// <summary>
        /// Provides a way to define feature togglable routes for WebForms to MVC transition in applications.
        /// </summary>
        /// <param name="routes">The route collection on which the extension method applies.</param>
        /// <param name="routeName">The name of the route.</param>
        /// <param name="routeUrl">The URL pattern for the route.</param>
        /// <param name="featureToogle">The feature toggle functer, if true A is rendered, B otherwise.</param>
        /// <param name="physicalFile">The physical URL for A feature of the route.</param>
        /// <param name="checkPhysicalUrlAccess">A value that indicates whether ASP.NET should validate that the user has authority to access the physical URL (the route URL is always checked). This parameter sets the <see cref="P:System.Web.Routing.PageRouteHandler.CheckPhysicalUrlAccess" /> property.</param>
        /// <param name="defaults">An object that contains default route values.</param>
        /// <param name="constraints">A set of expressions that specify values for the <paramref name="routeUrl" /> parameter.</param>
        /// <param name="namespaces">A set of namespaces for the application.</param>
        /// <returns>The route that is added to the route collection.</returns>
        public static Route MapFeatureToggledRoute(this RouteCollection routes, string routeName, string routeUrl, Func<RequestContext, bool> featureToogle, string physicalFile, bool checkPhysicalUrlAccess, RouteValueDictionary defaults, RouteValueDictionary constraints, string[] namespaces)
        {
            if (routes == null)
            {
                throw new ArgumentNullException("routes");
            }
            if (routeUrl == null)
            {
                throw new ArgumentNullException("routeUrl");
            }
            var routeHandler = new FeatureToggleRouteHandler(featureToogle, new MvcRouteHandler(), new PageRouteHandler(physicalFile, checkPhysicalUrlAccess));
            Route route = new Route(routeUrl, routeHandler)
            {
                Defaults = defaults,
                Constraints = constraints,
                DataTokens = new RouteValueDictionary()
            };
            route.Validate();
            if (namespaces != null && namespaces.Length > 0)
            {
                route.DataTokens["Namespaces"] = namespaces;
            }
            routes.Add(routeName, route);
            return route;
        }
    }
}
