using System.Threading.Tasks;
using Prism.AppModel;
using Prism.Navigation;
using Prism.Services;

namespace FindMyIP.ViewModels
{
    public class SplashScreenPageViewModel : ViewModelBase
    {
        public SplashScreenPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService, IDeviceService deviceService)
            : base(navigationService, pageDialogService, deviceService)
        {
            // Horrible code, don't care..
            IVisibility = false;
            PVisibility = false;
            AVisibility = false;
            D1Visibility = false;
            D2Visibility = false;
            RVisibility = false;
            SVisibility = false;
            HVisibility = false;
            OVisibility = false;
            WVisibility = false;
        }

        public bool IVisibility { get; set; }
        public bool PVisibility { get; set; }
        public bool AVisibility { get; set; }
        public bool D1Visibility { get; set; }
        public bool D2Visibility { get; set; }
        public bool RVisibility { get; set; }
        public bool SVisibility { get; set; }
        public bool HVisibility { get; set; }
        public bool OVisibility { get; set; }
        public bool WVisibility { get; set; }

        public override async void OnNavigatedTo(NavigationParameters parameters)
        {
            // TODO: Implement any initialization logic you need here. Example would be to handle automatic user login

            // Simulated long running task. You should remove this in your app.
            await AnimateIntro();

            // After performing the long running task we perform an absolute Navigation to remove the SplashScreen from
            // the Navigation Stack.
            await _navigationService.NavigateAsync("/CustomNavigationPage/MainPage");
        }

        public async Task AnimateIntro()
        {
            // Wackiest animation ever, but who cares.
            IVisibility = true;
            await Task.Delay(150);
            PVisibility = true;
            await Task.Delay(250);
            AVisibility = true;
            await Task.Delay(100);
            D1Visibility = true;
            await Task.Delay(150);
            D2Visibility = true;
            await Task.Delay(150);
            RVisibility = true;
            await Task.Delay(250);
            SVisibility = true;
            await Task.Delay(150);
            HVisibility = true;
            await Task.Delay(150);
            OVisibility = true;
            await Task.Delay(150);
            WVisibility = true;
            await Task.Delay(150);
        }
    }
}