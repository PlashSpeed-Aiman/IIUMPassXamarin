using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using IIUMPassXamarin.Helpers;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace IIUMPassXamarin
{
    public partial class MainPage
    {
        private const string WIFI_LOGIN_URL = "https://captiveportalmahallahgombak.iium.edu.my/cgi-bin/login";
        private const string WIFI_LOGOUT_URL = "https://captiveportal-login.iium.edu.my/cgi-bin/login?cmd=logout";
        private HttpClientHandler httpHandler;
        private HttpClient httpClient;
        private Task tsk;
        
        
        public MainPage()
        {
            InitializeComponent();
            tsk = new Task(async () =>
            {
                await ProgressBarConnecting.ProgressTo(1, 5000, Easing.SinIn);
                ProgressBarConnecting.Progress = 0.0;
            });
            Connectivity.ConnectivityChanged += Connectivity_ConnectivityChanged;
            WifiLabel.Text = "Current Connection : " + DependencyService.Get<IWifiLister>().StateWifi().Replace("\"","");
            EntryNum.Text = String.Empty;
            EntryPass.Text = String.Empty;
            httpHandler = new HttpClientHandler();
            httpHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
            httpClient = new HttpClient(httpHandler);
        }
        protected override void OnAppearing(){
            WifiLabel.Text = "Current Connection : " + DependencyService.Get<IWifiLister>().StateWifi().Replace("\"","");

        }

        private void Connectivity_ConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
        {
            WifiLabel.Text = "Current Connection : " + DependencyService.Get<IWifiLister>().StateWifi().Replace("\"","");

        }

        private async void Button_OnClicked(object sender, EventArgs e)
        {
            if (EntryNum.Text.Equals(String.Empty) || EntryPass.Text.Equals(String.Empty))
            {
               await this.DisplayToastAsync("MISSING DATA");
            }
            else
            {
                var username = EntryNum.Text.Trim();
                var password = EntryPass.Text.Trim();
                Preferences.Set("user_name",username);
                Preferences.Set("pass_key",password);
                EntryNum.Text  = String.Empty;
                EntryPass.Text = String.Empty;
                await this.DisplayToastAsync("Matric Number & Password Saved");
            }
            
        }

        private async void Button_OnClicked2(object sender, EventArgs e)
        {
            var token = new CancellationTokenSource();
            var content = LoginHelper.CreateForm();
            try
            {
               
                LoginButton.IsEnabled = false;
                LogoutButton.IsEnabled = false;
                var _ = Task.Run(async () =>
                {
                    await ProgressBarConnecting.ProgressTo(1, 3000, Easing.SinIn);
                },token.Token);
                
                var result = await httpClient.PostAsync(WIFI_LOGIN_URL, content);
                string resultContent = await result.Content.ReadAsStringAsync();
                Trace.WriteLine(resultContent);
                token.Cancel();
                await ProgressBarConnecting.ProgressTo(0.0,1000,Easing.SinIn);
                await this.DisplayToastAsync("Connection Successful");
                LoginButton.IsEnabled = true;
                LogoutButton.IsEnabled = true;

            }
            catch (Exception ew)
            {
                token.Cancel();
                await ProgressBarConnecting.ProgressTo(0.0,2000,Easing.SinIn);
                await this.DisplayToastAsync("EXCEPTION UNHANDLED! Maybe You Have Already Logged-In",durationMilliseconds:1500);
                Label1.Text = ew.Message;
                LoginButton.IsEnabled = true;
                LogoutButton.IsEnabled = true;
            }
        }

        private async void OnClicked_Logout(object sender, EventArgs e)
        {
            try
            {
                var result = await httpClient.GetAsync(WIFI_LOGOUT_URL);
                string resultContent = await result.Content.ReadAsStringAsync();
                Trace.WriteLine(resultContent);
                await this.DisplayToastAsync("Logout Successful! (Maybe)");

            }
            catch (Exception ew)
            {
                Trace.WriteLine(ew.ToString());
                await this.DisplayToastAsync("EXCEPTION UNHANDLED");
                Label1.Text = ew.Message;

            }
        }

        private async void ProgressButtonTest_OnClicked(object sender, EventArgs e)
        {
            await ProgressBarConnecting.ProgressTo(1, 5000, Easing.SinIn);
            await ProgressBarConnecting.ProgressTo(0.0, 1000, Easing.SinIn);
            

        }
    }
}

