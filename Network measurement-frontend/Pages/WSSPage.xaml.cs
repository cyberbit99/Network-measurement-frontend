#if ANDROID
using Network_measurement_frontend.Pages.Android;
using System.Net.NetworkInformation;
#endif

namespace Network_measurement_frontend.Pages;

public partial class WSSPage : ContentPage
{
    public List<object> WSSItems { get; set; } = new List<object>();
    public WSSPage()
    {
        InitializeComponent();
        var x = new DashboardPage();
        x.ActiveReportChanged += (sender, e) =>
        {
            WSSItems.Clear();
        };
    }
    public void WSS()
    {


        //Android
#if ANDROID
        
#endif
        //iOS

#if IOS

#endif
        //Windows
#if WINDOWS

#endif
        //macOS
#if MACCATALYST

#endif
    }
    private async void BtnAdd_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new AddToReportPage(WSSItems));
    }

    private void BtnMeasure_Clicked(object sender, EventArgs e)
    {
        WSS();
    }
}