using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ForegroundService.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ForegroundService.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MusicPlayer : ContentPage
    {
        public MusicPlayer()
        {
            InitializeComponent();
        }

        private void Start_Service(object sender, EventArgs e)
        {
            var start = new StartForegroundService();
            MessagingCenter.Send(start, "StartService");
            startBtn.IsEnabled = false;
        }

        private void Stop_Service(object sender, EventArgs e)
        {
            var stop = new StopForegroundService();
            MessagingCenter.Send(stop, "StopService");
            startBtn.IsEnabled = true;
        }
    }
}