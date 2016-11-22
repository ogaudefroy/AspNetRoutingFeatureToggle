namespace AspNetRoutingFeatureToggle.Sample.Pages.Offers
{
    public partial class Details : System.Web.UI.Page
    {
        public string OfferId
        {
            get { return this.RouteData.GetRequiredString("id"); }
        }
    }
}