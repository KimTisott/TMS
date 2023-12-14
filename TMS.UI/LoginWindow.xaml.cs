namespace TMS.UI;

public partial class LoginWindow
{
    // Temporary credentials
    private const string TempUsername = "admin";
    private const string TempPassword = "password123";

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
        string enteredUsername = UsernameTextBox.Text;
        string enteredPassword = PasswordPasswordBox.Password;

        // Validate credentials
        if (enteredUsername == TempUsername && enteredPassword == TempPassword)
        {
            // Credentials are correct, open MainWindow
            MainWindow main = new MainWindow();
            main.Show();
            this.Close();
        }
        else
        {
            Error.Text = "Invalid Username and/or Password.";
        }
    }
}