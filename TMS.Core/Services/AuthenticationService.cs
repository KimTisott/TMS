namespace TMS.Core;

public static class AuthenticationService
{
    public static User User { get; private set; }

    public static bool Login(string username, string password)
    {
        using DataContext context = new();
        User = context.Users.FirstOrDefault(
            user => user.Name == username 
            && user.Password == password);

        return User != null;
    }
}