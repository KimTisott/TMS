namespace TMS.Core;

public static class ConfigurationService
{
    private static readonly NameValueCollection _settings;

    public static string Get(ConfigurationType configuration) => _settings[configuration.ToString()];
    public static bool Set(ConfigurationType configuration, string value) => (_settings[configuration.ToString()] = value) != null;

    static ConfigurationService()
    {
        _settings = ConfigurationManager.AppSettings;
    }
}