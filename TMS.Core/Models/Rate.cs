namespace TMS.Core;

public class Rate
{
    [Key]
    public RateType Type { get; set; }
    public decimal Cost { get; set; }
}