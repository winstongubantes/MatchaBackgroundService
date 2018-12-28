using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Matcha.BackgroundService;
using Microsoft.Toolkit.Parsers.Rss;
using MonkeyCache.SQLite;
using SampleBackground.Models;

namespace SampleBackground.Services
{
    public abstract class BaseRssFeed : IPeriodicTask
    {
        private readonly IRssParserService _parserService;
        private readonly string _url;

        protected BaseRssFeed(int minutes, string url)
        {
            _url = url;
            _parserService = new RssParserService();
            Interval = TimeSpan.FromMinutes(minutes);
        }

        public TimeSpan Interval { get; set; }

        public async Task<bool> StartJob()
        {
            var existingList = Barrel.Current.Get<List<RssData>>("NewsFeeds") ?? new List<RssData>();

            var result = await _parserService.Parse(_url);

            foreach (var rssSchema in result)
            {
                var isExist = existingList.Any(e => e.Guid == rssSchema.InternalID);

                if (!isExist)
                {
                    existingList.Add(new RssData
                    {
                        Title = rssSchema.Title,
                        PubDate = rssSchema.PublishDate,
                        Link = rssSchema.FeedUrl,
                        Guid = rssSchema.InternalID,
                        Author = rssSchema.Author,
                        Thumbnail = string.IsNullOrWhiteSpace(rssSchema.ImageUrl) ? $"https://placeimg.com/80/80/nature" : rssSchema.ImageUrl,
                        Description = rssSchema.Summary
                    });
                }
            }

            existingList = existingList.OrderByDescending(e => e.PubDate).ToList();

            Barrel.Current.Add("NewsFeeds", existingList, TimeSpan.FromDays(30));

            return true; //return false when you want to stop or trigger only once
        }
    }
}
