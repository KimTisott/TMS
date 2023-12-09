namespace TMS.Core;

public class Customer : Entity
{
    public int MarketplaceId { get; set; }
    public Address Address { get; set; }
}