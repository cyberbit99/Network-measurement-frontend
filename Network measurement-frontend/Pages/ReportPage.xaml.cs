namespace Network_measurement_frontend.Pages;

public partial class ReportPage : ContentPage
{
	public ReportPage()
	{
		InitializeComponent();
	}

    private void btn_Edit_Selected(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//ReportEditorPage");
     }

    private void BtnCreateNewReport_Clicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//NewReportPage");
    }

    private void BtnExport_Clicked(object sender, EventArgs e)
    {

    }

    private void BtnDeleteSelected_Clicked(object sender, EventArgs e)
    {

    }
}