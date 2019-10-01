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
using ForegroundService.AllConstants;

namespace ForegroundService.Droid.Services
{
    public class BootCompletedReciever : BroadcastReceiver
    {
        public override void OnReceive(Context context, Intent intent)
        {
            if (intent.Action == MPConstants.BOOT_COMPLETED)
            {
                Intent restartIntent = new Intent(context, typeof(MusicService));
                    context.StartService(restartIntent);
            }

        }
    }
}