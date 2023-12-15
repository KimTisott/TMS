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

        modelBuilder.Entity<Country>().HasData(new Country[]
        {
            new() { Id = 1, Name = "Canada" },
            new() { Id = 2, Name = "United States" }
        });

        City ottawa = new() { Id = 1, CountryId = 1, Name = "Ottawa" };
        City kingston = new() { Id = 2, CountryId = 1, Name = "Kingston" };
        City belleville = new() { Id = 3, CountryId = 1, Name = "Belleville" };
        City oshawa = new() { Id = 4, CountryId = 1, Name = "Oshawa" };
        City toronto = new() { Id = 5, CountryId = 1, Name = "Toronto" };
        City hamilton = new() { Id = 6, CountryId = 1, Name = "Hamilton" };
        City london = new() { Id = 7, CountryId = 1, Name = "London" };
        City windsor = new() { Id = 8, CountryId = 1, Name = "Windsor" };
        
        modelBuilder.Entity<City>().HasData(ottawa, kingston, belleville, oshawa, toronto, hamilton, london, windsor);

        modelBuilder.Entity<Carrier>().HasData(new Carrier[]
        {
            new() { Id = 1, Name = "Planet Express", FTLAvailability = 50, LTLAvailability = 640, FTLRate = 5.21, LTLRate = 0.3621, ReeferCharge = 0.08, Depots = [ windsor, hamilton, oshawa, belleville, ottawa ] },
            new() { Id = 2, Name = "Schooner's", FTLAvailability = 18, LTLAvailability = 98, FTLRate = 5.05, LTLRate = 0.3434, ReeferCharge = 0.07, Depots = [ london, toronto, kingston ] },
            new() { Id = 3, Name = "Tillman Transport", FTLAvailability = 24, LTLAvailability = 35, FTLRate = 5.11, LTLRate = 0.3012, ReeferCharge = 0.09, Depots = [ windsor, london, hamilton ] },
            new() { Id = 4, Name = "We Haul", FTLAvailability = 11, LTLAvailability = 0, FTLRate = 5.2, LTLRate = 0, ReeferCharge = 0.065, Depots = [ ottawa, toronto ] },
        });

        modelBuilder.Entity<Rate>().HasData(new Rate[]
        {
            new() { Type = RateType.FTLAvgRate, Cost = 4.985m },
            new() { Type = RateType.LTLAvgRate, Cost = 0.2995m }
        });

        modelBuilder.Entity<Route>().HasData(new Route[]
        {
            new() { Id = 1, FromId = 1, ToId = 2, Distance = 196, Time = 2.5 },
            new() { Id = 2, FromId = 2, ToId = 3, Distance = 82, Time = 1.2 },
            new() { Id = 3, FromId = 3, ToId = 4, Distance = 134, Time = 1.65 },
            new() { Id = 4, FromId = 4, ToId = 5, Distance = 60, Time = 1.3 },
            new() { Id = 5, FromId = 5, ToId = 6, Distance = 68, Time = 1.25 },
            new() { Id = 6, FromId = 6, ToId = 7, Distance = 128, Time = 1.75 },
            new() { Id = 7, FromId = 7, ToId = 8, Distance = 191, Time = 2.5 }
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
    public DbSet<Rate> Rates => Set<Rate>();
    public DbSet<Route> Routes => Set<Route>();
    public DbSet<Trip> Trips => Set<Trip>();
    public DbSet<User> Users => Set<User>();
}