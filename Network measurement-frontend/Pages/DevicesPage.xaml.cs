using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Zeroconf;
using Network_measurement_frontend.Shared;

namespace Network_measurement_frontend.Pages;

public partial class DevicesPage : ContentPage
{
    public DevicesPage()
    {
        InitializeComponent();
    }

    private async void BtnDevices_Clicked(object sender, EventArgs e)
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
}