using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using F1XNotifyApp.Droid.Model;
using F1XNotifyApp.Model;

[assembly:Xamarin.Forms.Dependency(typeof(NotificationsReceiver))]
namespace F1XNotifyApp.Droid.Model
{
    class NotificationsReceiver : INotificationsReceiver
    {
        public event EventHandler<string[]> NotificationReceived;
        public event EventHandler<string> ErrorReceived;

        public void RaiseErrorReceived(string message)
        {
            ErrorReceived?.Invoke(this, message);
        }

        public void RaiseNotificationReceived(string[] message)
        {
            NotificationReceived?.Invoke(this, message);
        }
    }
}