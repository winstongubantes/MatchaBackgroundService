using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.ApplicationModel.Background;
using Xamarin.Forms;
using Xamarin.Forms.Platform.UWP;
using static Windows.ApplicationModel.Background.BackgroundAccessStatus;

namespace Matcha.BackgroundService.UWP
{
    public class WindowsPageExtension
    {
        public static void Init(WindowsPage page)
        {
            MessagingCenter.Subscribe<StartLongRunningTask>(page, nameof(StartLongRunningTask),
                (message) =>
                {

                    Task.Factory.StartNew(() =>
                    {
                        BackgroundAggregatorService.Instance.Start();
                    }, TaskCreationOptions.LongRunning);

                    #region UNUSED CODE
                    //var access = await BackgroundExecutionManager.RequestAccessAsync();

                    //Debug.WriteLine(access.ToString());

                    //switch (access)
                    //{
                    //    case Unspecified:
                    //        return;
                    //    case AllowedMayUseActiveRealTimeConnectivity:
                    //        return;
                    //    case AllowedWithAlwaysOnRealTimeConnectivity:
                    //        return;
                    //    case Denied:
                    //        return;
                    //}

                    //var task = new BackgroundTaskBuilder
                    //{
                    //    Name = "BackgroundService",
                    //    TaskEntryPoint = "Matcha.BackgroundService.UWP.MatchaBackgrounService"
                    //};

                    //var trigger = new ApplicationTrigger();
                    //task.SetTrigger(trigger);

                    ////var condition = new SystemCondition(SystemConditionType.InternetAvailable);
                    //task.Register();

                    //await trigger.RequestAsync(); 
                    #endregion
                });

            MessagingCenter.Subscribe<StopLongRunningTask>(page, nameof(StopLongRunningTask),
                message =>
                {
                    BackgroundAggregatorService.Instance.Stop();
                });
        }
    }
}
