using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Matcha.BackgroundService;

namespace SampleBackground.Services
{
    public class PeriodicCNNRssFeed : BaseRssFeed
    {
        public PeriodicCNNRssFeed(int minutes) 
            : base(minutes, "http://rss.cnn.com/rss/cnn_topstories.rss")
        {
        }
    }
}
