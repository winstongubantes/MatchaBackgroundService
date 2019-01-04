using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
                async(message) =>
                {
                    var access = await BackgroundExecutionManager.RequestAccessAsync();

                    switch (access)
                    {
                        case Unspecified:
                            return;
                        case AllowedMayUseActiveRealTimeConnectivity:
                            return;
                        case AllowedWithAlwaysOnRealTimeConnectivity:
                            return;
                        case Denied:
                            return;
                    }

                    var task = new BackgroundTaskBuilder
                    {
                        Name = "My Task",
                        TaskEntryPoint = typeof(Matcha.BackgroundService.UWP.MatchaBackgrounService).ToString()
                    };

                    var trigger = new ApplicationTrigger();
                    task.SetTrigger(trigger);

                    //var condition = new SystemCondition(SystemConditionType.InternetAvailable);
                    task.Register();

                    await trigger.RequestAsync();
                });

            MessagingCenter.Subscribe<StopLongRunningTask>(page, nameof(StopLongRunningTask),
                message =>
                {

                });
        }
    }
}
