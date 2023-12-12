namespace TMS.UI;

public partial class LoginWindow
{
    public LoginWindow()
    {
        InitializeComponent();

        UsernameTextBox.MaxLength = User.NameLength;
        PasswordPasswordBox.MaxLength = User.PasswordLength;
    }

    private async void LoginButton_Click(object sender, RoutedEventArgs e)
    {
        if (AuthenticationService.Login(UsernameTextBox.Text, PasswordPasswordBox.Password))
        {
            MainWindow main = new();
            main.Show();
            Close();
        }
        else
        {
            await this.ShowMessageAsync("TMS", "Invalid Username and/or Password!");
        }
    }
}