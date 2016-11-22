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

        private readonly RouteValueDictionary _currentDefaults;
        private readonly RouteValueDictionary _currentConstraints;
        private readonly RouteValueDictionary _currentDataTokens;

        private readonly RouteValueDictionary _experimentalDefaults;
        private readonly RouteValueDictionary _experimentalConstraints;
        private readonly RouteValueDictionary _experimentalDataTokens;

        /// <summary>
        /// Initializes a new instance of the <see cref="FeatureToggleRoute"/> class.
        /// </summary>
        /// <param name="url">The route URL.</param>
        /// <param name="ftFuncter">The feature toggle functer.</param>
        /// <param name="currentDefaults">The current default route values.</param>
        /// <param name="experimentalDefaults">The experimental default route values.</param>
        /// <param name="currentConstraints">The current route constraints.</param>
        /// <param name="experimentalConstraints">The experimental route constraints.</param>
        /// <param name="currentDataTokens">The current route data tokens.</param>
        /// <param name="experimentalDataTokens">The experimental route data tokens.</param>
        /// <param name="currentRouteHandler">The current route handler.</param>
        /// <param name="experimentalRouteHandler">The experimental route handler.</param>
        public FeatureToggleRoute(
            string url,
            Func<RequestContext, bool> ftFuncter,
            RouteValueDictionary currentDefaults,
            RouteValueDictionary experimentalDefaults,
            RouteValueDictionary currentConstraints,
            RouteValueDictionary experimentalConstraints,
            RouteValueDictionary currentDataTokens,
            RouteValueDictionary experimentalDataTokens,
            IRouteHandler currentRouteHandler,
            IRouteHandler experimentalRouteHandler)
            : base(url, new FeatureToggleRouteHandler(ftFuncter, currentRouteHandler, experimentalRouteHandler))
        {
            if (ftFuncter == null)
            {
                throw new ArgumentNullException("ftFuncter");
            }
            _ftFuncter = ftFuncter;
            _currentDefaults = currentDefaults;
            _currentConstraints = currentConstraints;
            _currentDataTokens = currentDataTokens;
            _experimentalDefaults = experimentalDefaults;
            _experimentalConstraints = experimentalConstraints;
            _experimentalDataTokens = experimentalDataTokens;
        }

        /// <summary>
        /// Returns information about the requested route.
        /// </summary>
        /// <param name="httpContext">An object that encapsulates information about the HTTP request.</param
        /// <returns>An object that contains the values from the route definition.</returns>
        public override RouteData GetRouteData(HttpContextBase httpContext)
        {
            FixRouteValues();
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
            FixRouteValues();
            return base.GetVirtualPath(requestContext, values);
        }

        private RequestContext CreateRequestContext()
        {
            return new RequestContext(new HttpContextWrapper(HttpContext.Current), new RouteData());
        }

        private void FixRouteValues()
        {
            bool isExperimental = _ftFuncter(CreateRequestContext());
            this.Defaults = isExperimental ? _experimentalDefaults : _currentDefaults;
            this.Constraints = isExperimental ? _experimentalConstraints : _currentConstraints;
            this.DataTokens = isExperimental ? _experimentalDataTokens : _currentDataTokens;
        }
    }
}
