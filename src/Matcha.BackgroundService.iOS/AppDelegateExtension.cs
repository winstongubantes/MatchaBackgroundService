using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

namespace Matcha.BackgroundService.iOS
{
    public class BackgroundAggregator
    {
        public static void Init(FormsApplicationDelegate appDelegate)
        {
            MessagingCenter.Subscribe<StartLongRunningTask>(appDelegate, nameof(StartLongRunningTask),
                message => { MatchaBackgroundService.Instance.Start(); });

            MessagingCenter.Subscribe<StopLongRunningTask>(appDelegate, nameof(StopLongRunningTask),
                message => { MatchaBackgroundService.Instance.Stop(); });
        }
    }
}