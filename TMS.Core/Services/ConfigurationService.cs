namespace TMS.Core;

public static class ConfigurationService
{
    public static readonly HashSet<Configuration> Configurations;
    public static readonly string ConnectionString;
    public static readonly string MarketplaceConnectionString;

    public static string Get(ConfigurationKey key)
        => Configurations.First(config => config.Key == key).Value;

    public static string Set(ConfigurationKey key, string value)
    {
        var configuration = Configurations.First(config => config.Key == key);
        configuration.Value = value;
        configuration.Save();
        return configuration.Reload();
    }

    static ConfigurationService()
    {
        Configurations = [];

        var appSettings = ConfigurationManager.AppSettings;
        foreach (var appConfig in appSettings)
        {
            var configurationString = appConfig.ToString();
            if (Enum.TryParse<ConfigurationKey>(configurationString, out var key))
            {
                Configurations.Add(new(key, appSettings[configurationString], ConfigurationType.Application));
            }
        }

        ConnectionString =
            $"database={Get(ConfigurationKey.Database)};" +
            $"password={Get(ConfigurationKey.Password)};" +
            $"port={Get(ConfigurationKey.Port)};" +
            $"server={Get(ConfigurationKey.Server)};" +
            $"uid={Get(ConfigurationKey.Username)}";

        using TMSContext tms = new();
        tms.Database.EnsureCreated();

        foreach (var dbConfig in tms.Configurations)
        {
            Configurations.Add(new(dbConfig.Key, dbConfig.Value, ConfigurationType.Database));
        }

        MarketplaceConnectionString =
            $"database={Get(ConfigurationKey.MarketplaceDatabase)};" +
            $"password={Get(ConfigurationKey.MarketplacePassword)};" +
            $"port={Get(ConfigurationKey.MarketplacePort)};" +
            $"server={Get(ConfigurationKey.MarketplaceServer)};" +
            $"uid={Get(ConfigurationKey.MarketplaceUsername)}";
    }
}