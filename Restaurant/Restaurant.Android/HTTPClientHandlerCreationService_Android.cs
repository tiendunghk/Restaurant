using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Restaurant.Services.Dependency;

namespace Restaurant.Droid
{
    public class HTTPClientHandlerCreationService_Android : IHTTPClientHandlerCreationService
    {
        public HttpClientHandler GetInsecureHandler()
        {
            throw new NotImplementedException();
        }
    }
}