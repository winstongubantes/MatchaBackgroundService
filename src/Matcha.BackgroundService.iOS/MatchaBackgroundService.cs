using System;
using UIKit;

namespace Matcha.BackgroundService.iOS
{
    public class MatchaBackgroundService
    {
        private static nint _taskId;
        private static MatchaBackgroundService _instance;
        private static bool _isRunning;

        static MatchaBackgroundService()
        {
        }

        private MatchaBackgroundService()
        {
        }

        /// <summary>
        /// Single Instance of MatchaBackgroundService
        /// </summary>
        public static MatchaBackgroundService Instance { get; } =
            _instance ?? (_instance = new MatchaBackgroundService());

        /// <summary>
        /// Start the execution of background service
        /// </summary>
        public void Start()
        {
            if(_isRunning) return;

            //We only have 3 minutes in the background service as per iOS 9
            _taskId = UIApplication.SharedApplication.BeginBackgroundTask(nameof(StartLongRunningTask), Stop);
            BackgroundAggregatorService.Instance.Start();

            _isRunning = true;
        }

        /// <summary>
        /// Stop the execution of background service
        /// </summary>
        public void Stop()
        {
            _isRunning = false;
            BackgroundAggregatorService.Instance.Stop();
            UIApplication.SharedApplication.EndBackgroundTask(_taskId);
        }
    }
}