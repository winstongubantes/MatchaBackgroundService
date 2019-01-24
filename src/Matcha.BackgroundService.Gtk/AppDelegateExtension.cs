using System;
using System.Collections.Generic;
using System.Text;
using Matcha.BackgroundService;
using Xamarin.Forms;

namespace Matcha.BackgroundService.Gtk
{
    /// <summary>
    /// BackgroundAggregator
    /// </summary>
    public class BackgroundAggregator
    {
        private static readonly BackgroundGtkObject ObjBackgroundGtkObject = new BackgroundGtkObject();

        public static void Init()
        {
            MessagingCenter.Subscribe<StartLongRunningTask>(ObjBackgroundGtkObject, nameof(StartLongRunningTask),
                message => { MatchaBackgroundSharedService.Instance.Start(); });

            MessagingCenter.Subscribe<StopLongRunningTask>(ObjBackgroundGtkObject, nameof(StopLongRunningTask),
                message => { MatchaBackgroundSharedService.Instance.Stop(); });
        }
    }
}
