using Network_measurement_frontend.Shared;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using Microsoft.Maui;
using Network_measurement_frontend.Shared.Model;

namespace Network_measurement_frontend.Pages;

public partial class ReportPage : ContentPage
{
    public ObservableCollection<MeasurementReport> Reports { get; set; } = new ObservableCollection<MeasurementReport>();

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

    private void BtnCreateNewReport_Clicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//NewReportPage");
    }

    private void BtnExport_Clicked(object sender, EventArgs e)
    {

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
}