namespace TMS.Core;

public class DataContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);

        optionsBuilder.UseMySql(DatabaseService.ConnectionString, ServerVersion.AutoDetect(DatabaseService.ConnectionString));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Setting>().HasData(new Setting[]
        {
            new() { Id = 1, Key = SettingKey.BackupDirectory, Value = "Backups"},
            new() { Id = 2, Key = SettingKey.LogDirectory, Value = "Logs"}
        });

        modelBuilder.Entity<User>().HasData(new User[] {
            new() { Id = 1, Name = "admin", Password = "admin", Role = UserRole.Admin },
            new() { Id = 2, Name = "buyer", Password = "buyer", Role = UserRole.Buyer },
            new() { Id = 3, Name = "planner", Password = "planner", Role = UserRole.Planner },
        });
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