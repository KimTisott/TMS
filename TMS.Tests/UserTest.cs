namespace TMS.Tests;

public class UserTest
{
    [Fact]
    public void DefaultLogin()
    {
        using DataContext context = new();
        Assert.NotNull(User.Login(context, "admin", "admin"));
        Assert.NotNull(User.Login(context, "buyer", "buyer"));
        Assert.NotNull(User.Login(context, "planner", "planner"));
    }

    [Fact]
    public void Backup()
    {
        using DataContext context = new();
        var buyer = context.Users.First(user => user.Role == UserRole.Buyer);
        Assert.False(buyer.Backup(context));
    }

    [Fact]
    public void ViewOrder()
    {
        using DataContext context = new();
        var planner = context.Users.First(user => user.Role == UserRole.Planner);
        var order = context.Orders.Single();
        Assert.Equal(order, planner.ViewOrder(context, order.Id));
    }

    [Fact]
    public void GenerateInvoice()
    {
        using DataContext context = new();
        var buyer = context.Users.First(user => user.Role == UserRole.Buyer);
        var orders = context.Orders.Where(order => order.IsCompleted).ToArray();
        var customer = context.Customers.First();
        Assert.NotNull(buyer.GenerateInvoice(orders, customer));
    }
}