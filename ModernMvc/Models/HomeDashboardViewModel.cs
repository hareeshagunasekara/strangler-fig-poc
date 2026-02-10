namespace ModernMvc.Models;

public class HomeDashboardViewModel
{
    public int TotalOrders { get; set; }
    public int PendingCount { get; set; }
    public int ProcessingCount { get; set; }
    public int ShippedCount { get; set; }
    public int DelayedCount { get; set; }
    public IList<OrderViewModel> RecentOrders { get; set; } = new List<OrderViewModel>();
}
