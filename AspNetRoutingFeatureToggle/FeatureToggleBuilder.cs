namespace AspNetRoutingFeatureToggle
{
    using System;
    using System.Web.Routing;

    /// <summary>
    /// Builder used to fill the feature toggle functer.
    /// </summary>
    public class FeatureToggleBuilder
    {
        private readonly FeatureToggleRouteBuilder _builder;

        /// <summary>
        /// Initializes a new instance of the <see cref="FeatureToggleBuilder"/> class.
        /// </summary>
        /// <param name="builder">The route builder.</param>
        internal FeatureToggleBuilder(FeatureToggleRouteBuilder builder)
        {
            _builder = builder;
        }

        /// <summary>
        /// Sets the feature toggle functer.
        /// </summary>
        /// <param name="featureToggle">The feature toggle function.</param>
        /// <returns>The FT route builder itself.</returns>
        public CurrentRouteBuilder WithFeatureToogle(Func<RequestContext, bool> featureToggle)
        {
            if (featureToggle == null)
            {
                throw new ArgumentNullException("featureToggle");
            }
            _builder.FeatureToggleFuncter = featureToggle;
            return new CurrentRouteBuilder(_builder);
        }
    }
}
