namespace TMS.UI;

public partial class LoginWindow
{
    public LoginWindow()
    {
        InitializeComponent();

        KeyUp += LoginWindow_KeyUp;

        UsernameTextBox.MaxLength = User.NameLength;
        PasswordPasswordBox.MaxLength = User.PasswordLength;

        UsernameTextBox.Focus();
#if DEBUG
        AuthenticationService.Login("admin", "admin");
        MainWindow main = new();
        main.Show();
        Close();
#endif
    }

    private void LoginWindow_KeyUp(object sender, KeyEventArgs e)
    {
        if (e.Key == Key.Enter)
        {
            LoginButton_Click(this, new());
        }
    }

    private void LoginButton_Click(object sender, RoutedEventArgs e)
    {
        if (AuthenticationService.Login(UsernameTextBox.Text, PasswordPasswordBox.Password))
        {
            MainWindow main = new();
            main.Show();
            Close();
        }
        else
        {
            Error.Text = "Invalid Username and/or Password.";
        }
    }
}