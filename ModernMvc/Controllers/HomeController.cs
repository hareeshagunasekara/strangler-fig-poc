using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ModernMvc.Models;

namespace ModernMvc.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        var recentOrders = new List<OrderViewModel>
        {
            new() { OrderId = 1001, Status = "Processing" },
            new() { OrderId = 1002, Status = "Shipped" },
            new() { OrderId = 1003, Status = "Pending" }
        };
        var model = new HomeDashboardViewModel
        {
            TotalOrders = 24,
            PendingCount = 5,
            ProcessingCount = 8,
            ShippedCount = 9,
            DelayedCount = 2,
            RecentOrders = recentOrders
        };
        return View(model);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

