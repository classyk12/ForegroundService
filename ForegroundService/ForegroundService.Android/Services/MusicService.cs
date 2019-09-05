using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using ForegroundService.AllConstants;

namespace ForegroundService.Droid.Services
{
    [Service]
    public class MusicService : Service
    {
        public override IBinder OnBind(Intent intent)
        {
            //this is null because this is not a bound service. i.e it does not bind to activity or some activity
            return null;
        }

        [return: GeneratedEnum]
        public override StartCommandResult OnStartCommand(Intent intent, [GeneratedEnum] StartCommandFlags flags, int startId)
        {
            if(intent != null)
            {
                string action = intent.Action;

                if(action == MPConstants.ACTION_START)
                {
                    startMyService();
                    Toast.MakeText(this, "Service started!", ToastLength.Long).Show();
                }

                else if( action == MPConstants.ACTION_STOP)
                {
                    StopMyService();
                    Toast.MakeText(this, "Service stopped", ToastLength.Long).Show();
                }

                else if( action == MPConstants.ACTION_PLAY)
                {
                    Toast.MakeText(this, "You pressed Play", ToastLength.Long).Show();
                }

                else if( action == MPConstants.ACTION_PAUSE)
                {
                    Toast.MakeText(this, "You pressed pause", ToastLength.Long).Show();
                }
            }
            //return StartCommandResult.Sticky;
            return base.OnStartCommand(intent, flags, startId);
        }

        private void StopMyService()
        {
            Log.Debug(MPConstants.TAG_FOREGROUND_SERVICE, "Service stopped");
            //removes the notification from the status bar and tell android to destroy the service when it likes
            StopForeground(true);

            //stops the service again. just making sure its dead lol
            StopSelf();
        }

        private void startMyService()
        {
            //create the default activity to be shown
            Intent intent = new Intent();
            PendingIntent pending = PendingIntent.GetActivity(this, 0, intent, 0);

           
          if(Android.OS.Build.VERSION.SdkInt <= BuildVersionCodes.NMr1)
            {
                var notification = new Notification.Builder(this, Constants.CHANNEL_ID)
                 .SetContentTitle(Constants.CONTENT_TITLE)
                 .SetContentText(Constants.CONTENT_TEXT)
                 .SetSmallIcon(Resource.Drawable.bsplash)
                 .SetOngoing(true)
                 .Build();

                StartForeground(300, notification);

            }

            else
            {
                //create a notification channel. this is a must if your service is going to run on 
                //Android 8.0(Oreo) and above
                NotificationChannel channel = new NotificationChannel(MPConstants.CHANNEL_ID1, MPConstants.CHANNEL_NAME, NotificationImportance.Max);
                NotificationManager manager = GetSystemService(NotificationService) as NotificationManager;

                if(manager != null)
                {
                    manager.CreateNotificationChannel(channel);
                }

                //create the notification
                var builder = new Notification.Builder(this, MPConstants.CHANNEL_ID1);

                //make notification show big text
                Notification.BigTextStyle BigStyle = new Notification.BigTextStyle();
                BigStyle.SetBigContentTitle("This service is running in the foreground");
                BigStyle.BigText("This is a music like foeground service that was built using some tutorial and some little tweaks");
                builder.SetStyle(BigStyle);

                //build notification
                builder.SetWhen(DateTime.Now.Millisecond);
                builder.SetSmallIcon(Resource.Drawable.badge);
                //////set large icon of type Bitmap
                //Bitmap LargeIconBitMap = BitmapFactory.DecodeResource(Resources.get, Resource.Drawable.bsplash);
               
                //used to set full screen intent
                builder.SetFullScreenIntent(pending, true);

                //Add play button to Notification
                Intent PlayIntent = new Intent(this, typeof(MusicService));
                intent.SetAction(MPConstants.ACTION_PLAY);
                PendingIntent PendingPlayIntent = PendingIntent.GetService(this, 0, PlayIntent, 0);
                Notification.Action playaction = new Notification.Action(Resource.Drawable.ic_media_play_light, "Play", PendingPlayIntent);
                builder.AddAction(playaction);

                //add Pause button to Notification
                Intent PauseIntent = new Intent(this, typeof(MusicService));
                intent.SetAction(MPConstants.ACTION_PAUSE);
                PendingIntent PausePendingIntent = PendingIntent.GetService(this, 0, PauseIntent, 0);
                Notification.Action PauseAction = new Notification.Action(Resource.Drawable.ic_media_pause_light, "Pause", PausePendingIntent);
                builder.AddAction(PauseAction);

                //build notification
               Notification notification =  builder.Build();

                StartForeground(500, notification);
            }

        }

        public override void OnCreate()
        {
            Log.Debug(MPConstants.TAG_FOREGROUND_SERVICE,"Service started");
         
            base.OnCreate();
        }
    }
}