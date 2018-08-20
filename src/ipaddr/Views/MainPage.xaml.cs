using System.Threading.Tasks;
using Xamarin.Forms;

namespace ipaddr.Views
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            Task.Delay(500);
            this.CommandTitle.FadeTo(0, 1000, Easing.SinInOut);
        }
    }
}
