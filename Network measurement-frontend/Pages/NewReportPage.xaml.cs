using Network_measurement_frontend.Shared.Model;
using Newtonsoft.Json;
using Network_measurement_frontend.Shared;

namespace Network_measurement_frontend.Pages;

public partial class NewReportPage : ContentPage
{
	public NewReportPage()
	{
		InitializeComponent();
	}

    private async void BtnCreateReport_Clicked(object sender, EventArgs e)
    {
        await CreateReportAsync();
    }
    private async Task CreateReportAsync()
    {
        MeasurementReport report = new MeasurementReport();
        if (EntDescription != null && EntName != null)
        {
            Session session = Session.Instance(null as User);
            report.Description = EntDescription.Text;
            report.Name = EntName.Text;
            report.StartDate = DateTime.Now;
            report.UserId = (int)session.GetUser().UserId;

            string json = JsonConvert.SerializeObject(report);
            var httpContent = new StringContent(json, null, "application/json");
            string target = "http://192.168.1.85:7037/api/cou/report";
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, target);
            request.Content = httpContent;
            var response = await client.SendAsync(request);

        }

    }

}