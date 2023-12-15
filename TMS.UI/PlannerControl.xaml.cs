namespace TMS.UI;

public partial class PlannerControl
{
    public PlannerControl()
    {
        InitializeComponent();

        using TMSContext tms = new();
        OrderDataGrid.ItemsSource = tms.Orders.ToArray();
    }

    private void OrderRefreshButton_Click(object sender, RoutedEventArgs e)
    {
        using TMSContext tms = new();
        OrderDataGrid.ItemsSource = tms.Orders.ToArray();
    }

    private void OrderDataGrid_MouseUp(object sender, MouseButtonEventArgs e)
    {

    }

    private void OrderSaveButton_Click(object sender, RoutedEventArgs e)
    {

    }

    private void OrderGenerateInvoiceAllTimeButton_Click(object sender, RoutedEventArgs e)
    {

    }

    private void OrderGenerateInvoicePast2WeeksButton_Click(object sender, RoutedEventArgs e)
    {

    }
}