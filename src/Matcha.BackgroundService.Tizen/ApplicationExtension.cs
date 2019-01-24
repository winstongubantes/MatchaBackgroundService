using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Matcha.BackgroundService;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Tizen;

namespace Matcha.BackgroundService.Tizen
{
    //FormsApplication
    public class BackgroundAggregator
    {
        public static void Init(FormsApplication appDelegate)
        {
            MessagingCenter.Subscribe<StartLongRunningTask>(appDelegate, nameof(StartLongRunningTask),
                message => { MatchaBackgroundSharedService.Instance.Start(); });

            MessagingCenter.Subscribe<StopLongRunningTask>(appDelegate, nameof(StopLongRunningTask),
                message => { MatchaBackgroundSharedService.Instance.Stop(); });
        }
    }
}
