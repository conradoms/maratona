using Microsoft.WindowsAzure.MobileServices;
using Maratona.Authentication;
using Maratona.Helpers;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Collections.Generic;
using Maratona.Model;
using System.Net.Http;
using Newtonsoft.Json;

[assembly: Xamarin.Forms.Dependency(typeof(Maratona.Services.AzureService))]
namespace Maratona.Services
{
    public class AzureService
    {
        static readonly string AppUrl = "http://notification-xamarin.azurewebsites.net/";
        public MobileServiceClient Client { get; set; } = null;
        List<AppServiceIdentity> identities = null;

        public void Initialize()
        {
            Client = new MobileServiceClient(AppUrl);

            if (!string.IsNullOrWhiteSpace(Settings.AuthToken) && !string.IsNullOrWhiteSpace(Settings.UserId))
            {
                Client.CurrentUser = new MobileServiceUser(Settings.UserId)
                {
                    MobileServiceAuthenticationToken = Settings.AuthToken
                };
            }
        }

        public async Task<bool> LoginAsync()
        {
            Initialize();
            
            var auth = DependencyService.Get<IAutentication>();
            var user = await auth.LoginAsync(Client, MobileServiceAuthenticationProvider.Facebook);

            if (user == null)
            {
                Settings.AuthToken = string.Empty;
                Settings.UserId = string.Empty;

                Device.BeginInvokeOnMainThread(async () =>
                {
                    await App.Current.MainPage.DisplayAlert("Ops!", "Não conseguimos efetuar o seu login, tente mais tarde!", "OK");
                });
                return false;
            }
            else
            {
                Settings.AuthToken = user.MobileServiceAuthenticationToken;
                Settings.UserId = user.UserId;

                identities = await Client.InvokeApiAsync<List<AppServiceIdentity>>("/.auth/me");

                Settings.UserName = identities[0].UserClaims.Find(c => c.Type.Contains("givenname")).Value;
                var userToken = identities[0].AccessToken;
                var requestUrl = $"https://graph.facebook.com/v2.9/me/?fields=picture.type(normal)&access_token={userToken}";
                var httpClient = new HttpClient();
                var userJson = await httpClient.GetStringAsync(requestUrl);
                var facebookProfile = JsonConvert.DeserializeObject<FacebookProfile>(userJson);

                Settings.UserImage = facebookProfile.Picture.Data.Url;
            }

            return true;
        }
    }
}