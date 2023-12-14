namespace TMS.UI;

public partial class LoginWindow
{
    // Temporary credentials
    private const string TempUsername = "admin";
    private const string TempPassword = "password123";

    public LoginWindow()
    {
        InitializeComponent();

        UsernameTextBox.MaxLength = User.NameLength;
        PasswordPasswordBox.MaxLength = User.PasswordLength;
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
            // Show error message for invalid credentials
            MessageBox.Show("Invalid Username and/or Password!", "TMS Login Error");
        }
    }
}