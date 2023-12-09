namespace TMS.Core;

public class Order : Entity
{
    public decimal Price { get; set; }
    public bool IsCompleted { get; set; }
    public int Capacity { get; set; }
    public Invoice Invoice { get; set; }
    public ICollection<Trip> Trips { get; } = new List<Trip>();
}