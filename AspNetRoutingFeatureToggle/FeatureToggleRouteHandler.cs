namespace AspNetRoutingFeatureToggle
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
        private readonly IRouteHandler _actualVersionRouteHandler;
        private readonly IRouteHandler _nextVersionRouteHandler;

        /// <summary>
        /// Initializes a new instance of the <see cref="FeatureToggleRouteHandler"/> class.
        /// </summary>
        /// <param name="ftFuncter">The feature toggle functer.</param>
        /// <param name="actualVersionHandler">The actual version route handler.</param>
        /// <param name="nextVersionHandler">The next version route handler.</param>
        public FeatureToggleRouteHandler(Func<RequestContext, bool> ftFuncter, IRouteHandler actualVersionHandler, IRouteHandler nextVersionHandler)
        {
            if (ftFuncter == null)
            {
                throw new ArgumentNullException("ftFuncter");
            }
            if (actualVersionHandler == null)
            {
                throw new ArgumentNullException("actualVersionHandler");
            }
            if (nextVersionHandler == null)
            {
                throw new ArgumentNullException("nextVersionHandler");
            }
            _ftFuncter = ftFuncter;
            _actualVersionRouteHandler = actualVersionHandler;
            _nextVersionRouteHandler = nextVersionHandler;
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
                ? _nextVersionRouteHandler.GetHttpHandler(requestContext)
                : _actualVersionRouteHandler.GetHttpHandler(requestContext);
        }
    }
}
