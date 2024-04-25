using Network_measurement_frontend.Shared;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using Microsoft.Maui;
using Network_measurement_frontend.Shared.Model;
using System.Diagnostics.Metrics;
using Network_measurement_frontend.Shared.Requests;

namespace Network_measurement_frontend.Pages;

public partial class ReportPage : ContentPage
{
    public ObservableCollection<MeasurementReport> Reports { get; set; } = new ObservableCollection<MeasurementReport>();

    HttpResponseMessage response = new HttpResponseMessage();

    public ReportPage()
    {
        InitializeComponent();
        BindingContext = this;
    }
    protected override async void OnAppearing()
    {
        base.OnAppearing();

        // Fetch the list of items from the backend API
        var fetchedItems = await FetchItems();

        // Clear the existing items and add the fetched items to the collection
        Reports.Clear();
        foreach (var item in fetchedItems ?? Enumerable.Empty<MeasurementReport> ())
        {
            Reports.Add(item);
        }
    }
    private async void btn_Edit_Selected(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ReportEditorPage(itemListView.SelectedItem));
    }

    private async void BtnCreateNewReport_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new NewReportPage());
        //Shell.Current.GoToAsync("//NewReportPage");
    }

    private async void BtnExport_Clicked(object sender, EventArgs e)
    {
        //add át a szükséges adatokat
        string target = "http://192.168.1.85:7037/api/getreportpdf";
        var client = new HttpClient();
        var request = new HttpRequestMessage(HttpMethod.Post, target);
        string filename = "";

        if (itemListView.SelectedItem != null)
        {
            MeasurementReport report = new MeasurementReport();
            report = itemListView.SelectedItem as MeasurementReport;
            filename = report.MeasurementReportId.ToString() + ".pdf";

            MeasurementReportRequest reportRequest = new MeasurementReportRequest();
            reportRequest.MeasurementReportId = report.MeasurementReportId;
            reportRequest.UserId = report.UserId;
            reportRequest.Name = report.Name;
            reportRequest.Description = report.Description;
            request.Content = CreateContent(reportRequest);

        }

        response = await client.SendAsync(request);
        byte[] file = await response.Content.ReadAsByteArrayAsync();
        OpenPdfAsync(await SaveTemporaryFileAsync(file, filename));

    }


    private async Task<List<MeasurementReport>> FetchItems()
    {
        // Use HttpClient or your preferred HTTP client to make a request to the API
        // Deserialize the response to a list of items
        // Return the list of items
        // Example:
        Session session = Session.Instance(null as User);
        User user = session.GetUser();
        var httpClient = new HttpClient();
        var response = await httpClient.GetStringAsync($"http://192.168.1.85:7037/api/getreport/{user.UserId}");
        return JsonConvert.DeserializeObject<List<MeasurementReport>>(response);
    }

    private StringContent CreateContent(MeasurementReportRequest reportRequest)
    {
        string json = JsonConvert.SerializeObject(reportRequest);
        var httpContent = new StringContent(json, null, "application/json");

        return httpContent;
    }

    public async Task<string> SaveTemporaryFileAsync(byte[] pdfBytes, string fileName)
    {
        string tempFolderPath = Path.GetTempPath(); // Get temporary folder path
        string filePath = Path.Combine(tempFolderPath, fileName);

        await File.WriteAllBytesAsync(filePath, pdfBytes); // Write the PDF bytes to the file

        return filePath;
    }
    public async Task OpenPdfAsync(string filePath)
    {
        if (File.Exists(filePath))
        {
            await Launcher.OpenAsync(new OpenFileRequest
            {
                File = new ReadOnlyFile(filePath)
            });
        }
        else
        {
            // Handle the error if the file does not exist
            Console.WriteLine("File not found.");
        }
    }

}