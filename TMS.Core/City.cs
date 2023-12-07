namespace TMS.Core;

public class City : Entity
{
    public string Name { get; set; }
    public Country Country { get; set; }
}