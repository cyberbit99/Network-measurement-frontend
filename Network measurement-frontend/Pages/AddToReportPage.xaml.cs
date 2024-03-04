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
    public AddToReportPage(List<object> initialItems)
    {
        InitializeComponent();
        foreach (var item in initialItems)
        {
            Items.Add(item);
        }

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

    private void BtnAddToSelected_Clicked(object sender, EventArgs e)
    {
        if (itemListView.SelectedItem != null)
        {


        }


        Navigation.PopAsync();
    }
    private async Task SaveMeasuerement(List<object> list)
    {
        Measurement measurement = new Measurement();
        string target = "http://192.168.1.85:7037/api/loginsession";
        var client = new HttpClient();
        var request = new HttpRequestMessage(HttpMethod.Post, target);
        measurement.MeasuredData = JsonConvert.SerializeObject(list);
        request.Content = CreateContent(measurement);
        client.SendAsync(request);
    }
    private void BtnAddToActive_Clicked(object sender, EventArgs e)
    {
        Navigation.PopAsync();
    }
    private StringContent CreateContent(object ob)
    {
        string json = JsonConvert.SerializeObject(ob);
        var httpContent = new StringContent(json, null, "application/json");

        return httpContent;
    }
}