namespace AspNetRoutingFeatureToggle.Sample.Controllers
{
    using System.Web.Mvc;

    public class OffersController : Controller
    {
        [HttpGet]
        public ActionResult Details(int id)
        {
            return new ContentResult() { Content = "Offer Details (" + id + ") in MVC" };
        }
    }
}