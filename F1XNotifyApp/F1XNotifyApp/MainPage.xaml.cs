using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using F1XNotifyApp.Model;
using Google.Api.Ads.AdWords.v201809;

namespace F1XNotifyApp
{
    public partial class MainPage : ContentPage
    {
        private INotificationsReceiver notificationsReceiver;
        public MainPage()
        {
            notificationsReceiver = DependencyService.Get<INotificationsReceiver>();
            notificationsReceiver.NotificationReceived += NotificationsReceiver_NotificationReceived;
            notificationsReceiver.ErrorReceived += NotificationsReceiver_ErrorReceived;
            InitializeComponent();
        }

        private void NotificationsReceiver_ErrorReceived(object sender, string e)
        {
            Dispatcher.BeginInvokeOnMainThread(() =>
            {
                TitleLabel.TextColor = Color.Red;
                TitleLabel.Text = "[Notification Error]";
                ContentLabel.TextColor = Color.Red;
                ContentLabel.Text = e.ToString();
            });
        }

        private void NotificationsReceiver_NotificationReceived(object sender, string[] e)
        {
            Dispatcher.BeginInvokeOnMainThread(() =>
            {
                TitleLabel.TextColor = Color.Blue;
                TitleLabel.Text = "[" + e[0] + "]";
                ContentLabel.TextColor = Color.Black;
                ContentLabel.Text = e[1];
            });
        }
    }
}
