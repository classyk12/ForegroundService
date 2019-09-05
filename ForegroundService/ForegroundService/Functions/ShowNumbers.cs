using ForegroundService.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ForegroundService.Functions
{
    public class ShowNumbers
    {


        public async Task DisplayAllNumbers(CancellationToken token)
        {
         
            await Task.Run(async () =>
            {
               
                    for (long i = 0; i <= long.MaxValue; i++)
                    {

                    //check if cancellation has been requested while the loop runs
                    token.ThrowIfCancellationRequested();

                    //delays the task for 300ms
                    await Task.Delay(300);

                        var numbers = new DisplayNumbers()
                        {
                            Numbers = i.ToString()
                        };

                        Device.BeginInvokeOnMainThread(() =>
                        {
                        //used to publish the message and ready for subscription
                        MessagingCenter.Send(numbers, "GetNumbers");
                        });
                    }
                          
        },token);
        }
    }
}
