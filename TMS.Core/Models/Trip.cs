namespace TMS.Core;

public class Trip
{
    public int Id { get; set; }

    public int CarrierId { get; set; }

    public Carrier Carrier { get; set; }

    public ICollection<Route> Routes { get; set; } = new List<Route>();

    public int Distance => Routes.Sum(route => route.Distance);
    
    public double Time
    {
        get
        {
            double time = 0;
            time = Routes.Sum(route => route.Time);
            var days = time / 8;
            if (days > 0)
            {
                time += days * 12;
            }
            return time;
        }
    }
}