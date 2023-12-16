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
    public ICollection<CarrierCity> Depots { get; } = new List<CarrierCity>();

    public string DepotNames => string.Join(", ", Depots?.Select(depot => depot.City?.Name));
}