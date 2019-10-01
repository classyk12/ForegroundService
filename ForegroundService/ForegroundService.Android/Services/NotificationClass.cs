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
using Android.App;
using Android.Util;
using Android.Support.V4.App;
using System;
using Android.Graphics;
using Android.Content.Res;

namespace ForegroundService.Droid.Services
{
   
   public  class NotificationClass 
    {
       public void IssueNotification()
        {
            if(Build.VERSION.SdkInt >= BuildVersionCodes.O)
            {
                //create Notification Channel
                NotificationChannel channel = new NotificationChannel(MyConstants.CHANNEL_ID, MyConstants.CHANNEL_NAME, NotificationImportance.High);
                channel.EnableVibration(true);
                channel.SetVibrationPattern(
               new long[]
               {500,500,500,500});
                //set visibility on lockscreen
                channel.LockscreenVisibility = NotificationVisibility.Public;
                
               
                //create the channel
                NotificationManager manager = Android.App.Application.Context.GetSystemService(Context.NotificationService) as NotificationManager ;
                manager.CreateNotificationChannel(channel);

                //set an action to tske the user to the active activity when user clicks on notification
                Intent intent = new Intent(Android.App.Application.Context, typeof(MainActivity));
                intent.AddCategory(Intent.CategoryLauncher);
                intent.SetAction(Intent.ActionMain);

                //create pendingIntent
                PendingIntent pendingIntent = PendingIntent.GetActivity(Android.App.Application.Context, 1, intent, 0);


                //build the notification
                var builder  = new NotificationCompat.Builder(Android.App.Application.Context, MyConstants.CHANNEL_ID);
                builder.SetContentTitle("This is the Notification Title")
                   .SetContentText("Testing Notification right now!")
                    .SetSmallIcon(Resource.Drawable.ic_mr_button_grey)
                    .SetWhen(Java.Lang.JavaSystem.CurrentTimeMillis())
                    .SetSound(RingtoneManager.GetDefaultUri(RingtoneType.Ringtone))
                    .SetContentIntent(pendingIntent)
                .SetLargeIcon(BitmapFactory.DecodeResource(Android.App.Application.Context.Resources, Resource.Drawable.user));
              
                //create big text style notification in the expanded. This class does not support method chaining
                //you have to explitcitely use the NotificationBuilder object to call the BigTextStyle class
                //NotificationCompat.BigTextStyle bigtext = new NotificationCompat.BigTextStyle();
                //string gibberish = "This is the new big text style notification";
                //gibberish += "/and we are just testing it";
                //bigtext.BigText(gibberish);
                //bigtext.SetSummaryText("This is just a summary");
                ////push BigText into builder for the notification
                //builder.SetStyle(bigtext);

                ////used to create image notification
                //NotificationCompat.BigPictureStyle picturestyle = new NotificationCompat.BigPictureStyle();
                //picturestyle.BigPicture(BitmapFactory.DecodeResource(Android.App.Application.Context.Resources, Resource.Drawable.bsplash));
                //builder.SetStyle(picturestyle);

                //used to create an inbox type of email
                NotificationCompat.InboxStyle inboxstyle = new NotificationCompat.InboxStyle();
                inboxstyle.AddLine("Temilayo: I am waiting at the park")
                    .AddLine("Marko: The Tesla is ready for pickup")
                    .AddLine("Honey 2: Our flight is in 20 mins")
                    .SetSummaryText("+3 more");

                builder.SetStyle(inboxstyle);


                //Notification.SetPriority(NotificationPriority.High);
                //.build the notification
                Notification notification = builder.Build();
                //issue the Notification
                NotificationManager manager2 = NotificationManager.FromContext(Android.App.Application.Context);
                manager2.Notify(MyConstants.NOTIFICATION_ID, notification);        
            }

            else
            {
                //set an action to tske the user to the active activity when user clicks on notification
                Intent intent = new Intent(Android.App.Application.Context, typeof(MainActivity));
                //intent.SetFlags(ActivityFlags.ClearTop | ActivityFlags.SingleTop);
                intent.SetAction(Intent.ActionMain);
                intent.AddCategory(Intent.CategoryLauncher);
                //create pendingIntent
                PendingIntent pendingIntent = PendingIntent.GetActivity(Android.App.Application.Context, 2, intent, 0);

                var notification = new NotificationCompat.Builder(Android.App.Application.Context,MyConstants.CHANNEL_ID_2);
                notification.SetContentTitle("This is the other notification")
                    .SetContentText("This is the body of the other notification")
                    .SetSmallIcon(Resource.Drawable.ic_audiotrack_dark)
                    .SetWhen(Java.Lang.JavaSystem.CurrentTimeMillis())
                    .SetContentIntent(pendingIntent)
                    .SetLargeIcon(BitmapFactory.DecodeResource(Android.App.Application.Context.Resources, Resource.Drawable.smallicon));
                    
                //create big text style notification in the expanded. This class does not support method chaining
                //you have to explitcitely use the NotificationBuilder object to call the BigTextStyle class

                NotificationCompat.BigTextStyle bigtext = new NotificationCompat.BigTextStyle();
                string gibberish = "This is the new big text style notification";
                gibberish += "/and we are just testing it";
                bigtext.BigText(gibberish);
                bigtext.SetSummaryText("This is just a summary");
                //push BigText into builder for the notification
                notification.SetStyle(bigtext);
                notification.SetPriority(NotificationCompat.PriorityMax);

                ////used to create image notification
                //NotificationCompat.BigPictureStyle picturestyle = new NotificationCompat.BigPictureStyle();
                //picturestyle.BigPicture(BitmapFactory.DecodeResource(Android.App.Application.Context.Resources, Resource.Drawable.bsplash));
                //notification.SetStyle(picturestyle);

                //used to create an inbox type of email
                NotificationCompat.InboxStyle inboxstyle = new NotificationCompat.InboxStyle();
                inboxstyle.AddLine("Temilayo: I am waiting at the park")
                    .AddLine("Marko: The Tesla is ready for pickup")
                    .AddLine("Honey 2: Our flight is in 20 mins")
                    .SetSummaryText("+3 more");

                notification.SetStyle(inboxstyle);

                Notification Notiffy = notification.Build();
                NotificationManager manager3 = NotificationManager.FromContext(Android.App.Application.Context);
                manager3.Notify(MyConstants.NOTIFICATION_ID_2,Notiffy);
            }
        }


       public void RemoveNotification()
        {
            NotificationManager mNotifyManager = (NotificationManager)Android.App.Application.Context.GetSystemService(Context.NotificationService);
            if (Build.VERSION.SdkInt >= BuildVersionCodes.O)
            {
                mNotifyManager.Cancel(MyConstants.NOTIFICATION_ID);
            }
            else
            {
                mNotifyManager.Cancel(MyConstants.NOTIFICATION_ID_2);
            }


        }

        public void UpdateNotification()
        {
            if (Build.VERSION.SdkInt >= BuildVersionCodes.O)
            {
                //create Notification Channel
                NotificationChannel channel = new NotificationChannel(MyConstants.CHANNEL_ID, MyConstants.CHANNEL_NAME, NotificationImportance.Default);
                channel.EnableVibration(true);
                channel.SetVibrationPattern(
               new long[]
               {500,500,500,500});
                //set visibility on lockscreen
                channel.LockscreenVisibility = NotificationVisibility.Private;

                //create the channel
                NotificationManager manager = Android.App.Application.Context.GetSystemService(Context.NotificationService) as NotificationManager;
                manager.CreateNotificationChannel(channel);

                //set an action to tske the user to the active activity when user clicks on notification
                Intent intent = new Intent(Android.App.Application.Context, typeof(MainActivity));
                intent.SetAction(Intent.ActionMain);
                intent.AddCategory(Intent.CategoryLauncher);

                //create pendingIntent
                PendingIntent pendingIntent = PendingIntent.GetActivity(Android.App.Application.Context, 3, intent, 0);

                //build the notification
                var Notification = new NotificationCompat.Builder(Android.App.Application.Context, MyConstants.CHANNEL_ID);
                Notification.SetContentTitle("This is the updated Notification")
                   .SetContentText("Testing updated Notification right now!")
                    .SetSmallIcon(Resource.Drawable.ic_mr_button_grey)
                    .SetWhen(Java.Lang.JavaSystem.CurrentTimeMillis())
                    .SetSound(RingtoneManager.GetDefaultUri(RingtoneType.Ringtone))
                    .SetContentIntent(pendingIntent)
                    .SetLargeIcon(BitmapFactory.DecodeResource(Android.App.Application.Context.Resources, Resource.Drawable.user));
                //create big text style notification in the expanded. This class does not support method chaining
                //you have to explitcitely use the NotificationBuilder object to call the BigTextStyle class
                NotificationCompat.BigTextStyle bigtext = new NotificationCompat.BigTextStyle();
                string gibberish = "This is the new big text style notification";
                gibberish += "/and we are just testing it";
                bigtext.BigText(gibberish);
                bigtext.SetSummaryText("This is just a summary");
                //push BigText into builder for the notification
                Notification.SetStyle(bigtext);
                //used to create image notification
                NotificationCompat.BigPictureStyle picturestyle = new NotificationCompat.BigPictureStyle();
                picturestyle.BigPicture(BitmapFactory.DecodeResource(Android.App.Application.Context.Resources, Resource.Drawable.bsplash));
                Notification.SetStyle(picturestyle);
                //Notification.SetPriority(NotificationPriority.High);

                //.SetLargeIcon(BitmapFactory.DecodeResource(GetDrawable(Resource.Drawable.badge), Resource.Drawable.badge);
                //.build the notification
                Notification notification = Notification.Build();
                //issue the Notification
                NotificationManager manager2 = NotificationManager.FromContext(Android.App.Application.Context);
                manager2.Notify(MyConstants.NOTIFICATION_ID, notification);
            }

            else
            {
                //set an action to tske the user to the active activity when user clicks on notification
                Intent intent = new Intent(Android.App.Application.Context, typeof(MainActivity));
                intent.AddCategory(Intent.CategoryLauncher);
                intent.SetAction(Intent.ActionMain);

                //create pendingIntent
                PendingIntent pendingIntent = PendingIntent.GetActivity(Android.App.Application.Context, 4, intent, 0);

                var notification = new NotificationCompat.Builder(Android.App.Application.Context, MyConstants.CHANNEL_ID_2);
                notification.SetContentTitle("This is the other updated notification")
                    .SetContentText("This is the body of the other updated notification")
                    .SetSmallIcon(Resource.Drawable.ic_audiotrack_dark)
                    .SetWhen(Java.Lang.JavaSystem.CurrentTimeMillis())
                    .SetTimeoutAfter(6000)
                    .SetContentIntent(pendingIntent)
                    .SetLargeIcon(BitmapFactory.DecodeResource(Android.App.Application.Context.Resources, Resource.Drawable.user));
                //create big text style notification in the expanded. This class does not support method chaining
                //you have to explitcitly use the NotificationBuilder object to call the BigTextStyle class
                NotificationCompat.BigTextStyle bigtext = new NotificationCompat.BigTextStyle();
                string gibberish = "This is the new big text style notification";
                gibberish += "/and we are just testing it";
                bigtext.BigText(gibberish);
                bigtext.SetSummaryText("This is just a summary");
                //push BigText into builder for the notification
                notification.SetStyle(bigtext);
                //used to create image notification
                NotificationCompat.BigPictureStyle picturestyle = new NotificationCompat.BigPictureStyle();
                picturestyle.BigPicture(BitmapFactory.DecodeResource(Android.App.Application.Context.Resources, Resource.Drawable.bsplash));
                notification.SetStyle(picturestyle);

                Notification Notiffy = notification.Build();
                NotificationManager manager3 = NotificationManager.FromContext(Android.App.Application.Context);
                manager3.Notify(MyConstants.NOTIFICATION_ID_2, Notiffy);
            }

            
        }
    }

      


    }
