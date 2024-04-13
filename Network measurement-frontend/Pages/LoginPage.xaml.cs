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
    HttpResponseMessage response = new HttpResponseMessage();
    bool warning = false;

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
        try
        {
            string target = "http://192.168.1.85:7037/api/loginsession";
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, target);
            request.Content = CreateContent();
            response = await client.SendAsync(request);
            var json = await response.Content.ReadAsStringAsync();
            if (json != null && response.IsSuccessStatusCode)
            {
                Warning.IsVisible = false;
                var user = JsonSerializer.Deserialize<List<User>>(json).First();
                if (user != null)
                {
                    Session.Instance(user);
                    await Shell.Current.GoToAsync("//HomePage");
                }

            }
            else
            {
                Warningmsg.Text = json.ToString();
                Warning.IsVisible = true;
            }
        }
        catch (Exception ex)
        {
            // Handle the exception and show the error message
            EntUsername.BackgroundColor = (Color.FromRgb(100, 100, 100));
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
        LoginData loginData = new LoginData(EntUsername.Text, EntPassword.Text);
        string json = JsonConvert.SerializeObject(loginData);
        var httpContent = new StringContent(json, null, "application/json");
        return httpContent;
    }


}