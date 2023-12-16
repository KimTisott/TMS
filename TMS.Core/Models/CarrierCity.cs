namespace TMS.Core;

public class CarrierCity
{
    public int CarrierId { get; set; }
    public Carrier Carrier { get; set; }
    public int CityId { get; set; }
    public City City { get; set; }
}