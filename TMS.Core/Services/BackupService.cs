namespace TMS.Core;

public static class BackupService
{
    private const string FileExtension = ".sql";

    public static bool Run()
    {
        try
        {
            using DataContext context = new();
            using MySqlConnection connection = new(context.Database.GetConnectionString());
            using MySqlCommand command = new();
            using MySqlBackup backup = new(command);
            command.Connection = connection;
            connection.Open();
            var directory = context.Settings.First(setting => setting.Key == SettingKey.BackupDirectory);
            var filename = DateTime.Now.Clean();
            var path = Path.Combine(directory.Value, filename, FileExtension).Remove(Path.GetInvalidPathChars());
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