using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using ForegroundService.Models;
using Xamarin.Forms;
using ForegroundService.AllConstants;
using ForegroundService.Droid;
using Android.Util;
using System.Threading;

using ForegroundService.Functions;

namespace ForegroundService.Droid.Services
{
    [Service]
    public class MyForegroundService : Service
    {
        CancellationTokenSource cts = new CancellationTokenSource();
        public override IBinder OnBind(Intent intent)
        {
            return null;
        }

        public override void OnCreate()
        {


            Task.Run(() =>
            {
                //invoke shared code
                try
                {
                    var num = new ShowNumbers();
                    num.DisplayAllNumbers(cts.Token).Wait();

                }
                catch (System.OperationCanceledException)
                {

                }
                finally
                {
                    if (cts.IsCancellationRequested)
                    {
                        var cancel = new CancelledMessage();
                        Device.BeginInvokeOnMainThread(() =>
                        {
                            MessagingCenter.Send(cancel, "Cancelled Message", "Service cancelled");
                        });
                    }
                }


            }, cts.Token);

            if (Build.VERSION.SdkInt <= BuildVersionCodes.NMr1)
            {
                //used to build the notification when the service starts
                var notification = new Notification.Builder(this, Constants.CHANNEL_ID)
                    .SetContentTitle(Constants.CONTENT_TITLE)
                    .SetContentText(Constants.CONTENT_TEXT)
                    .SetSmallIcon(Resource.Drawable.bsplash)
                    .SetOngoing(true)
                    .Build();


                //used to Register the foreground service
                StartForeground(Constants.NOTIFICATION_ID, notification);
            }

            else if (Build.VERSION.SdkInt >= BuildVersionCodes.O)
            {
                //build a notification channel
                NotificationChannel channel = new NotificationChannel(Constants.CHANNEL_ID2, Constants.CHANNEL_NAME, NotificationImportance.High);
                NotificationManager manager = GetSystemService(NotificationService) as NotificationManager;

                if (manager != null)
                {
                    manager.CreateNotificationChannel(channel);
                }

                //create the notifiction builder
                var notification = new Notification.Builder(this, Constants.CHANNEL_ID2);
                Notification notify = notification.SetContentTitle("This is a new foreground service")
                   .SetContentText("Service is running rigt now")
                   .SetSmallIcon(Resource.Drawable.smallicon)
                   .SetCategory(Notification.CategoryService)
                   //.SetBadgeIconType(NotificationBadgeIconType.Small)
                   .SetOngoing(true)
                   .SetContentIntent(BuildIntentToShowMainActivity())
                   .Build();

                StartForeground(300, notify);
            }


            base.OnCreate();
        }

        [return: GeneratedEnum]
        public override StartCommandResult OnStartCommand(Intent intent, [GeneratedEnum] StartCommandFlags flags, int startId)
        {

            return StartCommandResult.Sticky;
        }



        //used to show the main activity when user clicks on the notification
        private PendingIntent BuildIntentToShowMainActivity()
        {
            var notificationIntent = new Intent(this, typeof(MainActivity));
            notificationIntent.SetAction(Constants.ACTION_MAIN_ACTIVITY);
            notificationIntent.SetFlags(ActivityFlags.SingleTop | ActivityFlags.ClearTask);
            notificationIntent.PutExtra(Constants.SERVICE_STARTED_KEY, true);

            var pendingIntent = PendingIntent.GetActivity(this, 0, notificationIntent, PendingIntentFlags.UpdateCurrent);
            return pendingIntent;
        }

        public override void OnDestroy()
        {
            if (cts != null)
            {
                cts.Token.ThrowIfCancellationRequested();
                cts.Cancel();
                base.OnDestroy();
            }
        }

        //private void StopForeground()
        //{
        //    StopForeground();
        //    StopSelf();
        //}
    }
}