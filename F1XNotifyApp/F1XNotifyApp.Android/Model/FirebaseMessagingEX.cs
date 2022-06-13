using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using F1XNotifyApp.Model;
using Firebase.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace F1XNotifyApp.Droid.Model
{
    [Service(Exported = false)]
    [IntentFilter(new[] {"com.google.firebase.MESSAGING_EVENT"})]
    public class FirebaseMessagingEX : FirebaseMessagingService
    {
        public override void OnMessageReceived(RemoteMessage p0)
        {
            base.OnMessageReceived(p0);

            var receiver = DependencyService.Get<INotificationsReceiver>();

            var notification = p0.GetNotification();
            if(notification != null)
            {
                string[] strArr = new string[] { notification.Title, notification.Body };
                receiver.RaiseNotificationReceived(strArr);
            }
        }
        public override void OnNewToken(string p0)
        {
            base.OnNewToken(p0);

            System.Diagnostics.Debug.WriteLine("[newToken]:" + p0);
        }
    }
}