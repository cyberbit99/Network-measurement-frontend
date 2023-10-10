namespace Network_measurement_frontend.Pages;

public partial class LoginPage : ContentPage
{
	public LoginPage()
	{ 
		InitializeComponent();
	}

    private void btn_RegistrationPage_Jump(object sender, EventArgs e)
	{
		Shell.Current.GoToAsync("//RegistrationPage");
	}

	private void btn_Login(object sender, EventArgs e)
	{
		Shell.Current.GoToAsync("//HomePage");
	}
}