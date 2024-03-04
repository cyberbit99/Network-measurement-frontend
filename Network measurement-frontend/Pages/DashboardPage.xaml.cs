using Network_measurement_frontend.Shared;
using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace Network_measurement_frontend.Pages;

public partial class DashboardPage : ContentPage
{
    public ObservableCollection<MeasurementReport> Reports { get; set; } = new ObservableCollection<MeasurementReport>();
    public event EventHandler ActiveReportChanged;
    public DashboardPage()
    {
        InitializeComponent();
        BindingContext = this;
        BtnSetActive.IsEnabled = false;
        itemListView.ItemSelected += (sender, e) =>
        {
            BtnSetActive.IsEnabled = itemListView.SelectedItem != null;
        };
    }
    protected override async void OnAppearing()
    {
        LblGreeting.Text = "Welcome " + Session.Instance().GetUser().Firstname;
        SetActive();
        base.OnAppearing();

        BtnSetActive.IsEnabled = false;

        // Fetch the list of items from the backend API
        var fetchedItems = await FetchItems();

        // Clear the existing items and add the fetched items to the collection
        Reports.Clear();
        if (fetchedItems != null)
        {
            foreach (var item in fetchedItems)
            {
                Reports.Add(item);
            }
        }
    }
    private void SetActive()
    {
        if (Session.Instance().report != null)
        {
            ActiveReport.Text = Session.Instance().report.Name;
            ActiveReportChanged?.Invoke(this, EventArgs.Empty);
        }

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
        try
        {

        var response = await httpClient.GetStringAsync($"http://192.168.1.85:7037/api/getreport/{user.UserId}");
                return JsonConvert.DeserializeObject<List<MeasurementReport>>(response);
        }
        catch (Exception)
        {
            return null;
        }

    }

    private void BtnSetActive_Clicked(object sender, EventArgs e)
    {
        Session.Instance(itemListView.SelectedItem as MeasurementReport);
        SetActive();
    }
    private void BtnLogOut_Clicked(object sender, EventArgs e)
    {
        Session.LogOut();
        Shell.Current.GoToAsync("//LoginPage");
    }
}