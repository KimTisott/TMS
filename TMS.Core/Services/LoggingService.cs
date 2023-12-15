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
            var directory = ConfigurationService.Get(ConfigurationKey.LogDirectory);
            Directory.CreateDirectory(directory);
            var path = Path.Combine(directory, filename + FileExtension).Remove(Path.GetInvalidPathChars());
            File.WriteAllText(path, contents);
            Log log = new() { Filename = path };
            using TMSContext tms = new();
            tms.Add(log);
            tms.SaveChanges();
            return true;
        }
        catch
        {
            return false;
        }
    }

    public static string Read(Log log)
    {
        var directory = ConfigurationService.Get(ConfigurationKey.LogDirectory);

        return File.ReadAllText(Path.Combine(directory, log.Filename + FileExtension));
    }

    public static HashSet<Log> ReadAll()
    {
        using TMSContext tms = new();
        var logs = tms.Logs.ToHashSet();

        foreach (var log in logs)
        {
            log.Contents = File.ReadAllText(log.Filename);
        }

        return logs;
    }
}