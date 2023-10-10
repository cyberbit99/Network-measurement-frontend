namespace Network_measurement_frontend.Pages;

public partial class RegistrationPage : ContentPage
{
	public RegistrationPage()
	{
		InitializeComponent();
	}

    private void btn_RegistrationCancel(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//LoginPage");
    }

    private void btn_Registration(object sender, EventArgs e)
    {
        //call here a function for send date to backend
    }
}