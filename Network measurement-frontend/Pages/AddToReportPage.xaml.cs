using Network_measurement_frontend.Shared;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using Microsoft.Maui;
using Network_measurement_frontend.Shared.Model;

namespace Network_measurement_frontend.Pages;

public partial class AddToReportPage : ContentPage
{
    public ObservableCollection<MeasurementReport> Reports { get; set; } = new ObservableCollection<MeasurementReport>();
    public ObservableCollection<object> Items { get; set; } = new ObservableCollection<object>();

    public int Type { get; set; }
    public AddToReportPage(List<object> initialItems, int type)
    {
        InitializeComponent();
        foreach (var item in initialItems)
        {
            Items.Add(item);
        }
        Type = type;

        // Set the BindingContext to the collection of items
        BindingContext = this;
    }
    protected override async void OnAppearing()
    {
        base.OnAppearing();

        // Fetch the list of items from the backend API
        var fetchedItems = await FetchItems();

        // Clear the existing items and add the fetched items to the collection
        Reports.Clear();
        foreach (var item in fetchedItems)
        {
            Reports.Add(item);
        }
        ;
    }
    private async Task<List<MeasurementReport>> FetchItems()
    {
        // Use HttpClient or your preferred HTTP client to make a request to the API
        // Deserialize the response to a list of items
        // Return the list of items
        Session session = Session.Instance(null as User);
        User user = session.GetUser();
        var httpClient = new HttpClient();
        var response = await httpClient.GetStringAsync($"http://192.168.1.85:7037/api/getreport/{user.UserId}");
        return JsonConvert.DeserializeObject<List<MeasurementReport>>(response);
    }

    private async void BtnAddToSelected_Clicked(object sender, EventArgs e)
    {
        if (itemListView.SelectedItem != null)
        {
            MeasurementReport report = itemListView.SelectedItem as MeasurementReport;
            await SaveMeasuerement(Items, Type, report.MeasurementReportId );

        }
        await Navigation.PopAsync();
    }
    private async Task SaveMeasuerement(ObservableCollection<object> list, int type, int reportid)
    {
        Measurement measurement = new Measurement();
        string target = "http://192.168.1.85:7037/api/cou/measurement";
        var client = new HttpClient();
        var request = new HttpRequestMessage(HttpMethod.Post, target);

        measurement.MeasuredData = JsonConvert.SerializeObject(list);
        measurement.CreatedAt = DateTime.Now;
        measurement.MeasurementReportId = reportid;
        measurement.MeasurementTypeId = type;

        request.Content = CreateContent(measurement);
        await client.SendAsync(request);
    }
    private async void BtnAddToActive_Clicked(object sender, EventArgs e)
    {
        Session session = Session.Instance(null as User);
        await SaveMeasuerement(Items, Type, session.GetReport().MeasurementReportId);
        await Navigation.PopAsync();
    }
    private StringContent CreateContent(object ob)
    {
        string json = JsonConvert.SerializeObject(ob);
        var httpContent = new StringContent(json, null, "application/json");

        return httpContent;
    }

    private void BtnCancel_Clicked(object sender, EventArgs e)
    {
        Navigation.PopAsync();
    }
}