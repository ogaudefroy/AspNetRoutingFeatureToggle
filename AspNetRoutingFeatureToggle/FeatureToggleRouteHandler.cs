﻿namespace AspNetRoutingFeatureToggle
{
    using System;
    using System.Web;
    using System.Web.Routing;

    /// <summary>
    /// A custom route handler implementing A/B testing with route handling.
    /// </summary>
    public class FeatureToggleRouteHandler : IRouteHandler
    {
        private readonly Func<RequestContext, bool> _ftFuncter;
        private readonly IRouteHandler _currentRouteHandler;
        private readonly IRouteHandler _experimentalRouteHandler;

        /// <summary>
        /// Initializes a new instance of the <see cref="FeatureToggleRouteHandler"/> class.
        /// </summary>
        /// <param name="ftFuncter">The feature toggle functer.</param>
        /// <param name="currentHandler">The actual version route handler.</param>
        /// <param name="experimentalHandler">The next version route handler.</param>
        public FeatureToggleRouteHandler(Func<RequestContext, bool> ftFuncter, IRouteHandler currentHandler, IRouteHandler experimentalHandler)
        {
            if (ftFuncter == null)
            {
                throw new ArgumentNullException("ftFuncter");
            }
            if (currentHandler == null)
            {
                throw new ArgumentNullException("currentHandler");
            }
            if (experimentalHandler == null)
            {
                throw new ArgumentNullException("experimentalHandler");
            }
            _ftFuncter = ftFuncter;
            _currentRouteHandler = currentHandler;
            _experimentalRouteHandler = experimentalHandler;
        }

        /// <summary>
        /// Provides the object that processes the request.
        /// </summary>
        /// <param name="requestContext">An object that encapsulates information about the request.</param>
        /// <returns>An object that processes the request.</returns>
        public IHttpHandler GetHttpHandler(RequestContext requestContext)
        {
            if (requestContext == null)
            {
                throw new ArgumentNullException("requestContext");
            }
            return _ftFuncter(requestContext)
                ? _experimentalRouteHandler.GetHttpHandler(requestContext)
                : _currentRouteHandler.GetHttpHandler(requestContext);
        }
    }
}
