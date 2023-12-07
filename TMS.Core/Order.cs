namespace TMS.Core;

public class Order : Entity
{
    public decimal Price { get; set; }
    public bool IsCompleted { get; set; }
    public int Capacity { get; set; }
}