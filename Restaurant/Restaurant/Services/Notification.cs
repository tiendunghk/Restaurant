﻿using Com.OneSignal.Abstractions;
using Newtonsoft.Json;
using Restaurant.Services.Navigation;
using Restaurant.ViewModels;
using Restaurant.ViewModels.Order;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using Xamarin.Forms;

namespace Restaurant.Services
{
    public static class Notification
    {
        public enum NotiFlag
        {
            SENDTOCASHIER,
            PAYDONE
        }
        public static void HandleNotificationReceived(OSNotification notification)
        {
            var a = notification.payload.body;
            MessagingCenter.Send("a", "OnNotificationReceived", a);
        }
        public static async void HandleNotificationOpened(OSNotificationOpenedResult result)
        {
            var data = result.notification.payload.additionalData;
            if (data.TryGetValue("flag", out var flag))
            {
                var notiflag = (NotiFlag)int.Parse(flag.ToString());
                switch (notiflag)
                {
                    case NotiFlag.SENDTOCASHIER:
                        await ServiceLocator.Instance.Resolve<INavigationService>().NavigateToAsync<ListOrderViewModel>();
                        break;
                    default:
                        break;
                }
            }

        }
        public static void Push(object addData = null)
        {
            var request = WebRequest.Create("https://onesignal.com/api/v1/notifications") as HttpWebRequest;

            request.KeepAlive = true;
            request.Method = "POST";
            request.ContentType = "application/json; charset=utf-8";

            request.Headers.Add("authorization", "Basic MWQyY2VmOTktOTEyZi00YjhlLWJkZTgtYzljMTdlMGFmZWUy");

            var obj = new
            {
                app_id = "511f254e-f0fe-4353-856d-1ac41bee6027",
                contents = new { en = "English Message" },
                included_segments = new string[] { "All" },
                smallIcon = "ic_noti"
            };
            var param = JsonConvert.SerializeObject(obj);
            byte[] byteArray = Encoding.UTF8.GetBytes(param);

            string responseContent = null;

            try
            {
                using (var writer = request.GetRequestStream())
                {
                    writer.Write(byteArray, 0, byteArray.Length);
                }

                using (var response = request.GetResponse() as HttpWebResponse)
                {
                    using (var reader = new StreamReader(response.GetResponseStream()))
                    {
                        responseContent = reader.ReadToEnd();
                    }
                }
            }
            catch (WebException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(new StreamReader(ex.Response.GetResponseStream()).ReadToEnd());
            }

            System.Diagnostics.Debug.WriteLine(responseContent);
        }
        public static void PushExternalID(object addData = null, List<string> externalIds = null, string content = null)
        {
            var request = WebRequest.Create("https://onesignal.com/api/v1/notifications") as HttpWebRequest;

            request.KeepAlive = true;
            request.Method = "POST";
            request.ContentType = "application/json; charset=utf-8";

            request.Headers.Add("authorization", "Basic MWQyY2VmOTktOTEyZi00YjhlLWJkZTgtYzljMTdlMGFmZWUy");

            var obj = new
            {
                app_id = "511f254e-f0fe-4353-856d-1ac41bee6027",
                contents = new { en = content },
                include_external_user_ids = externalIds.ToArray(),
                small_icon = "ic_noti.png",
                data = addData
            };
            var param = JsonConvert.SerializeObject(obj);
            byte[] byteArray = Encoding.UTF8.GetBytes(param);

            string responseContent = null;

            try
            {
                using (var writer = request.GetRequestStream())
                {
                    writer.Write(byteArray, 0, byteArray.Length);
                }

                using (var response = request.GetResponse() as HttpWebResponse)
                {
                    using (var reader = new StreamReader(response.GetResponseStream()))
                    {
                        responseContent = reader.ReadToEnd();
                    }
                }
            }
            catch (WebException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(new StreamReader(ex.Response.GetResponseStream()).ReadToEnd());
            }

            System.Diagnostics.Debug.WriteLine(responseContent);
        }
    }
}
