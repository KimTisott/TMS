namespace TMS.UI;

public partial class PlannerControl
{
    public PlannerControl()
    {
        InitializeComponent();

        using TMSContext tms = new();
        OrderDataGrid.ItemsSource = tms.Orders
            .Include(order => order.Trips)
                .ThenInclude(trip => trip.Routes)
            .Include(order => order.Carriers)
                .ThenInclude(carrier => carrier.Depots)
            .Include(order => order.Cities)
            .Include(order => order.Customer)
            .ToArray();
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