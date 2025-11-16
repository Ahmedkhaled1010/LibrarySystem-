var builder = WebApplication.CreateBuilder(args);

builder.Services.AddReverseProxy()
    .LoadFromMemory(
        new[]
        {
            new Yarp.ReverseProxy.Configuration.RouteConfig
            {
                //route هيظهر للfront
                RouteId = "Library",
                //cluster بتاع الservices اللي هيروح عليها
                ClusterId = "LibraryCluster",
                Match = new() { Path = "/Library/{**catch-all}" },
                Transforms = new List<Dictionary<string, string>>
                {
                    new Dictionary<string, string>
                    {
                        { "PathRemovePrefix", "/Library" }


                    }
                }
            },
             new Yarp.ReverseProxy.Configuration.RouteConfig
            {
                RouteId = "Notification",
                ClusterId = "NotificationCluster",
                Match = new() { Path = "/Notification/{**catch-all}" },
                Transforms = new List<Dictionary<string, string>>
                {
                    new Dictionary<string, string>
                    {
                        { "PathRemovePrefix", "/Notification" }
                    }
                }
            },
              new Yarp.ReverseProxy.Configuration.RouteConfig
            {
                RouteId = "Email",
                ClusterId = "EmailCluster",
                Match = new() { Path = "/Email/{**catch-all}" },
                Transforms = new List<Dictionary<string, string>>
                {
                    new Dictionary<string, string>
                    {
                        { "PathRemovePrefix", "/Email" }
                    }
                }
            },
               new Yarp.ReverseProxy.Configuration.RouteConfig
            {
                RouteId = "Review",
                ClusterId = "ReviewCluster",
                Match = new() { Path = "/Review/{**catch-all}" },
                Transforms = new List<Dictionary<string, string>>
                {
                    new Dictionary<string, string>
                    {
                        { "PathRemovePrefix", "/Review" }
                    }
                }
            }, new Yarp.ReverseProxy.Configuration.RouteConfig
            {
                RouteId = "Payment",
                ClusterId = "PaymentCluster",
                Match = new() { Path = "/Payment/{**catch-all}" },
                Transforms = new List<Dictionary<string, string>>
                {
                    new Dictionary<string, string>
                    {
                        { "PathRemovePrefix", "/Payment" }
                    }
                }
            }
        },
        new[]
        {
            new Yarp.ReverseProxy.Configuration.ClusterConfig
            {
                ClusterId = "LibraryCluster",
                Destinations = new Dictionary<string, Yarp.ReverseProxy.Configuration.DestinationConfig>
                {
                    { "dest1", new() { Address = "https://localhost:7164" } }
                }
            },
              new Yarp.ReverseProxy.Configuration.ClusterConfig
            {
                ClusterId = "NotificationCluster",
                Destinations = new Dictionary<string, Yarp.ReverseProxy.Configuration.DestinationConfig>
                {
                    { "dest1", new() { Address = "https://localhost:7021" } }
                }
            },
                new Yarp.ReverseProxy.Configuration.ClusterConfig
            {
                ClusterId = "EmailCluster",
                Destinations = new Dictionary<string, Yarp.ReverseProxy.Configuration.DestinationConfig>
                {
                    { "dest1", new() { Address = "https://localhost:7155" } }
                }
            },
                  new Yarp.ReverseProxy.Configuration.ClusterConfig
            {
                ClusterId = "ReviewCluster",
                Destinations = new Dictionary<string, Yarp.ReverseProxy.Configuration.DestinationConfig>
                {
                    { "dest1", new() { Address = "https://localhost:7178" } }
                }
            },
                    new Yarp.ReverseProxy.Configuration.ClusterConfig
            {
                ClusterId = "PaymentCluster",
                Destinations = new Dictionary<string, Yarp.ReverseProxy.Configuration.DestinationConfig>
                {
                    { "dest1", new() { Address = "https://localhost:7207" } }
                }
            }
        });

var app = builder.Build();
app.Use(async (context, next) =>
{
    // Forward Authorization header to destination
    if (context.Request.Headers.ContainsKey("Authorization"))
    {
        context.Request.Headers.TryGetValue("Authorization", out var token);
        context.Request.Headers["Authorization"] = token;
    }

    await next();
});


app.MapReverseProxy();

app.Run();
