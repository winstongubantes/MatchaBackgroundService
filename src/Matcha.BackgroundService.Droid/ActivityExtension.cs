using Android.Content;
using Xamarin.Forms;

namespace Matcha.BackgroundService.Droid
{
    public class BackgroundAggregator
    {
        public static void Init(ContextWrapper context)
        {
            MessagingCenter.Subscribe<StartLongRunningTask>(context, nameof(StartLongRunningTask), message =>
            {
                var intent = new Intent(context, typeof(MatchaBackgroundService));
                context.StartService(intent);
            });

            MessagingCenter.Subscribe<StopLongRunningTask>(context, nameof(StopLongRunningTask), message =>
            {
                var intent = new Intent(context, typeof(MatchaBackgroundService));
                context.StopService(intent);
            });
        }
    }
}