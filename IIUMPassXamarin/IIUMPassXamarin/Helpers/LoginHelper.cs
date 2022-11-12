using System.Collections.Generic;
using System.Net.Http;
using Xamarin.Essentials;

namespace IIUMPassXamarin.Helpers
{
    public static class LoginHelper
    {
        public static FormUrlEncodedContent CreateForm()
        {
            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("user", Preferences.Get("user_name", "")),
                new KeyValuePair<string, string>("password", Preferences.Get("pass_key", "")),
                new KeyValuePair<string, string>("url", "http://www.iium.edu.my/"),
                new KeyValuePair<string, string>("cmd", "authenticate"),
                new KeyValuePair<string, string>("Login", "Log In"),
            });

            return content;
        }
    }
}