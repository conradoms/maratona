using Maratona.Helpers;
using Maratona.Services;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Maratona.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private readonly AzureService _azureService;
        public Command LoginCommand { get; }
        public string ComprasImg { get; set; }

        public LoginViewModel()
        {
            // Não utilizar token armazenado
            Settings.AuthToken = string.Empty;
            Settings.UserId = string.Empty;

            _azureService = DependencyService.Get<AzureService>();

            LoginCommand = new Command(async () => await ExecuteLoginCommandAsync());

            Title = "Login - Minhas Compras";
            ComprasImg = "compras.png";
        }

        private async Task ExecuteLoginCommandAsync()
        {
            if (IsBusy || !(await LoginAsync()))
                return;
            else
            {
                // var mainPage = new MainPage();
                await PushAsync<MainViewModel>();
            }
            IsBusy = false;
        }

        //private void RemovePageFromStack()
        //{
        //    var existingPages = navigation.NavigationStack.ToList();
        //    foreach (var page in existingPages)
        //    {
        //        if (page.GetType() == typeof(LoginPage))
        //        {
        //            navigation.RemovePage(page);
        //        }
        //    }
        //}

        public Task<bool> LoginAsync()
        {
            if (Settings.IsLoggedIn)
                return Task.FromResult(true);

            return _azureService.LoginAsync();
        }


    }
}
