namespace Network_measurement_frontend.Pages;

public partial class ReportEditorPage : ContentPage
{
	public ReportEditorPage()
	{
		InitializeComponent();
	}

    private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//HomePage");
    }
}