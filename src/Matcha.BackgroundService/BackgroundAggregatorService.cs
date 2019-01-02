using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Matcha.BackgroundService
{
    public partial  class BackgroundAggregatorService
    {
        internal static readonly CompositeDisposable EventSubscriptions = new CompositeDisposable();

        private static BackgroundAggregatorService _instance;
        private static Dictionary<string, IPeriodicTask> _schedules = new Dictionary<string, IPeriodicTask>();

        static BackgroundAggregatorService()
        {
        }

        private BackgroundAggregatorService()
        {
        }

        public static void Add<T>(Func<T> schedule) where T: IPeriodicTask
        {
            var typeName = schedule.GetType().GetGenericArguments()[0]?.Name;

            if (typeName != null && !_schedules.ContainsKey(typeName))
                _schedules.Add(typeName, schedule());
        }

        public static void StartBackgroundService()
        {
            var message = new StartLongRunningTask();
            MessagingCenter.Send(message, nameof(StartLongRunningTask));
        }

        public static void StopBackgroundService()
        {
            var message = new StopLongRunningTask();
            MessagingCenter.Send(message, nameof(StopLongRunningTask));
        }

        public static BackgroundAggregatorService Instance { get; } = _instance ?? (_instance = new BackgroundAggregatorService());

        public void Start()
        {
            foreach (var schedule in _schedules)
            {
                var observable = SyncRepeatObservable(schedule.Value);
                EventSubscriptions.Add(observable);
            }
        }

        public void Stop()
        {
            EventSubscriptions.Clear();
        }

        public void Clear()
        {
            EventSubscriptions.Clear();
            _schedules.Clear();
        }

        private static IDisposable SyncRepeatObservable(IPeriodicTask schedule)
        {
            return Observable
                .FromAsync(schedule.StartJob)
                .Delay(schedule.Interval)
                .Repeat()
                .TakeWhile(e=> e)
                .Subscribe();
        }
    }
}
