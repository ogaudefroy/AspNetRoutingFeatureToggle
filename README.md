# AspNetRoutingFeatureToggle
[![Build status](https://ci.appveyor.com/api/projects/status/gwb3vj9obv6i2te5/branch/master?svg=true)](https://ci.appveyor.com/project/ogaudefroy/aspnetroutingfeaturetoggle/branch/master)  [![NuGet version](https://badge.fury.io/nu/AspNetRoutingFeatureToggle.svg)](https://badge.fury.io/nu/AspNetRoutingFeatureToggle)  
AspNetRoutingFeatureToggle is a feature toggle library based on ASP.Net routing attempting to solve A/B testing with constant URLs.

## Use Case
Currently migrating a legacy ASP.Net application ?  
You want to implement A/B testing ?  
Maintaining public facing URLs is an explicit requirement ?  
Your URLs are already managed by ASP.Net routing ?

If all of the above are true, then you might be interested by this library to succeed your migration path.

Here is how it works: 

 - Feature toggle is based on a predicate with the [RequestContext](https://msdn.microsoft.com/en-us/library/system.web.routing.requestcontext%28v=vs.110%29.aspx) 
 - If the toggle returns true then ExperimentalRoute is executed
 - Otherwise CurrentRoute is executed
    

Current implementation supports WebForms and MVC [IRouteHandler](https://msdn.microsoft.com/fr-fr/library/system.web.routing.iroutehandler(v=vs.110).aspx) implementations ; works with anonymous and named routes.
A fluent builder helps you configure seamlessly your routes.

## Examples
### WebForms to WebForms

    var route = FeatureToggleRouteBuilder.WithUrl("homepage")
                .WithFeatureToogle((r) => r.HttpContext.Request.IsSecureConnection)
                .WithCurrentPageRoute("~/WebForm1.aspx")
                .WithExperimentalPageRoute("~/WebForm2.aspx")
                .Build();

### WebForms to MVC

    var route = FeatureToggleRouteBuilder.WithUrl("offers/offer-{title}_{id}")
                .WithFeatureToogle((r) => r.HttpContext.Request.IsSecureConnection)
                .WithCurrentPageRoute("~/Pages/Offers/Details.aspx", true, 
                    defaults: null, 
                    constraints: new { id = @"\d+" })
                .WithExperimentalMvcRoute(
                    defaults: new { controller = "Offers", action = "Details" }, 
                    constraints: new { id = @"\d+" })
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
                .WithCurrentMvcRoute(
                    defaults: new { controller = "Offers", action = "Details" },
                    constraints: new { id = @"\d+" })
                .WithExperimentalPageRoute("~/Pages/Offers/Details.aspx", true,
                    defaults: null,
                    constraints: new { id = @"\d+" })                
                .Build();
