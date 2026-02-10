using Microsoft.Extensions.Configuration;
using Yarp.ReverseProxy.Configuration;

namespace ProxyFacade.Configuration;

/// <summary>
/// YARP config filter that applies the UseModernHome rollback toggle.
/// When UseModernHome is false, the home routes (/, /home) are sent to legacy instead of modern.
/// </summary>
public class UseModernHomeConfigFilter : IProxyConfigFilter
{
    private readonly IConfiguration _configuration;

    public UseModernHomeConfigFilter(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public ValueTask<ClusterConfig> ConfigureClusterAsync(ClusterConfig cluster, CancellationToken cancel)
    {
        return new ValueTask<ClusterConfig>(cluster);
    }

    public ValueTask<RouteConfig> ConfigureRouteAsync(RouteConfig route, ClusterConfig? cluster, CancellationToken cancel)
    {
        var useModernHome = _configuration.GetValue<bool>("UseModernHome", true);

        // Only override the home routes when rolling back to legacy.
        if (!useModernHome && (route.RouteId == "route-home" || route.RouteId == "route-home-alt"))
        {
            return new ValueTask<RouteConfig>(route with { ClusterId = "legacy" });
        }

        return new ValueTask<RouteConfig>(route);
    }
}
