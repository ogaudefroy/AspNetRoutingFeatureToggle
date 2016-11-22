namespace AspNetRoutingFeatureToggle
{
    using System;
    using System.Web.Mvc;
    using System.Web.Routing;

    /// <summary>
    /// Builder user to create feature toggled routes.
    /// </summary>
    public class FeatureToggleRouteBuilder
    {
        private readonly string _url;
        private Func<RequestContext, bool> _ftFuncter;
        private RouteValueDictionary _defaults;
        private RouteValueDictionary _constraints;
        private RouteValueDictionary _dataTokens;
        private IRouteHandler _currentRouteHandler;
        private IRouteHandler _experimentRouteHandler;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="FeatureToggleRouteBuilder"/> class.
        /// </summary>
        /// <param name="url">The target route URL.</param>
        private FeatureToggleRouteBuilder(string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                throw new ArgumentNullException("url");
            }
            _url = url;
        }
        
        /// <summary>
        /// Builds a feature toggle route from a public facing URL.
        /// </summary>
        /// <param name="url">The target route URL.</param>
        /// <returns>The FT route builder itself.</returns>
        public static FeatureToggleRouteBuilder WithUrl(string url)
        {
            return new FeatureToggleRouteBuilder(url);
        }

        /// <summary>
        /// Sets the default values on the route.
        /// </summary>
        /// <param name="defaults"></param>
        /// <returns>The FT route builder itself.</returns>
        public FeatureToggleRouteBuilder WithDefaults(RouteValueDictionary defaults)
        {
            _defaults = defaults;
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="constraints"></param>
        /// <returns>The FT route builder itself.</returns>
        public FeatureToggleRouteBuilder WithConstraints(RouteValueDictionary constraints)
        {
            _constraints = constraints;
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataTokens"></param>
        /// <returns>The FT route builder itself.</returns>
        public FeatureToggleRouteBuilder WithDataTokens(RouteValueDictionary dataTokens)
        {
            _dataTokens = dataTokens;
            return this;
        }

        /// <summary>
        /// Sets the feature toggle functer.
        /// </summary>
        /// <param name="featureToggle">The feature toggle function.</param>
        /// <returns>The FT route builder itself.</returns>
        public FeatureToggleRouteBuilder WithFeatureToogle(Func<RequestContext, bool> featureToggle)
        {
            _ftFuncter = featureToggle;
            return this;
        }

        /// <summary>
        /// Sets the current route handler to PageRouteHandler (from WebForms to ?? scenario).
        /// </summary>
        /// <param name="physicalFile">The virtual path of the physical file for this <see cref="P:System.Web.Routing.RouteData.Route" /> object. The file must be located in the current application. Therefore, the path must begin with a tilde (~).</param>
        /// <returns>The FT route builder itself.</returns>
        public FeatureToggleRouteBuilder WithCurrentPageRoute(string physicalFile)
        {
            return WithCurrentPageRoute(physicalFile, true);
        }

        /// <summary>
        /// Sets the current route handler to PageRouteHandler (from WebForms to ?? scenario).
        /// </summary>
        /// <param name="physicalFile">The virtual path of the physical file for this <see cref="P:System.Web.Routing.RouteData.Route" /> object. The file must be located in the current application. Therefore, the path must begin with a tilde (~).</param>
        /// <param name="checkFileAccess">If this property is set to false, authorization rules will be applied to the request URL and not to the URL of the physical page. If this property is set to true, authorization rules will be applied to both the request URL and to the URL of the physical page.</param>
        /// <returns>The FT route builder itself.</returns>
        public FeatureToggleRouteBuilder WithCurrentPageRoute(string physicalFile, bool checkFileAccess)
        {
            _currentRouteHandler = new PageRouteHandler(physicalFile, checkFileAccess);
            return this;
        }

        /// <summary>
        /// Sets the experimental route handler to PageRouteHandler (from ?? to WebForms scenario).
        /// </summary>
        /// <param name="physicalFile">The virtual path of the physical file for this <see cref="P:System.Web.Routing.RouteData.Route" /> object. The file must be located in the current application. Therefore, the path must begin with a tilde (~).</param>
        /// <returns>The FT route builder itself.</returns>
        public FeatureToggleRouteBuilder WithExperimentalPageRoute(string physicalFile)
        {
            return WithExperimentalPageRoute(physicalFile, true);
        }

        /// <summary>
        /// Sets the experimental route handler to PageRouteHandler (from ?? to WebForms scenario).
        /// </summary>
        /// <param name="physicalFile">The virtual path of the physical file for this <see cref="P:System.Web.Routing.RouteData.Route" /> object. The file must be located in the current application. Therefore, the path must begin with a tilde (~).</param>
        /// <param name="checkFileAccess">If this property is set to false, authorization rules will be applied to the request URL and not to the URL of the physical page. If this property is set to true, authorization rules will be applied to both the request URL and to the URL of the physical page.</param>
        /// <returns>The FT route builder itself.</returns>
        public FeatureToggleRouteBuilder WithExperimentalPageRoute(string physicalFile, bool checkFileAccess)
        {
            _currentRouteHandler = new PageRouteHandler(physicalFile, checkFileAccess);
            return this;
        }

        /// <summary>
        /// Sets the experimental route handler to MvcRouteHandler (from ?? to MVC scenario).
        /// </summary>
        /// <returns>The FT route builder itself.</returns>
        public FeatureToggleRouteBuilder WithExperimentalMvcRoute()
        {
            _experimentRouteHandler = new MvcRouteHandler();
            return this;
        }
        
        /// <summary>
        /// Builds the route to add in route table.
        /// </summary>
        /// <returns>The generated route.</returns>
        public Route Build()
        {
            var handler = new FeatureToggleRouteHandler(_ftFuncter, _currentRouteHandler, _experimentRouteHandler);
            return new Route(_url, _defaults, _constraints, _dataTokens, handler);
        }
    }
}
