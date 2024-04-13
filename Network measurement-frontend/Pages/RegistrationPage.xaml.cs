using Network_measurement_frontend.Shared;
using Network_measurement_frontend.Shared.Model;
using Newtonsoft.Json;

namespace Network_measurement_frontend.Pages;

public partial class RegistrationPage : ContentPage
{
	public RegistrationPage()
	{
		InitializeComponent();
	}

    private void BtnRegistrationCancel(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//LoginPage");
    }

    private async Task RegistrationAsync()
    {
        string target = "http://192.168.1.85:7037/api/cou/user";
        var client = new HttpClient();
        var request = new HttpRequestMessage(HttpMethod.Post, target);
        if (EntPassword.Text == EntPassword2.Text)
        {
            request.Content = CreateContent();
            var response = await client.SendAsync(request);
            var json = await response.Content.ReadAsStringAsync();
            if (json != null && response.IsSuccessStatusCode)
            {
                await Shell.Current.GoToAsync("//LoginPage");
            }
        }
        
    }
    private StringContent CreateContent()
    {
        User regData = new User(EntUsername.Text,EntEmail.Text,EntPassword.Text,1,EntFirstname.Text,EntLastname.Text);
        string json = JsonConvert.SerializeObject(regData);
        var httpContent = new StringContent(json, null, "application/json");

        return httpContent;
    }

    private async void BtnRegister_Clicked(object sender, EventArgs e)
    {
        await RegistrationAsync();
    }
}