using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Zeroconf;
using Network_measurement_frontend.Shared;
using Network_measurement_frontend.Shared.Model;

namespace Network_measurement_frontend.Pages;

public partial class DevicesPage : ContentPage
{
    public List<object> DevicesItems { get; set; } = new List<object>();
    public DevicesPage()
    {
        InitializeComponent();
        var x = new DashboardPage();
        x.ActiveReportChanged += (sender, e) =>
        {
            DevicesItems.Clear();
        };
    }

    private async void BtnDevices_Clicked(object sender, EventArgs e)
    {
        try
        {
            var devices = await DiscoverDevices();
            deviceListView.ItemsSource = devices;
            DevicesItems.Add(devices);
        }
        catch (Exception ex)
        {
            // Handle any exceptions that may occur during device discovery
            await DisplayAlert("Error", ex.Message, "OK");
        }
    }

    private async void OnDiscoverClicked(object sender, EventArgs e)
    {
        try
        {
            var devices = await DiscoverDevices();
            deviceListView.ItemsSource = devices;
        }
        catch (Exception ex)
        {
            // Handle any exceptions that may occur during device discovery
            await DisplayAlert("Error", ex.Message, "OK");
        }
    }

    private async Task<List<Shared.Device>> DiscoverDevices()
    {
        ILookup<string, string> domains = await ZeroconfResolver.BrowseDomainsAsync();
        var results = await ZeroconfResolver.ResolveAsync(domains.Select(g => g.Key));

        var devices = new List<Shared.Device>();

        foreach (var result in results)
        {
            Shared.Device device = new Shared.Device();
            device.IPAddress = result.IPAddress;
            device.Name = result.DisplayName;
            
            devices.Add(device);
        }

        return devices;
    }
    private async void BtnAdd_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new AddToReportPage(DevicesItems, (int)MesurementType.DSC));
    }
}