using System;

using Android.App;
using Android.Content.PM;
using Android.Gms.Common;
using Android.Runtime;
using Android.OS;
using F1XNotifyApp.Model;
using Xamarin.Forms;

namespace F1XNotifyApp.Droid
{
    [Activity(Label = "F1XNotifyApp", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize )]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        private const string ChannelId = "F1PushNotificationTest.Channel";
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());

            var receiver = DependencyService.Get<INotificationsReceiver>();
            receiver.RaiseNotificationReceived(new string[] { "초기화", "Registering..." });

            if (IsPlayServicesAvailable(receiver))
            {
                CreateNotificationChannel();
                receiver.RaiseNotificationReceived(new string[] { "초기화", "Ready..." });
            }
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
        private bool IsPlayServicesAvailable(INotificationsReceiver receiver)
        {
            int resultCode = GoogleApiAvailability.Instance.IsGooglePlayServicesAvailable(this);

            if (resultCode != ConnectionResult.Success)
            {
                if (GoogleApiAvailability.Instance.IsUserResolvableError(resultCode))
                {
                    receiver.RaiseErrorReceived(GoogleApiAvailability.Instance.GetErrorString(resultCode));
                }
                else
                {
                    receiver.RaiseErrorReceived("This device is not supported");
                    Finish();
                }
                return false;
            }
            else
            {
                receiver.RaiseNotificationReceived(new string[] { "서비스 상태", "Google Play Services is available." });
                return true;
            }
        }
        private void CreateNotificationChannel()
        {
            if (Build.VERSION.SdkInt < BuildVersionCodes.O)
            {
                // Notification channels are new in API 26 (and not a part of the
                // support library). There is no need to create a notification
                // channel on older versions of Android.
                return;
            }

            var channel = new NotificationChannel(
                ChannelId,
                "Notifications for F1PushTest",
                NotificationImportance.Default);

            var notificationManager = (NotificationManager)GetSystemService(NotificationService);
            notificationManager.CreateNotificationChannel(channel);
        }
    }
}