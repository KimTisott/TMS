namespace TMS.Core;

public class TMSContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);

        optionsBuilder.UseMySql(ConfigurationService.ConnectionString, ServerVersion.AutoDetect(ConfigurationService.ConnectionString));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Configuration>().HasData(new Configuration[]
        {
            new(ConfigurationKey.BackupDirectory, "Backups", ConfigurationType.Database),
            new(ConfigurationKey.LogDirectory, "Logs", ConfigurationType.Database),
            new(ConfigurationKey.MarketplaceDatabase, "cmp", ConfigurationType.Database),
            new(ConfigurationKey.MarketplacePassword, "Snodgr4ss!", ConfigurationType.Database),
            new(ConfigurationKey.MarketplacePort, "3306", ConfigurationType.Database),
            new(ConfigurationKey.MarketplaceServer, "159.89.117.198", ConfigurationType.Database),
            new(ConfigurationKey.MarketplaceUsername, "DevOSHT", ConfigurationType.Database)
        });

        modelBuilder.Entity<User>().HasData(new User[] {
            new() { Name = "admin", Password = "admin", Role = UserRole.Admin },
            new() { Name = "buyer", Password = "buyer", Role = UserRole.Buyer },
            new() { Name = "planner", Password = "planner", Role = UserRole.Planner },
        });
    }

    public DbSet<Address> Addresses => Set<Address>();
    public DbSet<Carrier> Carriers => Set<Carrier>();
    public DbSet<City> Cities => Set<City>();
    public DbSet<Configuration> Configurations => Set<Configuration>();
    public DbSet<Country> Countries => Set<Country>();
    public DbSet<Customer> Customers => Set<Customer>();
    public DbSet<Invoice> Invoices => Set<Invoice>();
    public DbSet<Log> Logs => Set<Log>();
    public DbSet<Order> Orders => Set<Order>();
    public DbSet<Route> Routes => Set<Route>();
    public DbSet<Trip> Trips => Set<Trip>();
    public DbSet<User> Users => Set<User>();
}