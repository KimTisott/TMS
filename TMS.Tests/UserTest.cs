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
        using DataContext context = new();
        User user = context.Users.First(user => user.Role == userRole);

        var result = user.Backup();

        Assert.True(result);
    }

    [Theory]
    [InlineData(UserRole.Buyer)]
    [InlineData(UserRole.Planner)]
    public void Backup_InvalidUserRole_ReturnsFalse(UserRole userRole)
    {
        using DataContext context = new();
        User user = context.Users.First(user => user.Role == userRole);

        var result = user.Backup();

        Assert.False(result);
    }

    [Theory]
    [InlineData(UserRole.Admin, ConfigurationType.Database, "TMS")]
    [InlineData(UserRole.Admin, ConfigurationType.Password, "root")]
    [InlineData(UserRole.Admin, ConfigurationType.Port, "3306")]
    [InlineData(UserRole.Admin, ConfigurationType.Server, "127.0.0.1")]
    [InlineData(UserRole.Admin, ConfigurationType.Uid, "root")]
    public void SetConfiguration_ValidUserRole_ReturnsTrue(UserRole userRole, ConfigurationType configurationType, string value)
    {
        using DataContext context = new();
        User user = context.Users.First(user => user.Role == userRole);

        var result = user.SetConfiguration(configurationType, value);

        Assert.True(result);
    }

    [Theory]
    [InlineData(UserRole.Buyer, ConfigurationType.Database, "TMS")]
    [InlineData(UserRole.Planner, ConfigurationType.Database, "TMS")]
    public void SetConfiguration_InvalidUserRole_ReturnsFalse(UserRole userRole, ConfigurationType configurationType, string value)
    {
        using DataContext context = new();
        User user = context.Users.First(user => user.Role == userRole);

        var result = user.SetConfiguration(configurationType, value);

        Assert.False(result);
    }

    [Theory]
    [InlineData(UserRole.Admin, ConfigurationType.Database)]
    [InlineData(UserRole.Admin, ConfigurationType.Password)]
    [InlineData(UserRole.Admin, ConfigurationType.Port)]
    [InlineData(UserRole.Admin, ConfigurationType.Server)]
    [InlineData(UserRole.Admin, ConfigurationType.Uid)]
    public void GetConfiguration_ValidUserRole_ReturnsConfiguration(UserRole userRole, ConfigurationType configurationType)
    {
        using DataContext context = new();
        User user = context.Users.First(user => user.Role == userRole);

        var result = user.GetConfiguration(configurationType);

        Assert.NotNull(result);
    }

    [Theory]
    [InlineData(UserRole.Buyer, ConfigurationType.Database)]
    [InlineData(UserRole.Planner, ConfigurationType.Database)]
    public void GetConfiguration_InvalidUserRole_ReturnsNull(UserRole userRole, ConfigurationType configurationType)
    {
        using DataContext context = new();
        User user = context.Users.First(user => user.Role == userRole);

        var result = user.GetConfiguration(configurationType);

        Assert.Null(result);
    }

    [Theory]
    [InlineData(UserRole.Admin)]
    public void ReviewLog_ValidUserRole_ReturnsContents(UserRole userRole)
    {
        using DataContext context = new();
        User user = context.Users.First(user => user.Role == userRole);
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
        using DataContext context = new();
        User user = context.Users.First(user => user.Role == userRole);
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