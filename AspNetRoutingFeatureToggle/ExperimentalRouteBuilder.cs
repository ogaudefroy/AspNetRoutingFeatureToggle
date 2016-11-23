namespace AspNetRoutingFeatureToggle
{
    using System.Web.Mvc;
    using System.Web.Routing;

    /// <summary>
    /// Builder used to build experimental route values.
    /// </summary>
    public class ExperimentalRouteBuilder
    {
        private readonly FeatureToggleRouteBuilder _builder;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExperimentalRouteBuilder"/> class.
        /// </summary>
        /// <param name="builder">The underlying builder.</param>
        internal ExperimentalRouteBuilder(FeatureToggleRouteBuilder builder)
        {
            _builder = builder;
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
        public FeatureToggleRouteBuilder WithExperimentalPageRoute(string physicalFile, bool checkFileAccess, object defaults)
        {
            return WithExperimentalPageRoute(physicalFile, checkFileAccess, defaults, null, null);
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
        public FeatureToggleRouteBuilder WithExperimentalPageRoute(string physicalFile, bool checkFileAccess, object defaults, object constraints)
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
        public FeatureToggleRouteBuilder WithExperimentalPageRoute(string physicalFile, bool checkFileAccess, object defaults, object constraints, object dataTokens)
        {
            return WithExperimentalPageRoute(physicalFile, checkFileAccess, defaults.ToRouteValueDictionary(), constraints.ToRouteValueDictionary(), dataTokens.ToRouteValueDictionary());
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
            _builder.Experimental.RouteHandler = new PageRouteHandler(physicalFile, checkFileAccess);
            _builder.Experimental.Defaults = defaults;
            _builder.Experimental.Constraints = constraints;
            _builder.Experimental.DataTokens = dataTokens;
            return _builder;
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
        /// <returns>The FT route builder itself.</returns>
        public FeatureToggleRouteBuilder WithExperimentalMvcRoute(object defaults)
        {
            return WithExperimentalMvcRoute(defaults, null, null);
        }

        /// <summary>
        /// Sets the experimental route handler to MvcRouteHandler (from ?? to MVC scenario).
        /// </summary>
        /// <param name="defaults">The values to use if the URL does not contain all the parameters.</param>
        /// <param name="constraints">A regular expression that specifies valid values for a URL parameter.</param>
        /// <returns>The FT route builder itself.</returns>
        public FeatureToggleRouteBuilder WithExperimentalMvcRoute(object defaults, object constraints)
        {
            return WithExperimentalMvcRoute(defaults, constraints, null);
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
        public FeatureToggleRouteBuilder WithExperimentalMvcRoute(object defaults, object constraints, object dataTokens)
        {
            return WithExperimentalMvcRoute(defaults.ToRouteValueDictionary(), constraints.ToRouteValueDictionary(), dataTokens.ToRouteValueDictionary());
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
            _builder.Experimental.RouteHandler = new MvcRouteHandler();
            _builder.Experimental.Defaults = defaults;
            _builder.Experimental.Constraints = constraints;
            _builder.Experimental.DataTokens = dataTokens;
            return _builder;
        }
    }
}
