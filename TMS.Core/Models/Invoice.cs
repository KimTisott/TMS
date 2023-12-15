namespace TMS.Core;

public class Invoice
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public string Contents { get; set; }
    public int OrderId { get; set; }
    public Order Order { get; set; }
}