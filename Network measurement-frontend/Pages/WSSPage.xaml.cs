#if ANDROID
using Network_measurement_frontend.Pages.Android;
#endif

namespace Network_measurement_frontend.Pages;

public partial class WSSPage : ContentPage
{
    public WSSPage()
    {
        InitializeComponent();
    }
    public void WSS()
    {
        //Android
#if ANDROID
        AndroidWSS androidWSS = new AndroidWSS();
        var list = androidWSS.GetWSS();
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
}