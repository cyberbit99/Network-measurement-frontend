using Network_measurement_frontend.Shared;
using Network_measurement_frontend.Shared.Model;
using Newtonsoft.Json;

namespace Network_measurement_frontend.Pages;

public partial class ReportEditorPage : ContentPage
{
    public ReportEditorPage(object selectedItem)
    {
        InitializeComponent();
        BindingContext = selectedItem;
    }

    private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
    {
        Navigation.PopAsync();
    }

    private async Task BtnReportEdit_ClickedAsync()
    {
        MeasurementReport report = BindingContext as MeasurementReport;
        if (EntDescription != null && EntName != null)
        {
            report.Description = EntDescription.Text;
            report.Name = EntName.Text;

            string json = JsonConvert.SerializeObject(report);
            var httpContent = new StringContent(json, null, "application/json");
            string target = "http://192.168.1.85:7037/api/cou/report";
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, target);
            request.Content = httpContent;
            var response = await client.SendAsync(request);

        }
    }

    private void BtnReportEdit_Clicked(object sender, EventArgs e)
    {
        BtnReportEdit_ClickedAsync();
    }
}