namespace TMS.Core;

public class Carrier : Entity
{
    public string Name { get; set; }
    public CarrierType Type { get; set; }
    public int Capacity { get; set; }
}