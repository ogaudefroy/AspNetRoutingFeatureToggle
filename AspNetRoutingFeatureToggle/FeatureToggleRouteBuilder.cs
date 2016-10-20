namespace AspNetRoutingFeatureToggle
{
    using System;
    using System.Web.Routing;

    public class FeatureToggleRouteBuilder
    {
        private readonly string _url;
        private Func<RequestContext, bool> _ftFuncter;
        private RouteValueDictionary _defaults;
        private RouteValueDictionary _constraints;
        private RouteValueDictionary _dataTokens;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="FeatureToggleRouteBuilder"/> class.
        /// </summary>
        /// <param name="url"></param>
        private FeatureToggleRouteBuilder(string url)
        {
            if (url == null)
            {
                throw new ArgumentNullException("url");
            }
            _url = url;
        }

        public IRouteHandler CurrentRouteHandler { get; set; }

        public IRouteHandler ExperimentRouteHandler { get; set; }
        
        public static FeatureToggleRouteBuilder WithUrl(string url)
        {
            return new FeatureToggleRouteBuilder(url);
        }

        public FeatureToggleRouteBuilder WithDefaults(RouteValueDictionary defaults)
        {
            _defaults = defaults;
            return this;
        }

        public FeatureToggleRouteBuilder WithConstraints(RouteValueDictionary constraints)
        {
            _constraints = constraints;
            return this;
        }

        public FeatureToggleRouteBuilder WithDataTokens(RouteValueDictionary dataTokens)
        {
            _dataTokens = dataTokens;
            return this;
        }

        public FeatureToggleRouteBuilder WithFeatureToogle(Func<RequestContext, bool> featureToggle)
        {
            _ftFuncter = featureToggle;
            return this;
        }

        public FeatureToggleRouteBuilder WithCurrentPageRoute(string physicalFile)
        {
            return WithCurrentPageRoute(physicalFile, true);
        }

        public FeatureToggleRouteBuilder WithCurrentPageRoute(string physicalFile, bool checkFileAccess)
        {
            this.CurrentRouteHandler = new PageRouteHandler(physicalFile, checkFileAccess);
            return this;
        }

        public FeatureToggleRouteBuilder WithExperimentPageRoute(string physicalFile)
        {
            return WithExperimentPageRoute(physicalFile, true);
        }

        public FeatureToggleRouteBuilder WithExperimentPageRoute(string physicalFile, bool checkFileAccess)
        {
            this.CurrentRouteHandler = new PageRouteHandler(physicalFile, checkFileAccess);
            return this;
        }

        public Route Build()
        {
            var handler = new FeatureToggleRouteHandler(_ftFuncter, this.CurrentRouteHandler, this.ExperimentRouteHandler);
            return new Route(_url, _defaults, _constraints, _dataTokens, handler);
        }
    }
}
