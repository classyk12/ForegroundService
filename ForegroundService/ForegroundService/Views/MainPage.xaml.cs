using ForegroundService.Models;
using ForegroundService.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ForegroundService
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            HandleRecievedMessage();
        }

        private void StartBtn_Clicked(object sender, EventArgs e)
        {
            //Navigation.PushAsync(new NotificationPage()); 
            var message = new StartForegroundService();
            MessagingCenter.Send(message, "StartRunningMessage");
            StartBtn.IsEnabled = false;
            ////HandleRecievedMessage();
        }

        private void HandleRecievedMessage()
        {
            MessagingCenter.Subscribe<DisplayNumbers>(this, "GetNumbers", x =>
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    NumberLbl.Text = x.Numbers;
                }); 
                
            });

            MessagingCenter.Subscribe<CancelledMessage,string>(this, "Cancelled Message", (x,source) =>
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    NumberLbl.Text = source;
                });

            });








        }

        private void StopBtn_Clicked(object sender, EventArgs e)
        {
            var message = new StopForegroundService();
            MessagingCenter.Send(message, "Stop Running Message");
            StartBtn.IsEnabled = true;
         
           
            //HandleRecievedMessage();
        }
    }

   
}
