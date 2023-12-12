namespace TMS.Core;

public class Log : Entity
{
    public string Filename { get; set; }

    [NotMapped]
    public string Contents { get; set; }

    public bool Write() =>
        LoggingService.Write(Filename, Contents);
}