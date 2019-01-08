

using Windows.ApplicationModel.Background;

namespace Matcha.BackgroundService.UWP
{
    public sealed class MatchaBackgrounService : IBackgroundTask
    {
        public void Run(IBackgroundTaskInstance taskInstance)
        {
            BackgroundAggregatorService.Instance.Start();
        }

            
    }
}
