namespace TMS.Core;

public class User : Entity
{
    public string Name { get; set; }
    public string Password { get; set; }
    public UserRole Role { get; set; }
}