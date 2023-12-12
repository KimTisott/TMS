namespace TMS.Core;

public class User : Entity
{
    public const int NameLength = 15;
    public const int PasswordLength = 15;

    [StringLength(NameLength)]
    public string Name { get; set; }

    [StringLength(PasswordLength)]
    public string Password { get; set; }

    public UserRole Role { get; set; }

    public bool Login() =>
        AuthenticationService.Login(Name, Password);

    public bool Backup() =>
        Role == UserRole.Admin && BackupService.Run();

    public string GetConfiguration(ConfigurationType configurationType)
    {
        if (Role == UserRole.Admin)
        {
            return ConfigurationService.Get(configurationType);
        }

        return null;
    }

    public bool SetConfiguration(ConfigurationType configurationType, string value)
    {
        if (Role == UserRole.Admin)
        {
            return ConfigurationService.Set(configurationType, value);
        }

        return false;
    }

    public string ReviewLog(Log log)
    {
        if (Role == UserRole.Admin)
        {
            return LoggingService.Read(log);
        }

        return null;
    }

    public Order ViewOrder(DataContext context, int orderId)
    {
        if (Role == UserRole.Planner)
        {
            return context.Orders.Find(orderId);
        }

        return null;
    }

    public Invoice GenerateInvoice(Order[] orders, Customer customer)
    {
        if (Role == UserRole.Buyer)
        {
            return new()
            {
                Price = orders.Sum(order => order.Price),
                Filename = $"Invoice_{Guid.NewGuid}",
                Customer = customer,
                Orders = orders
            };
        }

        return null;
    }
}