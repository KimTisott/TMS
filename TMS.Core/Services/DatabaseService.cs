namespace TMS.Core;

public static class DatabaseService
{
    public static string ConnectionString { get; private set; }

    static DatabaseService()
    {
        var database = ConfigurationService.Get(ConfigurationType.Database);
        var password = ConfigurationService.Get(ConfigurationType.Password);
        var port = ConfigurationService.Get(ConfigurationType.Port);
        var server = ConfigurationService.Get(ConfigurationType.Server);
        var uid = ConfigurationService.Get(ConfigurationType.Uid);

        ConnectionString =
            $"{ConfigurationType.Database}={database};" +
            $"{ConfigurationType.Password}={password};" +
            $"{ConfigurationType.Port}={port};" +
            $"{ConfigurationType.Server}={server};" +
            $"{ConfigurationType.Uid}={uid}";

        using DataContext context = new();
        context.Database.EnsureCreated();
    }
}