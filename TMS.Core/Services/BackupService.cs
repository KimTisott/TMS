namespace TMS.Core;

public static class BackupService
{
    private const string FileExtension = ".sql";

    public static bool Run()
    {
        try
        {
            using TMSContext tms = new();
            using MySqlConnection connection = new(tms.Database.GetConnectionString());
            using MySqlCommand command = new();
            using MySqlBackup backup = new(command);
            command.Connection = connection;
            connection.Open();
            var directory = ConfigurationService.Get(ConfigurationKey.BackupDirectory);
            var filename = DateTime.Now.Clean();
            var path = Path.Combine(directory, filename, FileExtension).Remove(Path.GetInvalidPathChars());
            backup.ExportToFile(path);
            connection.Close();
            return true;
        }
        catch (Exception exception)
        {
            LoggingService.Write(DateTime.Now.Clean(), exception.ToString());
            return false;
        }
    }
}