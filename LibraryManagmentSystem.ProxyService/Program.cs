using Yarp.ReverseProxy;

var builder = WebApplication.CreateBuilder(args);

// إضافة خدمة YARP فقط
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
                Match = new() { Path = "/Library/{**catch-all}" }
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
            }
        });

var app = builder.Build();

// تفعيل الـ Proxy
app.MapReverseProxy();

app.Run();
