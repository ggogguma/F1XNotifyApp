using System;
using System.Collections.Generic;
using System.Text;

namespace F1XNotifyApp.Model
{
    public interface INotificationsReceiver
    {
        event EventHandler<string[]> NotificationReceived;
        event EventHandler<string> ErrorReceived;

        void RaiseNotificationReceived(string[] message);
        void RaiseErrorReceived(string message);
    }
}
