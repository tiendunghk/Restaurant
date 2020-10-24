﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Restaurant.Droid.Services;
using Restaurant.Services.Dialog;

[assembly:Xamarin.Forms.Dependency(typeof(Toast_Android))]
namespace Restaurant.Droid.Services
{
    public class Toast_Android : IToast
    {
        public void Show(string message)
        {
                Android.Widget.Toast.MakeText(Android.App.Application.Context, message, ToastLength.Short).Show();
        }
    }
}