namespace TMS.Core;

public class DataContext
    : DbContext(new DbContextOptionsBuilder<DataContext>()
        .UseMySql(ServerVersion.AutoDetect(""))
        .Options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>().HasData([
            new User { Name = "admin", Password = "admin", Role = UserRole.Admin },
            new User { Name = "buyer", Password = "buyer", Role = UserRole.Buyer },
            new User { Name = "planner", Password = "planner", Role = UserRole.Planner },
        ]);
    }

    public DbSet<Address> Addresses => Set<Address>();
    public DbSet<Carrier> Carriers => Set<Carrier>();
    public DbSet<City> Cities => Set<City>();
    public DbSet<Country> Countries => Set<Country>();
    public DbSet<Customer> Customers => Set<Customer>();
    public DbSet<Invoice> Invoices => Set<Invoice>();
    public DbSet<Log> Logs => Set<Log>();
    public DbSet<Order> Orders => Set<Order>();
    public DbSet<Route> Routes => Set<Route>();
    public DbSet<Setting> Settings => Set<Setting>();
    public DbSet<Trip> Trips => Set<Trip>();
    public DbSet<User> Users => Set<User>();
}