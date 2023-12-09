namespace TMS.Core;

public class Route : Entity
{
    public City From { get; set; }
    public City To { get; set; }
}