using Com.OneSignal.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Restaurant.Services
{
    public static class Notification
    {
        public static void HandleNotificationReceived(OSNotification notification)
        {
            var a = notification.payload.body;
            MessagingCenter.Send("a", "OnNotificationReceived", a);
        }
    }
}
