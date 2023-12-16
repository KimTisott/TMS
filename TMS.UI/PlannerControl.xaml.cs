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
            .Include(order => order.Routes)
            .Include(order => order.Cities)
            .Include(order => order.Customer)
            .ToArray();
    }

    private void OrderRefreshButton_Click(object sender, RoutedEventArgs e)
    {
        using TMSContext tms = new();
        OrderDataGrid.ItemsSource = tms.Orders
            .Include(order => order.Trips)
                .ThenInclude(trip => trip.Routes)
            .Include(order => order.Carriers)
                .ThenInclude(carrier => carrier.Depots)
            .Include(order => order.Routes)
            .Include(order => order.Cities)
            .Include(order => order.Customer)
            .ToArray();
    }

    private void OrderDataGrid_MouseUp(object sender, MouseButtonEventArgs e)
    {
        var source = (DependencyObject)e.OriginalSource;

        while (source != null && source is not DataGridCell && source is not DataGridColumnHeader)
        {
            source = VisualTreeHelper.GetParent(source);
        }

        if (source == null)
            return;

        if (source is DataGridCell)
        {
            var order = (Order)OrderDataGrid.SelectedItem;
            OrderIsCompletedCheckBox.IsChecked = order.IsCompleted;
            using TMSContext tms = new();
            OrderCarrierDataGrid.ItemsSource = order.Cities.Select(x => new CityCarrier()
            {
                City = x,
                Carriers = [.. tms.CarrierCities.Include(y => y.Carrier).Where(y => y.CityId == x.Id).Select(y => y.Carrier)],
                SelectedCarrier = order.Carriers.FirstOrDefault(y => tms.CarrierCities.Any(z => z.CarrierId == y.Id && z.CityId == x.Id))
            }).ToList();

            OrderSaveButton.IsEnabled = true;
        }
    }

    private void OrderSaveButton_Click(object sender, RoutedEventArgs e)
    {
        using TMSContext tms = new();
        var order = tms.Orders.First(order => order.Id == ((Order)OrderDataGrid.SelectedItem).Id);
        order.IsCompleted = OrderIsCompletedCheckBox.IsChecked == true;
        order.Carriers.Clear();
        foreach (var carrier in ((List<CityCarrier>)OrderCarrierDataGrid.ItemsSource).Where(x => x.SelectedCarrier != null).Select(x => x.SelectedCarrier))
        {
            order.Carriers.Add(carrier);
        }
        tms.SaveChanges();

        OrderDataGrid.ItemsSource = tms.Orders
            .Include(order => order.Trips)
                .ThenInclude(trip => trip.Routes)
            .Include(order => order.Carriers)
                .ThenInclude(carrier => carrier.Depots)
            .Include(order => order.Routes)
            .Include(order => order.Cities)
            .Include(order => order.Customer)
            .ToArray();

        OrderSaveButton.IsEnabled = false;
    }

    private void OrderGenerateInvoiceAllTimeButton_Click(object sender, RoutedEventArgs e)
    {
        using TMSContext tms = new();
        var invoices = tms.Invoices.ToList();
        File.WriteAllText(Path.Combine("Invoices", $"InvoiceAllTime_{DateTime.Now.Clean()}.txt"), string.Join(Environment.NewLine, invoices.Select(x => x.Contents)));
    }

    private void OrderGenerateInvoicePast2WeeksButton_Click(object sender, RoutedEventArgs e)
    {
        using TMSContext tms = new();
        var invoices = tms.Invoices.Where(x => x.CreatedAt > DateTime.Now.AddDays(-14)).ToList();
        File.WriteAllText(Path.Combine("Invoices", $"InvoiceLast2Weeks_{DateTime.Now.Clean()}.txt"), string.Join(Environment.NewLine, invoices.Select(x => x.Contents)));
    }

    private class CityCarrier
    {
        public City? City { get; set; }
        public List<Carrier> Carriers { get; set; } = new();
        public Carrier? SelectedCarrier { get; set; }
    }
}