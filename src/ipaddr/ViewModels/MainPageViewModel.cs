using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Prism.AppModel;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using ipaddr.Resources;
using System.Net;
using System.Net.Sockets;
using Xamarin.Essentials;
using System.Threading.Tasks;
using System.Net.NetworkInformation;

namespace ipaddr.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        public MainPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService,
                                 IDeviceService deviceService)
            : base(navigationService, pageDialogService, deviceService)
        {
            Title = AppResources.MainPageTitle;
            LocalIp = "loading...";
            PublicIp = "loading...";
            MacAdress = "loading...";
            WiperOpacity = 1.0;
        }

        public string LocalIp { get; set; }
        public string PublicIp { get; set; }
        public string MacAdress { get; set; }
        public double WiperOpacity { get; set; }

        public override async void OnNavigatingTo(NavigationParameters parameters)
        {
            // TODO: Implement your initialization logic
            await UpdateIp();
        }

        public override void OnNavigatedFrom(NavigationParameters parameters)
        {
            // TODO: Handle any final tasks before you navigate away
        }

        public override void OnNavigatedTo(NavigationParameters parameters)
        {
            switch (parameters.GetNavigationMode())
            {
                case NavigationMode.Back:
                    // TODO: Handle any tasks that should occur only when navigated back to
                    break;
                case NavigationMode.New:
                    // TODO: Handle any tasks that should occur only when navigated to for the first time
                    break;
            }

            // TODO: Handle any tasks that should be done every time OnNavigatedTo is triggered
        }

        public async Task UpdateIp()
        {
            WiperOpacity = 1.0;
            await Task.Run(async() =>
            {
                if (Connectivity.NetworkAccess == NetworkAccess.None || Connectivity.NetworkAccess == NetworkAccess.Unknown)
                {
                    PublicIp = "not connected.";
                    await GetLocalIp();
                    await GetMac();
                    WiperOpacity = 0.0;
                    return;
                }
                else if (Connectivity.NetworkAccess == NetworkAccess.ConstrainedInternet)
                {
                    PublicIp = "network constrained.";
                    await GetLocalIp();
                    await GetMac();
                    WiperOpacity = 0.0;
                    return;
                }
                else
                {
                    await GetLocalIp();
                    await GetMac();
                    await GetPublicIp();
                    WiperOpacity = 0.0;
                    return;
                }
            });

        }

        public async Task GetLocalIp()
        {
            await Task.Run(() =>
            {
                try
                {
                    var host = Dns.GetHostEntry(Dns.GetHostName());
                    foreach (var ip in host.AddressList)
                    {
                        if (ip.AddressFamily == AddressFamily.InterNetwork)
                        {
                            LocalIp = ip.ToString();
                        }
                    }
                }
                catch(Exception ex)
                {
                    LocalIp = "failed.";
                    System.Diagnostics.Debug.WriteLine(ex.ToString());
                }
            });
        }

        public async Task GetPublicIp()
        {
            await Task.Run(() =>
            {
            // Get public IP adress
            using (var client = new WebClient())
            {
                try
                {
                    PublicIp = client.DownloadString("https://api.ipify.org");
                }
                catch (Exception ex)
                {
                    PublicIp = "not available.";
                        System.Diagnostics.Debug.WriteLine(ex.ToString());
                }
            }
        });
        }

        public async Task GetMac()
        {
            await Task.Run(() =>
            {
                var firstMacAddress = NetworkInterface
                    .GetAllNetworkInterfaces()
                    .Where(nic => nic.OperationalStatus == OperationalStatus.Up && nic.NetworkInterfaceType != NetworkInterfaceType.Loopback)
                    .Select(nic => nic.GetPhysicalAddress().ToString())
                    .FirstOrDefault();

                MacAdress = firstMacAddress;
            });
        }
    }
}