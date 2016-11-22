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
        private IRouteHandler _currentRouteHandler;
        private IRouteHandler _experimentRouteHandler;
        private RouteValueDictionary _currentDefaults;
        private RouteValueDictionary _experimentalDefaults;
        private RouteValueDictionary _currentConstraints;
        private RouteValueDictionary _experimentalConstraints;
        private RouteValueDictionary _currentDataTokens;
        private RouteValueDictionary _experimentalDataTokens;

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
            return WithCurrentPageRoute(physicalFile, true, null, null, null);
        }

        /// <summary>
        /// Sets the current route handler to PageRouteHandler (from WebForms to ?? scenario).
        /// </summary>
        /// <param name="physicalFile">The virtual path of the physical file for this <see cref="P:System.Web.Routing.RouteData.Route" /> object. The file must be located in the current application. Therefore, the path must begin with a tilde (~).</param>
        /// <param name="checkFileAccess">If this property is set to false, authorization rules will be applied to the request URL and not to the URL of the physical page. If this property is set to true, authorization rules will be applied to both the request URL and to the URL of the physical page.</param>
        /// <returns>The FT route builder itself.</returns>
        public FeatureToggleRouteBuilder WithCurrentPageRoute(string physicalFile, bool checkFileAccess)
        {
            return WithCurrentPageRoute(physicalFile, checkFileAccess, null, null, null);
        }

        /// <summary>
        /// Sets the current route handler to PageRouteHandler (from WebForms to ?? scenario).
        /// </summary>
        /// <param name="physicalFile">The virtual path of the physical file for this <see cref="P:System.Web.Routing.RouteData.Route" /> object. The file must be located in the current application. Therefore, the path must begin with a tilde (~).</param>
        /// <param name="checkFileAccess">If this property is set to false, authorization rules will be applied to the request URL and not to the URL of the physical page. If this property is set to true, authorization rules will be applied to both the request URL and to the URL of the physical page.</param>
        /// <param name="defaults">The values to use if the URL does not contain all the parameters.</param>
        /// <returns>The FT route builder itself.</returns>
        public FeatureToggleRouteBuilder WithCurrentPageRoute(string physicalFile, bool checkFileAccess, RouteValueDictionary defaults)
        {
            return WithCurrentPageRoute(physicalFile, checkFileAccess, defaults, null, null);
        }

        /// <summary>
        /// Sets the current route handler to PageRouteHandler (from WebForms to ?? scenario).
        /// </summary>
        /// <param name="physicalFile">The virtual path of the physical file for this <see cref="P:System.Web.Routing.RouteData.Route" /> object. The file must be located in the current application. Therefore, the path must begin with a tilde (~).</param>
        /// <param name="checkFileAccess">If this property is set to false, authorization rules will be applied to the request URL and not to the URL of the physical page. If this property is set to true, authorization rules will be applied to both the request URL and to the URL of the physical page.</param>
        /// <param name="defaults">The values to use if the URL does not contain all the parameters.</param>
        /// <param name="constraints">A regular expression that specifies valid values for a URL parameter.</param>
        /// <returns>The FT route builder itself.</returns>
        public FeatureToggleRouteBuilder WithCurrentPageRoute(string physicalFile, bool checkFileAccess, RouteValueDictionary defaults, RouteValueDictionary constraints)
        {
            return WithCurrentPageRoute(physicalFile, checkFileAccess, defaults, constraints, null);
        }

        /// <summary>
        /// Sets the current route handler to PageRouteHandler (from WebForms to ?? scenario).
        /// </summary>
        /// <param name="physicalFile">The virtual path of the physical file for this <see cref="P:System.Web.Routing.RouteData.Route" /> object. The file must be located in the current application. Therefore, the path must begin with a tilde (~).</param>
        /// <param name="checkFileAccess">If this property is set to false, authorization rules will be applied to the request URL and not to the URL of the physical page. If this property is set to true, authorization rules will be applied to both the request URL and to the URL of the physical page.</param>
        /// <param name="defaults">The values to use if the URL does not contain all the parameters.</param>
        /// <param name="constraints">A regular expression that specifies valid values for a URL parameter.</param>
        /// <param name="dataTokens">Custom values that are passed to the route handler, but which are not used to determine whether the route matches a specific URL pattern. These values are passed to the route handler, where they can be used for processing the request.</param>
        /// <returns>The FT route builder itself.</returns>
        public FeatureToggleRouteBuilder WithCurrentPageRoute(string physicalFile, bool checkFileAccess, RouteValueDictionary defaults, RouteValueDictionary constraints, RouteValueDictionary dataTokens)
        {
            _currentRouteHandler = new PageRouteHandler(physicalFile, checkFileAccess);
            _currentDefaults = defaults;
            _currentConstraints = constraints;
            _currentDataTokens = dataTokens;
            return this;
        }

        /// <summary>
        /// Sets the current route handler to MvcRouteHandler (from MVC to ?? scenario).
        /// </summary>
        /// <param name="defaults">The values to use if the URL does not contain all the parameters.</param>
        /// <returns>The FT route builder itself.</returns>
        public FeatureToggleRouteBuilder WithCurrentMvcRoute(RouteValueDictionary defaults)
        {
            return WithCurrentPageRoute(defaults, null, null);
        }

        /// <summary>
        /// Sets the current route handler to MvcRouteHandler (from MVC to ?? scenario).
        /// </summary>
        /// <param name="defaults">The values to use if the URL does not contain all the parameters.</param>
        /// <param name="constraints">A regular expression that specifies valid values for a URL parameter.</param>
        /// <returns>The FT route builder itself.</returns>
        public FeatureToggleRouteBuilder WithCurrentMvcRoute(RouteValueDictionary defaults, RouteValueDictionary constraints)
        {
            return WithCurrentPageRoute(defaults, constraints, null);
        }

        /// <summary>
        /// Sets the current route handler to MvcRouteHandler (from MVC to ?? scenario).
        /// </summary>
        /// <param name="defaults">The values to use if the URL does not contain all the parameters.</param>
        /// <param name="constraints">A regular expression that specifies valid values for a URL parameter.</param>
        /// <param name="dataTokens">The route data tokens.</param>
        /// <returns>The FT route builder itself.</returns>
        public FeatureToggleRouteBuilder WithCurrentPageRoute(RouteValueDictionary defaults, RouteValueDictionary constraints, RouteValueDictionary dataTokens)
        {
            _currentRouteHandler = new MvcRouteHandler();
            _currentDefaults = defaults;
            _currentConstraints = constraints;
            _currentDataTokens = dataTokens;
            return this;
        }

        /// <summary>
        /// Sets the experimental route handler to PageRouteHandler (from ?? to WebForms scenario).
        /// </summary>
        /// <param name="physicalFile">The virtual path of the physical file for this <see cref="P:System.Web.Routing.RouteData.Route" /> object. The file must be located in the current application. Therefore, the path must begin with a tilde (~).</param>
        /// <returns>The FT route builder itself.</returns>
        public FeatureToggleRouteBuilder WithExperimentalPageRoute(string physicalFile)
        {
            return WithExperimentalPageRoute(physicalFile, true, null, null, null);
        }

        /// <summary>
        /// Sets the experimental route handler to PageRouteHandler (from ?? to WebForms scenario).
        /// </summary>
        /// <param name="physicalFile">The virtual path of the physical file for this <see cref="P:System.Web.Routing.RouteData.Route" /> object. The file must be located in the current application. Therefore, the path must begin with a tilde (~).</param>
        /// <param name="checkFileAccess">If this property is set to false, authorization rules will be applied to the request URL and not to the URL of the physical page. If this property is set to true, authorization rules will be applied to both the request URL and to the URL of the physical page.</param>
        /// <returns>The FT route builder itself.</returns>
        public FeatureToggleRouteBuilder WithExperimentalPageRoute(string physicalFile, bool checkFileAccess)
        {
            return WithExperimentalPageRoute(physicalFile, checkFileAccess, null, null, null);
        }

        /// <summary>
        /// Sets the experimental route handler to PageRouteHandler (from ?? to WebForms scenario).
        /// </summary>
        /// <param name="physicalFile">The virtual path of the physical file for this <see cref="P:System.Web.Routing.RouteData.Route" /> object. The file must be located in the current application. Therefore, the path must begin with a tilde (~).</param>
        /// <param name="checkFileAccess">If this property is set to false, authorization rules will be applied to the request URL and not to the URL of the physical page. If this property is set to true, authorization rules will be applied to both the request URL and to the URL of the physical page.</param>
        /// <param name="defaults">The values to use if the URL does not contain all the parameters.</param>
        /// <returns>The FT route builder itself.</returns>
        public FeatureToggleRouteBuilder WithExperimentalPageRoute(string physicalFile, bool checkFileAccess, RouteValueDictionary defaults)
        {
            return WithExperimentalPageRoute(physicalFile, checkFileAccess, defaults, null, null);
        }

        /// <summary>
        /// Sets the experimental route handler to PageRouteHandler (from ?? to WebForms scenario).
        /// </summary>
        /// <param name="physicalFile">The virtual path of the physical file for this <see cref="P:System.Web.Routing.RouteData.Route" /> object. The file must be located in the current application. Therefore, the path must begin with a tilde (~).</param>
        /// <param name="checkFileAccess">If this property is set to false, authorization rules will be applied to the request URL and not to the URL of the physical page. If this property is set to true, authorization rules will be applied to both the request URL and to the URL of the physical page.</param>
        /// <param name="defaults">The values to use if the URL does not contain all the parameters.</param>
        /// <param name="constraints">A regular expression that specifies valid values for a URL parameter.</param>
        /// <returns>The FT route builder itself.</returns>
        public FeatureToggleRouteBuilder WithExperimentalPageRoute(string physicalFile, bool checkFileAccess, RouteValueDictionary defaults, RouteValueDictionary constraints)
        {
            return WithExperimentalPageRoute(physicalFile, checkFileAccess, defaults, constraints, null);
        }

        /// <summary>
        /// Sets the experimental route handler to PageRouteHandler (from ?? to WebForms scenario).
        /// </summary>
        /// <param name="physicalFile">The virtual path of the physical file for this <see cref="P:System.Web.Routing.RouteData.Route" /> object. The file must be located in the current application. Therefore, the path must begin with a tilde (~).</param>
        /// <param name="checkFileAccess">If this property is set to false, authorization rules will be applied to the request URL and not to the URL of the physical page. If this property is set to true, authorization rules will be applied to both the request URL and to the URL of the physical page.</param>
        /// <param name="defaults">The values to use if the URL does not contain all the parameters.</param>
        /// <param name="constraints">A regular expression that specifies valid values for a URL parameter.</param>
        /// <param name="dataTokens">Custom values that are passed to the route handler, but which are not used to determine whether the route matches a specific URL pattern. These values are passed to the route handler, where they can be used for processing the request.</param>
        /// <returns>The FT route builder itself.</returns>
        public FeatureToggleRouteBuilder WithExperimentalPageRoute(string physicalFile, bool checkFileAccess, RouteValueDictionary defaults, RouteValueDictionary constraints, RouteValueDictionary dataTokens)
        {
            _experimentRouteHandler = new PageRouteHandler(physicalFile, checkFileAccess);
            _experimentalDefaults = defaults;
            _experimentalConstraints = constraints;
            _experimentalDataTokens = dataTokens;
            return this;
        }

        /// <summary>
        /// Sets the experimental route handler to MvcRouteHandler (from ?? to MVC scenario).
        /// </summary>
        /// <returns>The FT route builder itself.</returns>
        public FeatureToggleRouteBuilder WithExperimentalMvcRoute()
        {
            return WithExperimentalMvcRoute(null, null, null);
        }

        /// <summary>
        /// Sets the experimental route handler to MvcRouteHandler (from ?? to MVC scenario).
        /// </summary>
        /// <param name="defaults">The values to use if the URL does not contain all the parameters.</param>
        /// <returns>The FT route builder itself.</returns>
        public FeatureToggleRouteBuilder WithExperimentalMvcRoute(RouteValueDictionary defaults)
        {
            return WithExperimentalMvcRoute(defaults, null, null);
        }

        /// <summary>
        /// Sets the experimental route handler to MvcRouteHandler (from ?? to MVC scenario).
        /// </summary>
        /// <param name="defaults">The values to use if the URL does not contain all the parameters.</param>
        /// <param name="constraints">A regular expression that specifies valid values for a URL parameter.</param>
        /// <returns>The FT route builder itself.</returns>
        public FeatureToggleRouteBuilder WithExperimentalMvcRoute(RouteValueDictionary defaults, RouteValueDictionary constraints)
        {
            return WithExperimentalMvcRoute(defaults, constraints, null);
        }

        /// <summary>
        /// Sets the experimental route handler to MvcRouteHandler (from ?? to MVC scenario).
        /// </summary>
        /// <param name="defaults">The values to use if the URL does not contain all the parameters.</param>
        /// <param name="constraints">A regular expression that specifies valid values for a URL parameter.</param>
        /// <param name="dataTokens">Custom values that are passed to the route handler, but which are not used to determine whether the route matches a specific URL pattern. These values are passed to the route handler, where they can be used for processing the request.</param>
        /// <returns>The FT route builder itself.</returns>
        public FeatureToggleRouteBuilder WithExperimentalMvcRoute(RouteValueDictionary defaults, RouteValueDictionary constraints, RouteValueDictionary dataTokens)
        {
            _experimentRouteHandler = new MvcRouteHandler();
            _experimentalDefaults = defaults;
            _experimentalConstraints = constraints;
            _experimentalDataTokens = dataTokens;
            return this;
        }

        /// <summary>
        /// Builds the route to add in route table.
        /// </summary>
        /// <returns>The generated route.</returns>
        public Route Build()
        {
            return new FeatureToggleRoute(
                _url,
                _ftFuncter,
                _currentDefaults,
                _experimentalDefaults,
                _currentConstraints,
                _experimentalConstraints,
                _currentDataTokens,
                _experimentalDataTokens,
                _currentRouteHandler,
                _experimentRouteHandler);
        }
    }
}
