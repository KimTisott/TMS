namespace TMS.Core;

public class Route
{
    public int Id { get; set; }
    public int FromId { get; set; }
    public City From { get; set; }
    public int ToId { get; set; }
    public City To { get; set; }
    public int Distance { get; set; }
    public double Time { get; set; }
}