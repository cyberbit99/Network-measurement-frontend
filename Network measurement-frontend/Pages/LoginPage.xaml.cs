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
                    Shell.Current.GoToAsync("//HomePage");
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
            EntUsername.BackgroundColor  = (Color.FromRgb(100,100,100));
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
        //access for development

        if (loginData.Email == "admin" && loginData.Password == "admin")
        {
            User u = new User{ Username="admin", Password="admin", Firstname="admin", Lastname="admin", UserEmail="admin@admin.hu", UserId=1, UserRoleId=2};
            Session.Instance(u);
            Shell.Current.GoToAsync("//HomePage");
        }
        string json = JsonConvert.SerializeObject(loginData);
        var httpContent = new StringContent(json, null, "application/json");

        return httpContent;
    }


}