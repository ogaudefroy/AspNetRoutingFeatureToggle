namespace AspNetRoutingFeatureToggle.Mvc
{
    using System.Web.Mvc;

    public static class FeatureToggleRouteBuilderExtensions
    {
        public static FeatureToggleRouteBuilder WithExperimentMvcRoute(this FeatureToggleRouteBuilder builder)
        {
            builder.ExperimentRouteHandler = new MvcRouteHandler();
            return builder;
        }
    }
}
