using Network_measurement_frontend.Shared.Model;
using Plugin.MauiWifiManager;

namespace Network_measurement_frontend.Pages;

public partial class WSSPage : ContentPage
{
    public Command InfoCommand { get; }
    public List<object> WSSItems { get; set; } = new List<object>();
    public WSSPage()
    {
        InitializeComponent();
        BindingContext = this;
        InfoCommand = new Command<NetworkData>(ExecuteInfoCommand);
        var x = new DashboardPage();
        x.ActiveReportChanged += (sender, e) =>
        {
            WSSItems.Clear();
        };
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await GetWifiList();
    }

    private async void ExecuteInfoCommand(NetworkData model)
    {
        var info = $"StatusId: {model.StausId}, " +
               $"Ssid: {model.SsidName}, " +
               $"IpAddress: {model.IpAddress}, " +
               $"GatewayAddress: {model.GatewayAddress ?? "N/A"}, " +
               $"Signal Strength: {model.SignalStrengthDecibel}, " + // EZT TETTEM HOZZÁ
               $"NativeObject: {model.NativeObject}, " +
               $"Bssid: {model.Bssid}";
        await DisplayAlert("Network info", info, "OK");
    }
    private async Task GetWifiList()
    {
        PermissionStatus status = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();
        if (status == PermissionStatus.Granted || DeviceInfo.Current.Platform == DevicePlatform.WinUI)
        {
            await Task.Delay(1000);
            loading.IsRunning = true;
            scanCollectionView.IsVisible = false;
            var response = await CrossWifiManager.Current.ScanWifiNetworks();
            foreach (var item in response)
            {
                WSSItems.Add(new NetworkData()
                {
                    StausId = item.StausId,
                    IpAddress = (int)item.IpAddress,
                    Bssid = item.Bssid,
                    Ssid = item.Ssid,
                    GatewayAddress = item.GatewayAddress,
                    NativeObject = item.NativeObject,
                    SignalStrengthDecibel = item.SignalStrengthDecibel  // EZT TETTEM HOZZÁ

                });
            }
            scanCollectionView.ItemsSource = WSSItems;
            loading.IsRunning = false;
            scanCollectionView.IsVisible = true;
        }
        else
            await DisplayAlert("No location permisson", "Please provide location permission", "OK");
    }

    private async void BtnAdd_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new AddToReportPage(WSSItems));
    }

    private async void BtnMeasure_Clicked(object sender, EventArgs e)
    {
        await GetWifiList();
    }
}