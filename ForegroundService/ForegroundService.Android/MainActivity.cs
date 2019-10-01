using System;
using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Xamarin.Forms;
using ForegroundService.Models;
using Android.Content;
using ForegroundService.Droid.Services;
using ForegroundService.AllConstants;
using Android.Media;

namespace ForegroundService.Droid
{
    [Activity(Label = "ForegroundService", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());
            GetNotification();
            //GetSubscription();
            //HandleRecievedMessages();
        }

        private void GetNotification()
        {
            NotificationClass clas = new NotificationClass();
            //Intent intent = new Intent(this, typeof(NotificationClass));
            MessagingCenter.Subscribe<StartForegroundService>(this, "Show Notification", y =>
            {
                clas.IssueNotification();
                //IssueNotification();
              
              

            });

            MessagingCenter.Subscribe<StopForegroundService>(this, "Remove Notification", y =>
            {
                clas.RemoveNotification();
             
            });

            MessagingCenter.Subscribe<CancelledMessage>(this, "Update Notification", z =>
            {
                clas.UpdateNotification();
            });

        }

      

        //private void GetSubscription()
        //{
        //    MessagingCenter.Subscribe<StartForegroundService>(this, "StartService", x =>
        //    {
        //        Intent intent = new Intent(this, typeof(MusicService));
        //        intent.SetAction(MPConstants.ACTION_START);
        //        if (Android.OS.Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.O)
        //            {
        //                StartForegroundService(intent);
        //            }
        //            else
        //            {
        //                StartService(intent);
        //          }

        //    });

        //    MessagingCenter.Subscribe<StopForegroundService>(this, "StopService", y =>
        //    {
        //        Intent intent = new Intent(this, typeof(MusicService));
        //        intent.SetAction(MPConstants.ACTION_STOP);
        //        Toast.MakeText(this, "service stopped!", ToastLength.Long).Show();
        //        StopService(intent);

        //    });

        //}

        //private void HandleRecievedMessages()
        //{

        //    MessagingCenter.Subscribe<StartForegroundService>(this, "StartRunningMessage", x =>
        //    {
        //        var intent = new Intent(this, typeof(MyForegroundService));
        //        if (Android.OS.Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.O)
        //        {
        //            StartForegroundService(intent);
        //        }
        //        else
        //        {
        //            StartService(intent);
        //        }

        //    });

        //    MessagingCenter.Subscribe<StopForegroundService>(this, "Stop Running Message", y =>
        //    {
        //        var intents = new Intent(this, typeof(MyForegroundService));
        //        StopService(intents);

        //        var cancel = new CancelledMessage();
        //        Device.BeginInvokeOnMainThread(() =>
        //        {
        //            MessagingCenter.Send(cancel, "Clear UI", "Cancelled");
        //        });


        //    });
        //}

        //public void IssueNotification()
        //{
        //    if (Build.VERSION.SdkInt >= BuildVersionCodes.O)
        //    {
        //        //create Notification Channel
        //        NotificationChannel channel = new NotificationChannel(MyConstants.CHANNEL_ID, MyConstants.CHANNEL_NAME, NotificationImportance.Default);
        //        channel.EnableVibration(true);
        //        channel.SetVibrationPattern(
        //       new long[]
        //       {500,500,500,500});
        //        //set visibility on lockscreen
        //        channel.LockscreenVisibility = NotificationVisibility.Private;

        //        //create the channel
        //        NotificationManager manager = (NotificationManager)GetSystemService(NotificationService);
        //        manager.CreateNotificationChannel(channel);

        //        //build the notification
        //        var Notification = new Notification.Builder(this, MyConstants.CHANNEL_ID);
        //        Notification.SetContentTitle("This is the Notification Title")
        //           .SetContentText("Testing Notification right now!")
        //            .SetSmallIcon(Resource.Drawable.smallicon)
        //            .SetSound(RingtoneManager.GetDefaultUri(RingtoneType.Alarm));
        //        //Notification.SetPriority(NotificationPriority.High);

        //        //.SetLargeIcon(BitmapFactory.DecodeResource(GetDrawable(Resource.Drawable.badge), Resource.Drawable.badge);
        //        //.build the notification
        //        Notification notification = Notification.Build();
        //        //issue the Notification
        //        NotificationManager manager2 = NotificationManager.FromContext(this);
        //        manager2.Notify(MyConstants.NOTIFICATION_ID, notification);
        //    }

        //    else
        //    {
        //        var notification = new Notification.Builder(this, MyConstants.CHANNEL_ID_2);
        //        notification.SetContentTitle("This is the other notification")
        //            .SetContentText("This is the body of the other notification")
        //            .SetSmallIcon(Resource.Drawable.badge);
        //        Notification Notiffy = notification.Build();
        //        NotificationManager manager3 = NotificationManager.FromContext(this);
        //        manager3.Notify(MyConstants.NOTIFICATION_ID_2, Notiffy);
        //    }


        //}

        //private void RemoveNotification()
        //{
        //    NotificationManager mNotifyManager = (NotificationManager)GetSystemService(NotificationService);
        //    if (Build.VERSION.SdkInt >= BuildVersionCodes.O)
        //    {
        //        mNotifyManager.Cancel(MyConstants.NOTIFICATION_ID);
        //    }
        //    else
        //    {
        //        mNotifyManager.Cancel(MyConstants.NOTIFICATION_ID_2);
        //    }


        //}


    }
    }
