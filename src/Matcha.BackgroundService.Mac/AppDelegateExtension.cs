using System;
using System.Collections.Generic;
using System.Text;
using Matcha.BackgroundService;
using Matcha.BackgroundService.Mac;
using Xamarin.Forms;
using Xamarin.Forms.Platform.MacOS;

namespace Matcha.BackgroundService.Mac
{
    /// <summary>
    /// BackgroundAggregator
    /// </summary>
    public class BackgroundAggregator
    {
        /// <summary>
        /// Initializes the Background For Mac
        /// </summary>
        /// <param name="appDelegate"></param>
        public static void Init(FormsApplicationDelegate appDelegate)
        {
            MessagingCenter.Subscribe<StartLongRunningTask>(appDelegate, nameof(StartLongRunningTask),
                message => { MatchaBackgroundSharedService.Instance.Start(); });

            MessagingCenter.Subscribe<StopLongRunningTask>(appDelegate, nameof(StopLongRunningTask),
                message => { MatchaBackgroundSharedService.Instance.Stop(); });
        }
    }
}
