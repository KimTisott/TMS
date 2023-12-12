namespace TMS.Tests;

public class LogTest
{
    [Theory]
    [InlineData("2023111255510PM", "Testing 123")]
    public void Write_ValidFilenameAndContents_ReturnsTrue(string filename, string contents)
    {
        Log log = new()
        {
            Filename = filename,
            Contents = contents
        };

        var result = log.Write();

        Assert.True(result);
    }

    [Theory]
    [InlineData("./-Broken-^+", "The filename should be cleaned.")]
    public void Write_DirtyFilename_ReturnsTrue(string filename, string contents)
    {
        Log log = new()
        {
            Filename = filename,
            Contents = contents
        };

        var result = log.Write();

        Assert.True(result);
    }

    [Theory]
    [InlineData(null, "Cannot write this.")]
    public void Write_InvalidFilename_ReturnsFalse(string filename, string contents)
    {
        Log log = new()
        {
            Filename = filename,
            Contents = contents
        };

        var result = log.Write();

        Assert.False(result);
    }
}