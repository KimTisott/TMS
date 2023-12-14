namespace TMS.UI;

public partial class MainWindow
{
    public MainWindow()
    {
        InitializeComponent();

        switch (AuthenticationService.User.Role)
        {
            case UserRole.Admin:
            {
                BuyerControl.Visibility = Visibility.Collapsed;
                PlannerControl.Visibility = Visibility.Collapsed;
                break;
            }
            case UserRole.Buyer:
            {
                AdminControl.Visibility = Visibility.Collapsed;
                PlannerControl.Visibility = Visibility.Collapsed;
                break;
            }
            case UserRole.Planner:
            {
                AdminControl.Visibility = Visibility.Collapsed;
                BuyerControl.Visibility = Visibility.Collapsed;
                break;
            }
        }
    }

    private void LogoutButton_Click(object sender, RoutedEventArgs e)
    {
        AuthenticationService.User.Logout();

        LoginWindow login = new();
        login.Show();
        Close();
    }
}