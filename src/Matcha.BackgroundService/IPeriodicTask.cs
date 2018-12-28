using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Matcha.BackgroundService
{
    public interface IPeriodicTask
    {
        TimeSpan Interval { get; set; }
        Task<bool> StartJob();
    }
}
