namespace TMS.Core;

public class Carrier
{
    public int Id { get; set; }
    public string Name { get; set; }
    public CarrierType Type { get; set; }
    public int Capacity { get; set; }
}