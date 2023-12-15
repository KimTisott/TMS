namespace TMS.UI;

public partial class AdminControl
{
    public AdminControl()
    {
        InitializeComponent();

        ConfigurationDataGrid.ItemsSource = ConfigurationService.Configurations;
        LogDataGrid.ItemsSource = LoggingService.ReadAll();
        using TMSContext tms = new();
        CarrierDataGrid.ItemsSource = tms.Carriers.Include(carrier => carrier.Depots).ToArray();
        CarrierDepotsMultiSelectionComboBox.ItemsSource = tms.Cities.Select(city => city.Name).ToArray();
        RouteDataGrid.ItemsSource = tms.Routes.Include(route => route.From).Include(route => route.To).ToArray();
        RouteFromComboBox.ItemsSource = RouteToComboBox.ItemsSource = tms.Cities.ToArray();
        RateDataGrid.ItemsSource = tms.Rates.ToArray();
    }

    private void ConfigurationDataGrid_MouseUp(object sender, MouseButtonEventArgs e)
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
            var configuration = (Configuration)ConfigurationDataGrid.SelectedItem;
            ConfigurationValueTextBox.Text = configuration.Value;
        }
    }

    private void ConfigurationSaveButton_Click(object sender, RoutedEventArgs e)
    {
        var configuration = (Configuration)ConfigurationDataGrid.SelectedItem;
        configuration.Save();
    }

    private void LogRefreshButton_Click(object sender, RoutedEventArgs e)
    {
        LogDataGrid.ItemsSource = LoggingService.ReadAll();
    }

    private void CarrierDataGrid_MouseUp(object sender, MouseButtonEventArgs e)
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
            var carrier = (Carrier?)CarrierDataGrid.SelectedItem;
            if (carrier != null)
            {
                CarrierNameTextBox.Text = carrier.Name;
                CarrierDepotsMultiSelectionComboBox.SelectedItems?.Clear();
                foreach (var depot in carrier.Depots)
                {
                    CarrierDepotsMultiSelectionComboBox.SelectedItems?.Add(depot.Name);
                }
                CarrierFTLAvailabilityNumericUpDown.Value = carrier.FTLAvailability;
                CarrierLTLAvailabilityNumericUpDown.Value = carrier.LTLAvailability;
                CarrierFTLRateNumericUpDown.Value = (double)carrier.FTLRate;
                CarrierLTLRateNumericUpDown.Value = (double)carrier.LTLRate;
                CarrierReeferChargeNumericUpDown.Value = carrier.ReeferCharge;
                CarrierSaveButton.IsEnabled = CarrierDeleteButton.IsEnabled = true;
            }
            else
            {
                CarrierSaveButton.IsEnabled = CarrierDeleteButton.IsEnabled = false;
            }
        }
    }

    private void CarrierCreateButton_Click(object sender, RoutedEventArgs e)
    {
        using TMSContext tms = new();
        Carrier carrier = new()
        {
            Name = CarrierNameTextBox.Text,
            FTLAvailability = (int)CarrierFTLAvailabilityNumericUpDown.Value.GetValueOrDefault(),
            LTLAvailability = (int)CarrierLTLAvailabilityNumericUpDown.Value.GetValueOrDefault(),
            FTLRate = (int)CarrierFTLRateNumericUpDown.Value.GetValueOrDefault(),
            LTLRate = (int)CarrierLTLRateNumericUpDown.Value.GetValueOrDefault(),
            ReeferCharge = (int)CarrierReeferChargeNumericUpDown.Value.GetValueOrDefault()
        };
        var cityNames = CarrierDepotsMultiSelectionComboBox.SelectedItems;
        if (cityNames != null)
        {
            var cities = tms.Cities.Where(city => cityNames.Contains(city.Name));
            carrier.Depots = new List<City>();
            foreach (var city in cities)
            {
                if (carrier.Depots.Contains(city))
                {
                    tms.Entry(city).State = EntityState.Modified;
                }
                else
                {
                    carrier.Depots.Add(city);
                }
            }
        }
        tms.Carriers.Add(carrier);
        tms.SaveChanges();

        CarrierDataGrid.ItemsSource = tms.Carriers.Include(carrier => carrier.Depots).ToArray();
        CarrierSaveButton.IsEnabled = CarrierDeleteButton.IsEnabled = false;
    }

    private void CarrierSaveButton_Click(object sender, RoutedEventArgs e)
    {
        using TMSContext tms = new();
        var carrier = tms.Carriers.Include(carrier => carrier.Depots).First(carrier => carrier.Id == ((Carrier)CarrierDataGrid.SelectedItem).Id);
        carrier.Name = CarrierNameTextBox.Text;
        carrier.FTLAvailability = (int)CarrierFTLAvailabilityNumericUpDown.Value.GetValueOrDefault();
        carrier.LTLAvailability = (int)CarrierLTLAvailabilityNumericUpDown.Value.GetValueOrDefault();
        carrier.FTLRate = (int)CarrierFTLRateNumericUpDown.Value.GetValueOrDefault();
        carrier.LTLRate = (int)CarrierLTLRateNumericUpDown.Value.GetValueOrDefault();
        carrier.ReeferCharge = (int)CarrierReeferChargeNumericUpDown.Value.GetValueOrDefault();
        var cityNames = CarrierDepotsMultiSelectionComboBox.SelectedItems;
        if (cityNames != null)
        {
            var cities = tms.Cities.Where(city => cityNames.Contains(city.Name));
            carrier.Depots = new List<City>();
            foreach (var city in cities)
            {
                if (carrier.Depots.Contains(city))
                {
                    tms.Entry(city).State = EntityState.Modified;
                }
                else
                {
                    carrier.Depots.Add(city);
                }
            }
        }
        tms.SaveChanges();

        CarrierDataGrid.ItemsSource = tms.Carriers.Include(carrier => carrier.Depots).ToArray();
        CarrierSaveButton.IsEnabled = CarrierDeleteButton.IsEnabled = false;
    }

    private void CarrierDeleteButton_Click(object sender, RoutedEventArgs e)
    {
        using TMSContext tms = new();
        tms.Carriers.Remove((Carrier)CarrierDataGrid.SelectedItem);
        tms.SaveChanges();

        CarrierDataGrid.ItemsSource = tms.Carriers.Include(carrier => carrier.Depots).ToArray();
        CarrierSaveButton.IsEnabled = CarrierDeleteButton.IsEnabled = false;
    }

    private void RouteDataGrid_MouseUp(object sender, MouseButtonEventArgs e)
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
            var route = (Route?)RouteDataGrid.SelectedItem;
            if (route != null)
            {
                RouteFromComboBox.SelectedItem = route.From;
                RouteToComboBox.SelectedItem = route.To;
                RouteDistanceNumericUpDown.Value = route.Distance;
                RouteTimeNumericUpDown.Value = route.Time;
                RouteSaveButton.IsEnabled = true;
            }
            else
            {
                RouteSaveButton.IsEnabled = false;
            }
        }
    }

    private void RouteSaveButton_Click(object sender, RoutedEventArgs e)
    {
        using TMSContext tms = new();
        var route = tms.Routes.First(route => route.Id == ((Route)RouteDataGrid.SelectedItem).Id);
        route.FromId = ((City)RouteFromComboBox.SelectedItem).Id;
        route.ToId = ((City)RouteToComboBox.SelectedItem).Id;
        route.Distance = (int)RouteDistanceNumericUpDown.Value.GetValueOrDefault();
        route.Time = RouteTimeNumericUpDown.Value.GetValueOrDefault();
        tms.SaveChanges();

        RouteDataGrid.ItemsSource = tms.Routes.Include(route => route.From).Include(route => route.To).ToArray();
        RouteFromComboBox.ItemsSource = RouteToComboBox.ItemsSource = tms.Cities.ToArray();
        RouteSaveButton.IsEnabled = false;
    }

    private void RateDataGrid_MouseUp(object sender, MouseButtonEventArgs e)
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
            var rate = (Rate)RateDataGrid.SelectedItem;
            RateCostNumericUpDown.Value = (double)rate.Cost;
        }
    }

    private void RateSaveButton_Click(object sender, RoutedEventArgs e)
    {
        using TMSContext tms = new();
        var rate = tms.Rates.First(rate => rate.Type == ((Rate)RateDataGrid.SelectedItem).Type);
        rate.Cost = (decimal)RateCostNumericUpDown.Value.GetValueOrDefault();
        tms.SaveChanges();

        RateDataGrid.ItemsSource = tms.Rates.ToArray();
    }
}