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
            : base(url, null)
        {
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
            RouteProperties actualProperties = new RouteProperties()
            {
                Defaults = this.Defaults,
                Constraints = this.Constraints,
                DataTokens = this.DataTokens,
                RouteHandler = this.RouteHandler
            };
            var requestContext = new RequestContext(httpContext, new RouteData());

            try
            {
                bool isExperimental = _ftFuncter(requestContext);
                this.Defaults = isExperimental ? this.ExperimentalRouteProperties.Defaults : this.CurrentRouteProperties.Defaults;
                this.Constraints = isExperimental ? this.ExperimentalRouteProperties.Constraints : this.CurrentRouteProperties.Constraints;
                this.DataTokens = isExperimental ? this.ExperimentalRouteProperties.DataTokens : this.CurrentRouteProperties.DataTokens;
                this.RouteHandler = isExperimental ? this.ExperimentalRouteProperties.RouteHandler : this.CurrentRouteProperties.RouteHandler;

                return base.GetRouteData(httpContext);
            }
            finally
            {
                this.Defaults = actualProperties.Defaults;
                this.Constraints = actualProperties.Constraints;
                this.DataTokens = actualProperties.DataTokens;
                this.RouteHandler = actualProperties.RouteHandler;
            }
        }

        /// <summary>
        /// Returns information about the URL that is associated with the route.
        /// </summary>
        /// <returns>An object that contains information about the URL that is associated with the route.</returns>
        /// <param name="requestContext">An object that encapsulates information about the requested route.</param>
        /// <param name="values">An object that contains the parameters for a route.</param>
        public override VirtualPathData GetVirtualPath(RequestContext requestContext, RouteValueDictionary values)
        {
            RouteProperties actualProperties = new RouteProperties()
            {
                Defaults = this.Defaults,
                Constraints = this.Constraints,
                DataTokens = this.DataTokens,
                RouteHandler = this.RouteHandler
            };

            try
            {
                bool isExperimental = _ftFuncter(requestContext);
                this.Defaults = isExperimental ? this.ExperimentalRouteProperties.Defaults : this.CurrentRouteProperties.Defaults;
                this.Constraints = isExperimental ? this.ExperimentalRouteProperties.Constraints : this.CurrentRouteProperties.Constraints;
                this.DataTokens = isExperimental ? this.ExperimentalRouteProperties.DataTokens : this.CurrentRouteProperties.DataTokens;
                this.RouteHandler = isExperimental ? this.ExperimentalRouteProperties.RouteHandler : this.CurrentRouteProperties.RouteHandler;

                return base.GetVirtualPath(requestContext, values);
            }
            finally
            {
                this.Defaults = actualProperties.Defaults;
                this.Constraints = actualProperties.Constraints;
                this.DataTokens = actualProperties.DataTokens;
                this.RouteHandler = actualProperties.RouteHandler;
            }
        }
    }
}
