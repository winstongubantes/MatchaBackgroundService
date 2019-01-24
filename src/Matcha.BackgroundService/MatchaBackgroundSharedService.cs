using System;
using System.Collections.Generic;
using System.Text;

namespace Matcha.BackgroundService
{
    public class MatchaBackgroundSharedService
    {
        private static MatchaBackgroundSharedService _instance;
        private static bool _isRunning;

        static MatchaBackgroundSharedService()
        {
        }

        private MatchaBackgroundSharedService()
        {
        }

        /// <summary>
        /// Single Instance of MatchaBackgroundService
        /// </summary>
        public static MatchaBackgroundSharedService Instance { get; } =
            _instance ?? (_instance = new MatchaBackgroundSharedService());


        /// <summary>
        /// Start the execution of background service
        /// </summary>
        public void Start()
        {
            if (_isRunning) return;
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
        }
    }
}
