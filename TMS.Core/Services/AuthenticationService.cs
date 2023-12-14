namespace TMS.Core;

public static class AuthenticationService
{
    public static User User { get; private set; }

    public static bool Login(string username, string password)
    {
        using TMSContext tms = new();
        User = tms.Users.FirstOrDefault(
            user => user.Name == username 
            && user.Password == password);

        return User != null;
    }

    public static void Logout()
    {
        User = null;
    }
}