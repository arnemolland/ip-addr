using System;
using Xamarin.Forms;

namespace FindMyIP.ViewModels
{
	public class CustomNavigationPage : NavigationPage
    {
        public CustomNavigationPage()
        {
            BarTextColor = Color.White;
            SetHasNavigationBar(this, false);
        }
    }
}
