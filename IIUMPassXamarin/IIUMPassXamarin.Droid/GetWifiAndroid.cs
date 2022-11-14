using Android.Content;
using Android.Net;
using Android.Net.Wifi;
using IIUMPassXamarin.Droid;
using Xamarin.Forms;
[assembly: Dependency(typeof(GetSSIDAndroid))]
namespace IIUMPassXamarin.Droid
{
    
    public class GetSSIDAndroid:IWifiLister
    {
        public string StateWifi()
        
        {
            WifiManager wifiManager = (WifiManager)(Android.App.Application.Context.GetSystemService(Context.WifiService));

            if (wifiManager != null && !string.IsNullOrEmpty(wifiManager.ConnectionInfo.SSID))
            {
                return wifiManager.ConnectionInfo.SSID;
            }
            else
            {
                return "WiFiManager is NULL";
            }
        }
    }
}