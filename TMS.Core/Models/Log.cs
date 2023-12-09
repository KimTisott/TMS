namespace TMS.Core;

public class Log
{
    public string Filename { get; set; }
    public string Content { get; set; }

    public bool Write()
    {
        try
        {
            File.WriteAllText(Filename, Content);
            return true;
        }
        catch
        {
            return false;
        }
    }
}