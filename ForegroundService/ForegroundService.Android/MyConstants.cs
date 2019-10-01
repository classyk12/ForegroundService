using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace ForegroundService.Droid
{
   public static class MyConstants
    {
        //create Channel Id for the notification
        public const int NOTIFICATION_ID = 100;
        public const int NOTIFICATION_ID_2= 200;
        //channel ID for Notification Channel
        public const string CHANNEL_ID = "CHANNEL_ID";
        public const string CHANNEL_ID_2 = "CHANNEL_ID_2";
        //channel Name for Notification Channel
        public const string CHANNEL_NAME = "MY_CHANNEL_NAME";
        //importance for all notifications in this channel
        const int Importance = (int)NotificationImportance.Default;
      
    }
}