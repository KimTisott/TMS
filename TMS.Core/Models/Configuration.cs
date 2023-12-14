namespace TMS.Core;

public class Configuration
{
    [NotMapped]
    public ConfigurationType Type { get; set; }

    [Key]
    public ConfigurationKey Key { get; set; }

    public string Value { get; set; }

    public Configuration()
    {

    }

    public Configuration(ConfigurationKey key, string value, ConfigurationType type)
    {
        Key = key;
        Value = value;
        Type = type;
    }

    public string Reload()
    {
        if (Type == ConfigurationType.Application)
        {
            return Value = ConfigurationManager.AppSettings[Key.ToString()];
        }
        else
        {
            using TMSContext tms = new();
            return Value = tms.Configurations.Find(Key)?.Value;
        }
    }

    public void Save()
    {
        if (Type == ConfigurationType.Application)
        {
            var configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            configuration.AppSettings.Settings[Key.ToString()].Value = Value;
            configuration.Save(ConfigurationSaveMode.Full, true);
            ConfigurationManager.RefreshSection("appSettings");
        }
        else if (Type == ConfigurationType.Database)
        {
            using TMSContext tms = new();
            var configuration = tms.Configurations.Find(Key);
            configuration.Value = Value;
            tms.SaveChanges();
        }
    }
}