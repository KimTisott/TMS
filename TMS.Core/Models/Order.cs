namespace TMS.Core;

public class Order
{
    public int Id { get; set; }
    public decimal? Price { get; set; }
    public bool IsCompleted { get; set; }
    public int CustomerId { get; set; }
    public Customer Customer { get; set; }
    public ICollection<City> Cities { get; set; } = new List<City>();
    public ICollection<Carrier> Carriers { get; } = new List<Carrier>();
    public ICollection<Trip> Trips { get; } = new List<Trip>();

    public string CityNames => string.Join(", ", Cities.Select(city => city.Name));

    public static decimal CalculatePrice()
    {
        var price = 0;

        return price;
    }
}