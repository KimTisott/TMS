namespace TMS.UI;

public partial class BuyerControl
{
    public BuyerControl()
    {
        InitializeComponent();

        ContractDataGrid.ItemsSource = MarketplaceContext.GetContracts();
        using TMSContext tms = new();
        CustomerDataGrid.ItemsSource = tms.Customers.ToArray();
        OrderDataGrid.ItemsSource = tms.Orders.Include(order => order.Customer).Include(order => order.Cities).ToArray();
        OrderCitiesMultiSelectionComboBox.ItemsSource = tms.Cities.Select(city => city.Name).ToArray();
    }

    private void ContractRefreshButton_Click(object sender, RoutedEventArgs e)
    {
        ContractDataGrid.ItemsSource = MarketplaceContext.GetContracts();
    }

    private void ContractDataGrid_MouseUp(object sender, MouseButtonEventArgs e)
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
            var contract = (Contract)ContractDataGrid.SelectedItem;

            using TMSContext tms = new();
            var customerExists = tms.Customers.Any(customer => customer.Name == contract.ClientName);
            ContractCreateCustomerButton.IsEnabled = !customerExists;
            ContractCreateOrderButton.IsEnabled = customerExists;
        }
    }

    private void ContractCreateCustomerButton_Click(object sender, RoutedEventArgs e)
    {
        if (ContractDataGrid.SelectedItem != null)
        {
            using TMSContext tms = new();
            tms.Customers.Add(new()
            {
                Name = ((Contract)ContractDataGrid.SelectedItem).ClientName
            });
            tms.SaveChanges();
            CustomerDataGrid.ItemsSource = tms.Customers.ToArray();
            ContractCreateCustomerButton.IsEnabled = false;
            ContractCreateOrderButton.IsEnabled = true;
        }
    }

    private void ContractCreateOrderButton_Click(object sender, RoutedEventArgs e)
    {
        var contract = (Contract)ContractDataGrid.SelectedItem;
        using TMSContext tms = new();
        var customer = tms.Customers.First(customer => customer.Name == contract.ClientName);
        tms.Orders.Add(new()
        {
            JobType = contract.JobType,
            VanType = contract.VanType,
            Quantity = contract.Quantity,
            CustomerId = customer.Id,
            Customer = customer
        });
        tms.SaveChanges();
        OrderDataGrid.ItemsSource = tms.Orders.Include(order => order.Customer).Include(order => order.Cities).ToArray();
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
            OrderCitiesMultiSelectionComboBox.SelectedItems?.Clear();
            foreach (var city in order.Cities)
            {
                OrderCitiesMultiSelectionComboBox.SelectedItems?.Add(city.Name);
            }
            OrderSaveButton.IsEnabled = true;
            OrderCreateInvoiceButton.IsEnabled = order.IsCompleted;
        }
    }

    private void OrderSaveButton_Click(object sender, RoutedEventArgs e)
    {
        using TMSContext tms = new();
        var order = tms.Orders.Include(order => order.Cities).First(order => order.Id == ((Order)OrderDataGrid.SelectedItem).Id);
        var cityNames = OrderCitiesMultiSelectionComboBox.SelectedItems;
        if (cityNames != null)
        {
            var cities = tms.Cities.Where(city => cityNames.Contains(city.Name));
            order.Cities.Clear();
            foreach (var city in cities)
            {
                if (order.Cities.Contains(city))
                {
                    tms.Entry(city).State = EntityState.Modified;
                }
                else
                {
                    order.Cities.Add(city);
                }
            }
        }
        tms.SaveChanges();

        OrderDataGrid.ItemsSource = tms.Orders.Include(order => order.Customer).Include(order => order.Cities).ToArray();
        OrderSaveButton.IsEnabled = false;
    }

    private void OrderCreateInvoiceButton_Click(object sender, RoutedEventArgs e)
    {
        var order = (Order)OrderDataGrid.SelectedItem;
        using TMSContext tms = new();
        tms.Invoices.Add(new()
        {
            CreatedAt = DateTime.Now,
            Contents = $"Customer: {order.Customer.Name}\nVan Type: {order.VanType}\nJob Type: {order.JobType}\nQuantity: {order.Quantity}",
            OrderId = order.Id
        });
        tms.SaveChanges();
        OrderSaveButton.IsEnabled = false;
        OrderCreateInvoiceButton.IsEnabled = false;
    }
}