using Network_measurement_frontend.Shared;

namespace Network_measurement_frontend.Pages;

public partial class SettingsPage : ContentPage
{
    public SettingsPage()
    {
        InitializeComponent();
    }

    private void BtnLogOut_Clicked(object sender, EventArgs e)
    {
        Session.LogOut();
        Shell.Current.GoToAsync("//LoginPage");
    }
}