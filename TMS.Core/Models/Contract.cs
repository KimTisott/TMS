namespace TMS.Core;

public class Contract
{
    public string ClientName { get; set; }

    public JobType JobType { get; set; }

    public int Quantity { get; set; }

    public string Origin { get; set; }

    public string Destination { get; set; }

    public VanType VanType { get; set; }
}