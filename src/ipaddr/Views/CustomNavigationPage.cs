using System;
using Xamarin.Forms;

namespace ipaddr.ViewModels
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
