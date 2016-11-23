namespace AspNetRoutingFeatureToggle
{
    using System;
    using System.Web;
    using System.Web.Routing;

    /// <summary>
    /// Extends route to deliver based on feature toggle: custom defaults, constraints and data tokens.
    /// </summary>
    public class FeatureToggleRoute : Route
    {
        private readonly Func<RequestContext, bool> _ftFuncter;

        /// <summary>
        /// Initializes a new instance of the <see cref="FeatureToggleRoute"/> class.
        /// </summary>
        /// <param name="url">The route URL.</param>
        /// <param name="ftFuncter">The feature toggle functer.</param>
        /// <param name="currentRouteProperties">The current route properties.</param>
        /// <param name="experimentalRouteProperties">The experimental route properties.</param>
        public FeatureToggleRoute(
            string url,
            Func<RequestContext, bool> ftFuncter,
            RouteProperties currentRouteProperties,
            RouteProperties experimentalRouteProperties)
            : base(url, new FeatureToggleRouteHandler(ftFuncter, currentRouteProperties.RouteHandler, experimentalRouteProperties.RouteHandler))
        {
            if (ftFuncter == null)
            {
                throw new ArgumentNullException("ftFuncter");
            }
            _ftFuncter = ftFuncter;
            this.CurrentRouteProperties = currentRouteProperties;
            this.ExperimentalRouteProperties = experimentalRouteProperties;
        }

        /// <summary>
        /// Gets the current route properties.
        /// </summary>
        public RouteProperties CurrentRouteProperties
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the experimental route properties.
        /// </summary>
        public RouteProperties ExperimentalRouteProperties
        {
            get;
            private set;
        }

        /// <summary>
        /// Returns information about the requested route.
        /// </summary>
        /// <param name="httpContext">An object that encapsulates information about the HTTP request.</param
        /// <returns>An object that contains the values from the route definition.</returns>
        public override RouteData GetRouteData(HttpContextBase httpContext)
        {
            FixRouteValues(new RequestContext(httpContext, new RouteData()));
            return base.GetRouteData(httpContext);
        }

        /// <summary>
        /// Returns information about the URL that is associated with the route.
        /// </summary>
        /// <returns>An object that contains information about the URL that is associated with the route.</returns>
        /// <param name="requestContext">An object that encapsulates information about the requested route.</param>
        /// <param name="values">An object that contains the parameters for a route.</param>
        public override VirtualPathData GetVirtualPath(RequestContext requestContext, RouteValueDictionary values)
        {
            FixRouteValues(requestContext);
            return base.GetVirtualPath(requestContext, values);
        }
        
        private void FixRouteValues(RequestContext requestContext)
        {
            bool isExperimental = _ftFuncter(requestContext);
            this.Defaults = isExperimental ? this.ExperimentalRouteProperties.Defaults : this.CurrentRouteProperties.Defaults;
            this.Constraints = isExperimental ? this.ExperimentalRouteProperties.Constraints : this.CurrentRouteProperties.Constraints;
            this.DataTokens = isExperimental ? this.ExperimentalRouteProperties.DataTokens : this.CurrentRouteProperties.DataTokens;
        }
    }
}
