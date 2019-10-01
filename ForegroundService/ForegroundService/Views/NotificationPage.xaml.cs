using ForegroundService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ForegroundService.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class NotificationPage : ContentPage
	{
		public NotificationPage ()
		{
			InitializeComponent ();
            StartBtn.Text = "Show Notification";
            StartBtn.BackgroundColor = Color.Green;
            UpdateBtn.IsVisible = false;
		}

        public void SendMessage()
        {
            var send = new StartForegroundService();
            MessagingCenter.Send(send, "Show Notification");
            StartBtn.Text = "Remove Notification";
            StartBtn.BackgroundColor = Color.Red;
            UpdateBtn.IsVisible = true;
        }

        public void StopMessage()
        {
            var stop = new StopForegroundService();
            MessagingCenter.Send(stop, "Remove Notification");
            StartBtn.Text = "Show Notification";
            StartBtn.BackgroundColor = Color.Green;
            UpdateBtn.IsVisible = false;
        }

        private void StartBtn_Clicked(object sender, EventArgs e)
        {
            if(StartBtn.Text == "Show Notification")
            {
                SendMessage();
            }

            else
            {
                StopMessage();
            }
        }

        private void UpdateBtn_Clicked(object sender, EventArgs e)
        {
            var update = new CancelledMessage();
            MessagingCenter.Send(update, "Update Notification");

        }
    }
}