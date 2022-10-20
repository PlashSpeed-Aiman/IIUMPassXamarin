using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace IIUMPassXamarin
{
    public partial class MainPage : ContentPage
    {
        private const string WIFI_LOGIN_URL = "https://captiveportalmahallahgombak.iium.edu.my/cgi-bin/login";
        private const string WIFI_LOGOUT_URL = "https://captiveportal-login.iium.edu.my/cgi-bin/login?cmd=logout";
        public MainPage()
        {
            
            InitializeComponent();
            EntryNum.Text = String.Empty;
            EntryPass.Text = String.Empty;
        }

        private async void Button_OnClicked(object sender, EventArgs e)
        {
            if (EntryNum.Text.Equals(String.Empty) || EntryPass.Text.Equals(String.Empty))
            {
               await this.DisplayToastAsync("MISSING DATA");
            }
            else
            {
                Preferences.Set("user_name",EntryNum.Text);
                Preferences.Set("pass_key",EntryPass.Text);
                EntryNum.Text=String.Empty;
                EntryPass.Text=String.Empty;
                await this.DisplayToastAsync("Matric Number & Password Saved");
            }
            
        }

        private async void Button_OnClicked2(object sender, EventArgs e)
        {
            using (var httpHandle = new HttpClientHandler())
            {
                httpHandle.ServerCertificateCustomValidationCallback  = (message, cert, chain, errors) => { return true; };
            using (var client = new HttpClient())
            {

                var content = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("user", Preferences.Get("user_name", "")),
                    new KeyValuePair<string, string>("password", Preferences.Get("pass_key", "")),
                    new KeyValuePair<string, string>("url", "http://www.iium.edu.my/"),
                    new KeyValuePair<string, string>("cmd", "authenticate"),
                    new KeyValuePair<string, string>("Login", "Log In"),
                });
                try
                {
                    ServicePointManager.ServerCertificateValidationCallback =
                        (message, certificate, chain, sslPolicyErrors) => true;
                    var result = await client.PostAsync(WIFI_LOGIN_URL, content);
                    string resultContent = await result.Content.ReadAsStringAsync();
                    Trace.WriteLine(resultContent);
                    await this.DisplayToastAsync("Connection Successful");

                }
                catch (Exception ew)
                {
                    Trace.WriteLine(ew);
                    await this.DisplayToastAsync("EXCEPTION UNHANDLED! Maybe You Have Already Logged-In");
                }
                
            } 
            }
    }

        private async void OnClicked_Logout(object sender, EventArgs e)
        {
            using (var httpHandle = new HttpClientHandler())
            {
                httpHandle.ServerCertificateCustomValidationCallback  = (message, cert, chain, errors) => { return true; };
                using (var client = new HttpClient(httpHandle))
                {
                    try
                    {
                        var result = await client.GetAsync(WIFI_LOGOUT_URL);
                        string resultContent = await result.Content.ReadAsStringAsync();
                        Trace.WriteLine(resultContent);
                        await this.DisplayToastAsync("Logout Successful! (Maybe)");

                    }
                    catch (Exception ew)
                    {
                        Trace.WriteLine(ew.ToString());
                        await this.DisplayToastAsync("EXCEPTION UNHANDLED");
                    }
                    
                }
            }
           
        }
    }
}
//Login dashboard
//Add option to explore app
//standardise pictures
//find recipes malaysia
//simulate purchase
//
