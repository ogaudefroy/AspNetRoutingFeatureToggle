namespace AspNetRoutingFeatureToggle
{
    using System.Web.Routing;

    /// <summary>
    /// Common route properties.
    /// </summary>
    public class RouteProperties
    {
        /// <summary>
        /// Gets or sets the values to use if the URL does not contain all the parameters.
        /// </summary>
        /// <returns>An object that contains the parameter names and default values.</returns>
        public RouteValueDictionary Defaults
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a dictionary of expressions that specify valid values for a URL parameter.
        /// </summary>
        /// <returns>An object that contains the parameter names and expressions.</returns>
        public RouteValueDictionary Constraints
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets custom values that are passed to the route handler, but which are not used to determine whether the route matches a URL pattern.
        /// </summary>
        /// <returns>An object that contains custom values.</returns>
        public RouteValueDictionary DataTokens
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the object that processes requests for the route.
        /// </summary>
        /// <returns>The object that processes the request.</returns>
        public IRouteHandler RouteHandler
        {
            get;
            set;
        }
    }
}
