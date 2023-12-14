namespace TMS.Core;

public class Invoice
{
    public int Id { get; set; }
    public decimal Price { get; set; }
    public string Filename { get; set; }
    public Customer Customer { get; set; }
    public ICollection<Order> Orders { get; set; } = new List<Order>();
}