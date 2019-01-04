

using Windows.ApplicationModel.Background;

namespace Matcha.BackgroundService.UWP
{
    public class MatchaBackgrounService : IBackgroundTask
    {
        public void Run(IBackgroundTaskInstance taskInstance)
        {
            BackgroundAggregatorService.Instance.Start();
        }
    }
}
