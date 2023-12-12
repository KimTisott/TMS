namespace TMS.Core;

public static class LoggingService
{
    private const string FileExtension = ".txt";

    public static bool Write(string filename, string contents)
    {
        if (string.IsNullOrWhiteSpace(filename))
        {
            return false;
        }

        try
        {
            using DataContext context = new();
            var directory = context.Settings.First(setting => setting.Key == SettingKey.LogDirectory);
            Directory.CreateDirectory(directory.Value);
            var path = Path.Combine(directory.Value, filename + FileExtension).Remove(Path.GetInvalidPathChars());
            File.WriteAllText(path, contents);
            Log log = new() { Filename = path };
            context.Add(log);
            context.SaveChanges();
            return true;
        }
        catch
        {
            return false;
        }
    }

    public static string Read(Log log)
    {
        using DataContext context = new();
        var directory = context.Settings.First(setting => setting.Key == SettingKey.LogDirectory);

        return File.ReadAllText(Path.Combine(directory.Value, log.Filename + FileExtension));
    }
}