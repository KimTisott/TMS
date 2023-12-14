namespace TMS.Tests;

public class UserTest
{
    [Theory]
    [InlineData("admin", "admin")]
    [InlineData("buyer", "buyer")]
    [InlineData("planner", "planner")]
    public void Login_ValidNameAndPassword_ReturnsTrue(string username, string password)
    {
        User user = new()
        {
            Name = username,
            Password = password
        };

        var result = user.Login();

        Assert.True(result);
    }

    [Theory]
    [InlineData(null, null)]
    [InlineData("", "")]
    [InlineData("admin", "buyer")]
    [InlineData("buyer", "admin")]
    public void Login_InvalidNameAndPassword_ReturnsFalse(string username, string password)
    {
        User user = new()
        {
            Name = username,
            Password = password
        };

        var result = user.Login();

        Assert.False(result);
    }

    [Theory]
    [InlineData(UserRole.Admin)]
    public void Backup_ValidUserRole_ReturnsTrue(UserRole userRole)
    {
        using TMSContext tms = new();
        User user = tms.Users.First(user => user.Role == userRole);

        var result = user.Backup();

        Assert.True(result);
    }

    [Theory]
    [InlineData(UserRole.Buyer)]
    [InlineData(UserRole.Planner)]
    public void Backup_InvalidUserRole_ReturnsFalse(UserRole userRole)
    {
        using TMSContext tms = new();
        User user = tms.Users.First(user => user.Role == userRole);

        var result = user.Backup();

        Assert.False(result);
    }

    [Theory]
    [InlineData(UserRole.Admin, ConfigurationKey.Database, "TMS")]
    [InlineData(UserRole.Admin, ConfigurationKey.Password, "root")]
    [InlineData(UserRole.Admin, ConfigurationKey.Port, "3306")]
    [InlineData(UserRole.Admin, ConfigurationKey.Server, "127.0.0.1")]
    [InlineData(UserRole.Admin, ConfigurationKey.Username, "root")]
    public void SetConfiguration_ValidUserRole_ReturnsValue(UserRole userRole, ConfigurationKey configurationType, string value)
    {
        using TMSContext tms = new();
        User user = tms.Users.First(user => user.Role == userRole);

        var result = user.SetConfiguration(configurationType, value);

        Assert.NotNull(result);
    }

    [Theory]
    [InlineData(UserRole.Buyer, ConfigurationKey.Database, "TMS")]
    [InlineData(UserRole.Planner, ConfigurationKey.Database, "TMS")]
    public void SetConfiguration_InvalidUserRole_ReturnsNull(UserRole userRole, ConfigurationKey configurationType, string value)
    {
        using TMSContext tms = new();
        User user = tms.Users.First(user => user.Role == userRole);

        var result = user.SetConfiguration(configurationType, value);

        Assert.Null(result);
    }

    [Theory]
    [InlineData(UserRole.Admin, ConfigurationKey.Database)]
    [InlineData(UserRole.Admin, ConfigurationKey.Password)]
    [InlineData(UserRole.Admin, ConfigurationKey.Port)]
    [InlineData(UserRole.Admin, ConfigurationKey.Server)]
    [InlineData(UserRole.Admin, ConfigurationKey.Username)]
    public void GetConfiguration_ValidUserRole_ReturnsConfiguration(UserRole userRole, ConfigurationKey configurationType)
    {
        using TMSContext tms = new();
        User user = tms.Users.First(user => user.Role == userRole);

        var result = user.GetConfiguration(configurationType);

        Assert.NotNull(result);
    }

    [Theory]
    [InlineData(UserRole.Buyer, ConfigurationKey.Database)]
    [InlineData(UserRole.Planner, ConfigurationKey.Database)]
    public void GetConfiguration_InvalidUserRole_ReturnsNull(UserRole userRole, ConfigurationKey configurationType)
    {
        using TMSContext tms = new();
        User user = tms.Users.First(user => user.Role == userRole);

        var result = user.GetConfiguration(configurationType);

        Assert.Null(result);
    }

    [Theory]
    [InlineData(UserRole.Admin)]
    public void ReviewLog_ValidUserRole_ReturnsContents(UserRole userRole)
    {
        using TMSContext tms = new();
        User user = tms.Users.First(user => user.Role == userRole);
        Log log = new()
        {
            Filename = DateTime.Now.Clean(),
            Contents = "Testing 123"
        };

        log.Write();
        var result = user.ReviewLog(log);

        Assert.NotNull(result);
    }

    [Theory]
    [InlineData(UserRole.Buyer)]
    [InlineData(UserRole.Planner)]
    public void ReviewLog_InvalidUserRole_ReturnsNull(UserRole userRole)
    {
        using TMSContext tms = new();
        User user = tms.Users.First(user => user.Role == userRole);
        Log log = new()
        {
            Filename = DateTime.Now.Clean(),
            Contents = "Testing 123"
        };

        log.Write();
        var result = user.ReviewLog(log);

        Assert.Null(result);
    }
}