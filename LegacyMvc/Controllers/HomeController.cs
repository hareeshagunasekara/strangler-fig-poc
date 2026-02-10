using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using LegacyMvc.Models;

namespace LegacyMvc.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        var orders = new List<OrderViewModel>
        {
            new() { OrderId = 1001, Status = "Processing" },
            new() { OrderId = 1002, Status = "Shipped" }
        };
        var dashboard = new HomeDashboardViewModel
        {
            TotalOrders = orders.Count,
            PendingCount = orders.Count(o => o.Status == "Processing"),
            ShippedCount = orders.Count(o => o.Status == "Shipped"),
            RecentOrders = orders
        };
        return View(dashboard);
    }

    public IActionResult Contact()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

