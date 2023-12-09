namespace TMS.Core;

public class Address : Entity
{
    public string Street { get; set; }
    public City City { get; set; }
}