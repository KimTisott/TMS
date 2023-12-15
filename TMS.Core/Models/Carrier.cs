namespace TMS.Core;

public class Carrier
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int DepotId { get; set; }
    public City Depot { get; set; }
    public int FTLAvailability { get; set; }
    public int LTLAvailability { get; set; }
    public decimal FTLRate { get; set; }
    public decimal LTLRate { get; set; }
    public int ReeferCharge { get; set; }
}