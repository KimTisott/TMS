namespace TMS.Core;

public class User
{
    public const int NameLength = 15;
    public const int PasswordLength = 15;

    [Key, StringLength(NameLength)]
    public string Name { get; set; }

    [StringLength(PasswordLength)]
    public string Password { get; set; }

    public UserRole Role { get; set; }

    public bool Login() =>
        AuthenticationService.Login(Name, Password);

    public void Logout() =>
        AuthenticationService.Logout();

    public bool Backup() =>
        Role == UserRole.Admin && BackupService.Run();

    public string GetConfiguration(ConfigurationKey configurationType)
    {
        if (Role == UserRole.Admin)
        {
            return ConfigurationService.Get(configurationType);
        }

        return null;
    }

    public string SetConfiguration(ConfigurationKey configurationType, string value)
    {
        if (Role == UserRole.Admin)
        {
            return ConfigurationService.Set(configurationType, value);
        }

        return null;
    }

    public string ReviewLog(Log log)
    {
        if (Role == UserRole.Admin)
        {
            return LoggingService.Read(log);
        }

        return null;
    }

    public Order ViewOrder(TMSContext tms, int orderId)
    {
        if (Role == UserRole.Planner)
        {
            return tms.Orders.Find(orderId);
        }

        return null;
    }

    public Invoice GenerateInvoice(Order order)
    {
        if (Role == UserRole.Buyer)
        {
            return new()
            {
                CreatedAt = DateTime.Now,
                Contents = $"Cost: {order.Cost:C}",
                OrderId = order.Id
            };
        }

        return null;
    }
}