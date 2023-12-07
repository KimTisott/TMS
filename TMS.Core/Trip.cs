namespace TMS.Core;

public class Trip : Entity
{
    public Order Order { get; set; }
    public Carrier Carrier { get; set; }
    public Route Route { get; set; }
}