using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IIUMPassXamarin
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UtilitiesPage : ContentPage
    {
        private string _whatsappname = String.Empty;
        private const string _whatsapplink = "https://wa.me/";
        
        public UtilitiesPage()
        {
            InitializeComponent();
            var res = _whatsapplink + "6"+ _whatsappname;
            Label.Text = res;
        }


        private void Button_OnClicked(object sender, EventArgs e)
        {
            _whatsappname = Regex.Replace(_whatsappname, "[^0-9.]", "");
            var res = _whatsapplink + "6"+ _whatsappname;
            Launcher.OpenAsync(new System.Uri(res));
        }

        private void InputView_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            _whatsappname = Entry.Text;
            _whatsappname = Regex.Replace(_whatsappname, "[^0-9.]", "");
            var res = _whatsapplink + "6"+ _whatsappname;
            Label.Text = res;
        }

        private async void TapGestureRecognizer_OnTapped(object sender, EventArgs e)
        {
            await Clipboard.SetTextAsync(_whatsapplink + "6" + _whatsappname);
            await this.DisplayToastAsync("Link Copied!");
        }

    }
}