namespace TMS.UI;

public partial class AdminControl
{
    public AdminControl()
    {
        InitializeComponent();

        ConfigurationDataGrid.ItemsSource = ConfigurationService.Configurations;
    }

    private void SaveButton_Click(object sender, RoutedEventArgs e)
    {
        var configuration = (Configuration)ConfigurationDataGrid.SelectedItem;
        configuration.Save();
    }
}