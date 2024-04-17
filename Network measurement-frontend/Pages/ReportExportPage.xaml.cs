namespace Network_measurement_frontend.Pages;

public partial class ReportExportPage : ContentPage
{
	public ReportExportPage()
	{
        InitializeComponent();
    }

    private async void BtnCancel_Clicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }

    private void BtnExportSelected_Clicked(object sender, EventArgs e)
    {


    }
}