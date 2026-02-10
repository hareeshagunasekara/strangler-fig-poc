namespace LegacyMvc.Models;

public class HomeDashboardViewModel
{
    public string SystemName { get; set; } = "Order Management System (Legacy)";
    public string WelcomeText { get; set; } = "Welcome back";
    public int TotalOrders { get; set; }
    public int PendingCount { get; set; }
    public int ShippedCount { get; set; }
    public IList<OrderViewModel> RecentOrders { get; set; } = new List<OrderViewModel>();
}
