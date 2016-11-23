# AspNetRoutingFeatureToggle
A feature toggle library with ASP.Net routing attempting to solve A/B testing with constant URLs.

### WebForms to WebForms

    var route = FeatureToggleRouteBuilder.WithUrl("homepage")
                .WithFeatureToogle((r) => r.HttpContext.Request.IsSecureConnection)
                .WithCurrentPageRoute("~/WebForm1.aspx")
                .WithExperimentalPageRoute("~/WebForm2.aspx")
                .Build();

### WebForms to MVC

    var route = FeatureToggleRouteBuilder.WithUrl("offers/offer-{title}_{id}")
                .WithFeatureToogle((r) => r.HttpContext.Request.IsSecureConnection)
                .WithCurrentPageRoute("~/Pages/Offers/Details.aspx", true, null, new { id = @"\d+" })
                .WithExperimentalMvcRoute(new { controller = "Offers", action = "Details" }, new { id = @"\d+" })
                .Build();

### MVC to MVC

    var route = FeatureToggleRouteBuilder.WithUrl("test")
                .WithFeatureToogle((r) => r.HttpContext.Request.IsSecureConnection)
                .WithCurrentMvcRoute(new { controller = "Home", action = "Index" })
                .WithExperimentalMvcRoute(new { controller = "HomeV2", action = "Index" })
                .Build();

### MVC to WebForms (sounds weird I know...)

    var route = FeatureToggleRouteBuilder.WithUrl("offers/offer-{title}_{id}")
                .WithFeatureToogle((r) => r.HttpContext.Request.IsSecureConnection)
                .WithCurrentMvcRoute(new { controller = "Offers", action = "Details" }, new { id = @"\d+" })
                .WithExperimentalPageRoute("~/Pages/Offers/Details.aspx", true, null, new { id = @"\d+" })                
                .Build();
