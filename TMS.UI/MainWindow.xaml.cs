namespace TMS.UI;

public partial class MainWindow
{
    public MainWindow()
    {
        InitializeComponent();

        Title += AuthenticationService.User.Role;
        switch (AuthenticationService.User.Role)
        {
            case UserRole.Admin:
            {
                SimulateTimePassageButton.Visibility = Visibility.Collapsed;
                BuyerControl.Visibility = Visibility.Collapsed;
                PlannerControl.Visibility = Visibility.Collapsed;
                break;
            }
            case UserRole.Buyer:
            {
                BackupButton.Visibility = Visibility.Collapsed;
                SimulateTimePassageButton.Visibility = Visibility.Collapsed;
                AdminControl.Visibility = Visibility.Collapsed;
                PlannerControl.Visibility = Visibility.Collapsed;
                break;
            }
            case UserRole.Planner:
            {
                BackupButton.Visibility = Visibility.Collapsed;
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

    private void BackupButton_Click(object sender, RoutedEventArgs e)
    {
        if (BackupService.Run())
        {
            WindowTextBlock.Text = $"Last backup at: {DateTime.Now}";
        }
    }

    private void SimulateTimePassageButton_Click(object sender, RoutedEventArgs e)
    {

    }
}