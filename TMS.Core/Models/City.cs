namespace TMS.Core;

public class City
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int CountryId { get; set; }
    public Country Country { get; set; }
    public ICollection<Order> Orders { get; } = new List<Order>();
}