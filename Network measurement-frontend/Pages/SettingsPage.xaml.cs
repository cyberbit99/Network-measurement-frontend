namespace Network_measurement_frontend.Pages;

public partial class SettingsPage : ContentPage
{
	public SettingsPage()
	{
		InitializeComponent();
	}

    private void BtnLogOut_Clicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//LoginPage");
    }
}