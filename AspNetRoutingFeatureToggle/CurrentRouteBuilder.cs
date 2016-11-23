namespace AspNetRoutingFeatureToggle
{
    using System.Web.Mvc;
    using System.Web.Routing;

    /// <summary>
    /// Builder used to build current route values.
    /// </summary>
    public class CurrentRouteBuilder
    {
        private readonly FeatureToggleRouteBuilder _builder;

        /// <summary>
        /// Initializes a new instance of the <see cref="CurrentRouteBuilder"/> class.
        /// </summary>
        /// <param name="builder">The underlying builder.</param>
        internal CurrentRouteBuilder(FeatureToggleRouteBuilder builder)
        {
            _builder = builder;
        }

        /// <summary>
        /// Sets the current route handler to PageRouteHandler (from WebForms to ?? scenario).
        /// </summary>
        /// <param name="physicalFile">The virtual path of the physical file for this <see cref="P:System.Web.Routing.RouteData.Route" /> object. The file must be located in the current application. Therefore, the path must begin with a tilde (~).</param>
        /// <returns>The FT route builder itself.</returns>
        public ExperimentalRouteBuilder WithCurrentPageRoute(string physicalFile)
        {
            return WithCurrentPageRoute(physicalFile, true, null, null, null);
        }

        /// <summary>
        /// Sets the current route handler to PageRouteHandler (from WebForms to ?? scenario).
        /// </summary>
        /// <param name="physicalFile">The virtual path of the physical file for this <see cref="P:System.Web.Routing.RouteData.Route" /> object. The file must be located in the current application. Therefore, the path must begin with a tilde (~).</param>
        /// <param name="checkFileAccess">If this property is set to false, authorization rules will be applied to the request URL and not to the URL of the physical page. If this property is set to true, authorization rules will be applied to both the request URL and to the URL of the physical page.</param>
        /// <returns>The FT route builder itself.</returns>
        public ExperimentalRouteBuilder WithCurrentPageRoute(string physicalFile, bool checkFileAccess)
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
        public ExperimentalRouteBuilder WithCurrentPageRoute(string physicalFile, bool checkFileAccess, RouteValueDictionary defaults)
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
        public ExperimentalRouteBuilder WithCurrentPageRoute(string physicalFile, bool checkFileAccess, RouteValueDictionary defaults, RouteValueDictionary constraints)
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
        public ExperimentalRouteBuilder WithCurrentPageRoute(string physicalFile, bool checkFileAccess, RouteValueDictionary defaults, RouteValueDictionary constraints, RouteValueDictionary dataTokens)
        {
            _builder.Current.RouteHandler = new PageRouteHandler(physicalFile, checkFileAccess);
            _builder.Current.Defaults = defaults;
            _builder.Current.Constraints = constraints;
            _builder.Current.DataTokens = dataTokens;
            return new ExperimentalRouteBuilder(_builder);
        }

        /// <summary>
        /// Sets the current route handler to MvcRouteHandler (from MVC to ?? scenario).
        /// </summary>
        /// <param name="defaults">The values to use if the URL does not contain all the parameters.</param>
        /// <returns>The FT route builder itself.</returns>
        public ExperimentalRouteBuilder WithCurrentMvcRoute(RouteValueDictionary defaults)
        {
            return WithCurrentMvcRoute(defaults, null, null);
        }

        /// <summary>
        /// Sets the current route handler to MvcRouteHandler (from MVC to ?? scenario).
        /// </summary>
        /// <param name="defaults">The values to use if the URL does not contain all the parameters.</param>
        /// <param name="constraints">A regular expression that specifies valid values for a URL parameter.</param>
        /// <returns>The FT route builder itself.</returns>
        public ExperimentalRouteBuilder WithCurrentMvcRoute(RouteValueDictionary defaults, RouteValueDictionary constraints)
        {
            return WithCurrentMvcRoute(defaults, constraints, null);
        }

        /// <summary>
        /// Sets the current route handler to MvcRouteHandler (from MVC to ?? scenario).
        /// </summary>
        /// <param name="defaults">The values to use if the URL does not contain all the parameters.</param>
        /// <param name="constraints">A regular expression that specifies valid values for a URL parameter.</param>
        /// <param name="dataTokens">The route data tokens.</param>
        /// <returns>The FT route builder itself.</returns>
        public ExperimentalRouteBuilder WithCurrentMvcRoute(RouteValueDictionary defaults, RouteValueDictionary constraints, RouteValueDictionary dataTokens)
        {
            _builder.Current.RouteHandler = new MvcRouteHandler();
            _builder.Current.Defaults = defaults;
            _builder.Current.Constraints = constraints;
            _builder.Current.DataTokens = dataTokens;
            return new ExperimentalRouteBuilder(_builder);
        }
    }
}
