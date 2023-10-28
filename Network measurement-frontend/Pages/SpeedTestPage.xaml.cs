using System.Diagnostics;
using System.Net.Http;
using System.Net.NetworkInformation;
using Network_measurement_frontend.Measurements;

namespace Network_measurement_frontend.Pages;

public partial class SpeedTestPage : ContentPage
{
	private const string downloadUrl1 = "https://testmy.net/dl-100MB";
	private const string uploadUrl1 = "https://testmy.net/U-100MB";

    public SpeedTestPage()
	{
		InitializeComponent();
	}
	public async void  RunSpeedTest(object sender, EventArgs e)
	{
        SpeedTest st = new SpeedTest();

        var down = await st.DownloadSpeedTest(downloadUrl1);
        var up = await st.UploadSpeedTest(uploadUrl1);
        var ping = await st.PingTime("testmy.net");

        Down.Text = down.ToString(); 
        Up.Text = up.ToString();
        Ping.Text = ping.ToString();

    }
    
}