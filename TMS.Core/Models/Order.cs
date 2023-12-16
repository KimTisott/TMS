namespace TMS.Core;

public class Order
{
    public int Id { get; set; }

    public bool IsCompleted { get; set; }

    public JobType JobType { get; set; }

    public VanType VanType { get; set; }

    public int Quantity { get; set; }

    public int CustomerId { get; set; }

    public Customer Customer { get; set; }

    public ICollection<City> Cities { get; } = new List<City>();
    
    public ICollection<Carrier> Carriers { get; } = new List<Carrier>();

    public ICollection<Route> Routes { get; } = new List<Route>();
    
    public ICollection<Trip> Trips { get; } = new List<Trip>();

    public string CityNames => string.Join(", ", Cities.Select(city => city.Name));

    public int Distance => Trips.Sum(trip => trip.Distance);

    public double Cost
    {
        get
        {
            double cost = 0;
            if (JobType == JobType.FullTruckLoad)
            {
                cost += Trips.Sum(trip => trip.Distance * trip.Carrier.FTLRate + 
                    trip.Carrier.ReeferCharge * trip.Distance * trip.Carrier.FTLRate / 100);
            }
            else if (JobType == JobType.LessThanTruckload)
            {
                cost += Trips.Sum(trip => trip.Distance * trip.Carrier.LTLRate +
                    trip.Carrier.ReeferCharge * trip.Distance * trip.Carrier.LTLRate / 100) * Quantity;
            }

            if (VanType == VanType.Reefer)
            {
                cost += Trips.Sum(trip => trip.Carrier.ReeferCharge);
            }
            return cost;
        }
    }

    public double Time
    {
        get
        {
            double time = 0;
            time = Trips.Sum(trip => trip.Time);
            time += 4;
            if (JobType == JobType.LessThanTruckload)
            {
                var routeCount = Trips.Sum(trip => trip.Routes.Count);
                if (routeCount > 1)
                {
                    time += 2 * (routeCount - 1);
                }
            }
            return time;
        }
    }
}