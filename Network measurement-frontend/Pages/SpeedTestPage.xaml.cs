using System.Diagnostics;
using System.Net.Http;
using System.Net.NetworkInformation;

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
        var down = await DownloadSpeedTest();
        var up = await UploadSpeedTest();
        var ping = await PingTime("testmy.net");

        Down.Text = down.ToString(); 
        Up.Text = up.ToString();
        Ping.Text = ping.ToString();

    }
    private async Task<double> DownloadSpeedTest()
    {
        var httpClient = new HttpClient();
        var stopwatch = new Stopwatch();

        stopwatch.Start();
        HttpResponseMessage response = await httpClient.GetAsync(downloadUrl1);
        stopwatch.Stop();
        var downloadTime = stopwatch.ElapsedMilliseconds;

        return MBsCalc(downloadTime, 100);
    } 
    private async Task<double> UploadSpeedTest()
    {
        var httpClient = new HttpClient();
        var stopwatch = new Stopwatch();
        int MB = 100;

        byte[] randomData = GenerateRandomData(MB * 1024 * 1024); // 100MB

        // Create ByteArrayContent from the random data
        ByteArrayContent dataContent = new ByteArrayContent(randomData);

        stopwatch.Start();
        // Perform the HTTP POST request
        HttpResponseMessage response = await httpClient.PostAsync(uploadUrl1, dataContent);
        stopwatch.Stop();
        var uploadTime = stopwatch.ElapsedMilliseconds;

        return MBsCalc(uploadTime, MB);
    }
    private async Task<long> PingTime(string targetHost)
    {
        try
        {
            Ping ping = new Ping();
            PingReply reply = await ping.SendPingAsync(targetHost);

            if (reply.Status == IPStatus.Success)
            {
                return reply.RoundtripTime;
            }
            else
            {
                return -1;
            }
        }
        catch (PingException ex)
        {
            return -1;
        }
    }

    private double MBsCalc(long ms, int MB)
    {
        return MB / ms / 1000;  
    }
	private static byte[] GenerateRandomData(int size)
    {
        byte[] data = new byte[size];
        new Random().NextBytes(data);
        return data;
    }
}