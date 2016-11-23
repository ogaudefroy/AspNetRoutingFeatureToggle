namespace AspNetRoutingFeatureToggle
{
    using System;
    using System.Web.Routing;

    /// <summary>
    /// Builder used to create feature toggled routes.
    /// </summary>
    public class FeatureToggleRouteBuilder
    {
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
            this.Url = url;
            this.Current = new RouteProperties();
            this.Experimental = new RouteProperties();
        }

        /// <summary>
        /// Gets the Url associated with the route.
        /// </summary>
        public string Url
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the feature toggle functer.
        /// </summary>
        public Func<RequestContext, bool> FeatureToggleFuncter
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the current route properties.
        /// </summary>
        public RouteProperties Current
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the experimental route properties.
        /// </summary>
        public RouteProperties Experimental
        {
            get;
            private set;
        }

        /// <summary>
        /// Builds a feature toggle route from a public facing URL.
        /// </summary>
        /// <param name="url">The target route URL.</param>
        /// <returns>The FT route builder itself.</returns>
        public static FeatureToggleBuilder WithUrl(string url)
        {
            return new FeatureToggleBuilder(new FeatureToggleRouteBuilder(url));
        }

        /// <summary>
        /// Builds the route to add in route table.
        /// </summary>
        /// <returns>The generated route.</returns>
        public Route Build()
        {
            return new FeatureToggleRoute(
                this.Url,
                this.FeatureToggleFuncter,
                this.Current,
                this.Experimental);
        }
    }
}
