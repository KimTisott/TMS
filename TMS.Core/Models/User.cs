namespace TMS.Core;

public class User : Entity
{
    public string Name { get; set; }
    public string Password { get; set; }
    public UserRole Role { get; set; }

    public static User Login(DataContext context, string name, string password)
        => context.Users.First(user => user.Name == name && user.Password == password);

    public bool Backup(DataContext context)
    {
        if (Role == UserRole.Admin)
        {
            using MySqlConnection connection = new(context.Database.GetConnectionString());
            using MySqlCommand command = new();
            using MySqlBackup backup = new(command);
            command.Connection = connection;
            connection.Open();
            var directory = context.Settings.First(setting => setting.Key == SettingKey.BackupDirectory)?.Value ?? "./Backups";
            var now = DateTime.Now;
            backup.ExportToFile(Path.Combine(directory, now.ToString()));
            connection.Close();
            return true;
        }

        return false;
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