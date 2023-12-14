namespace TMS.Core;

public class MarketplaceContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);

        optionsBuilder.UseMySql(ConfigurationService.MarketplaceConnectionString, ServerVersion.AutoDetect(ConfigurationService.MarketplaceConnectionString));
    }

    public DbSet<Contract> Contracts => Set<Contract>();
}