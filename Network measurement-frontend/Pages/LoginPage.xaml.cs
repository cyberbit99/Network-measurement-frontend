using Network_measurement_frontend.Shared;
using Network_measurement_frontend.Shared.Model;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using JsonSerializer = System.Text.Json.JsonSerializer;

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

    private async void btn_Login(object sender, EventArgs e)
    {
        string target = "http://192.168.1.85:7037/api/loginsession";
        var client = new HttpClient();
        var request = new HttpRequestMessage(HttpMethod.Post, target);
        request.Content = CreateContent();
        var response = await client.SendAsync(request);
        var json = await response.Content.ReadAsStringAsync();
        if (json != null && response.IsSuccessStatusCode)
        {
            User? user = JsonSerializer.Deserialize<List<User>>(json).First();
            Session.Instance(user);
        }
        if (response.IsSuccessStatusCode)
        {
            Shell.Current.GoToAsync("//HomePage");
        }
    }
    private async Task<HttpResponseMessage> GetSession()
    {
        string target = "http://192.168.1.85:7037/api/loginsession";
        var client = new HttpClient();
        var request = new HttpRequestMessage(HttpMethod.Post, target);
        request.Content = CreateContent();
        var response = await client.SendAsync(request);
        response.EnsureSuccessStatusCode();
        //Session.Instance(JsonConvert.DeserializeObject<User>(await response.Content.ReadAsStringAsync()));
        return response;
    }
    private StringContent CreateContent()
    {
        LoginData loginData = new LoginData(EntEmail.Text, EntPassword.Text);
        string json = JsonConvert.SerializeObject(loginData);
        var httpContent = new StringContent(json, null, "application/json");

        return httpContent;
    }


}