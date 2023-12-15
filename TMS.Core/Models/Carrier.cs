namespace TMS.Core;

public class Carrier
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int FTLAvailability { get; set; }
    public int LTLAvailability { get; set; }
    public double FTLRate { get; set; }
    public double LTLRate { get; set; }
    public double ReeferCharge { get; set; }
    public ICollection<City> Depots { get; set; } = new List<City>();
}