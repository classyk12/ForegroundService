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
            GetSubscription();
            //HandleRecievedMessages();
        }

        private void GetSubscription()
        {
            MessagingCenter.Subscribe<StartForegroundService>(this, "StartService", x =>
            {
                Intent intent = new Intent(this, typeof(MusicService));
                intent.SetAction(MPConstants.ACTION_START);
                if (Android.OS.Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.O)
                    {
                        StartForegroundService(intent);
                    }
                    else
                    {
                        StartService(intent);
                  }

            });

            MessagingCenter.Subscribe<StopForegroundService>(this, "StopService", y =>
            {
                Intent intent = new Intent(this, typeof(MusicService));
                intent.SetAction(MPConstants.ACTION_STOP);
                StopService(intent);

            });

        }

        //private void HandleRecievedMessages()
        //{

        //        MessagingCenter.Subscribe<StartForegroundService>(this, "StartRunningMessage", x =>
        //        {
        //            var intent = new Intent(this, typeof(MyForegroundService));
        //            if (Android.OS.Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.O)
        //            {
        //                StartForegroundService(intent);
        //            }
        //            else
        //            {
        //                StartService(intent);
        //            }

        //        });

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
        //    }
    }
    }
