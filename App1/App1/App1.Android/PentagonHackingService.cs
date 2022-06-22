using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using App1.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using GraphQLClient;
using App1.Droid;
using Xamarin.Forms;

[assembly: Dependency(typeof(PentagonHackingService))]
namespace App1.Droid
{
    internal class PentagonHackingService : IPentagonHackingService
    {
        public HttpClientHandler GetHttpClientHandler()
        {
                    HttpClientHandler handler = new HttpClientHandler
                    {
                       ServerCertificateCustomValidationCallback = (message, cert, chain, errors) =>
                        {
                           if (cert.Issuer.Equals("CN=localhost"))
                               return true;
                             return errors == System.Net.Security.SslPolicyErrors.None;
                       }
                    };
                    return handler;
            //return new Xamarin.Android.Net.AndroidClientHandler();
        }
    }
}