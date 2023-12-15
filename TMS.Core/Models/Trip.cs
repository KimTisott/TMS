namespace TMS.Core;

public class Trip
{
    public int Id { get; set; }
    public Order Order { get; set; }
    public Carrier Carrier { get; set; }
    public ICollection<Route> Routes { get; set; } = new List<Route>();
}