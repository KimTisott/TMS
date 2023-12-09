namespace TMS.Tests;

public class LogTest
{
    [Fact]
    public void Write()
    {
        using DataContext context = new();
        var log = context.Logs.Add(new Log { 
            Content = "test",
            Filename = "newLog.txt" });
        Assert.True(log.Entity.Write());
    }
}