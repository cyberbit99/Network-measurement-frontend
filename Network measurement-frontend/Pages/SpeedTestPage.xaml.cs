using System.Diagnostics;
using System.Net.Http;
using System.Net.NetworkInformation;
using Microsoft.Maui.Controls;
using Network_measurement_frontend.Measurements;
using Network_measurement_frontend.Shared.Model;

namespace Network_measurement_frontend.Pages;

public partial class SpeedTestPage : ContentPage
{
    private const string downloadUrl = "https://testmy.net/dl-100MB";
    private const string uploadUrl = "https://testmy.net/U-100MB";
    private const string downloadUrl1 = "http://192.168.1.85:7037/api/speedtest/download";
    private const string uploadUrl1 = "http://192.168.1.85:7037/api/speedtest/upload";
    private const string pingUrl1 = "http://192.168.1.85:7037/api/seedtest/ping";
    private const string pingUrl = "192.168.1.85:7037/api/seedtest/ping";

    public List<object> SpeedTestItems { get; set; } = new List<object>();

    public SpeedTestPage()
    {
        InitializeComponent();
        var x = new DashboardPage();
        x.ActiveReportChanged += (sender, e) =>
        {
            SpeedTestItems.Clear();
        };

    }

    public async void RunSpeedTest(object sender, EventArgs e)
    {
        SpeedTest st = new SpeedTest();

        var down = await st.DownloadSpeedTest(downloadUrl1);
        var up = await st.UploadSpeedTest(uploadUrl1);
        var ping = await st.PingTime(pingUrl1);

        Down.Text = down.ToString() + " MB/s";
        Up.Text = up.ToString() + " MB/s";
        Ping.Text = ping.ToString() + " ms";
        SpeedTestItems.Add(new SpeedTestItem() { Down = down, Up = up, Ping = ping });
    }

    private async void BtnAdd_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new AddToReportPage(SpeedTestItems));
    }
}