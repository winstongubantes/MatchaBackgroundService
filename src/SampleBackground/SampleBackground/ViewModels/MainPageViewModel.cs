using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using MonkeyCache;
using Prism.Services;
using SampleBackground.Models;
using SampleBackground.Views;

namespace SampleBackground.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IBarrel _barrel;
        private readonly IDeviceService _deviceService;
        private ObservableCollection<RssData> _newsFeed;
        private string _topNewsTitle;
        private string _topNewsLink;
        private string _topNewsGuid;
        private string _topNewsAuthor;
        private string _topNewsThumbnail;
        private string _topNewsDescription;

        public MainPageViewModel(
            INavigationService navigationService, 
            IBarrel barrel, 
            IDeviceService deviceService) 
            : base (navigationService)
        {
            _navigationService = navigationService;
            _barrel = barrel;
            _deviceService = deviceService;
            Title = "News Feed";
        }

        private ICommand _refreshNewsFeedCommand;

        public ICommand RefreshNewsFeedCommand =>
            _refreshNewsFeedCommand ?? (_refreshNewsFeedCommand = new DelegateCommand(
                () =>
                {
                    IsBusy = true;

                    Task.Factory.StartNew(async () =>
                    {
                        Refresh:

                        if (!_barrel.Exists("NewsFeeds"))
                        {
                            await Task.Delay(1000);
                            goto Refresh;
                        }

                        var newsFeed = _barrel.Get<List<RssData>>("NewsFeeds");

                        _deviceService.BeginInvokeOnMainThread(() =>
                        {
                            if (newsFeed != null)
                            {
                                NewsFeed = new ObservableCollection<RssData>(newsFeed);

                                if (newsFeed.Any())
                                {
                                    TopNewsTitle = newsFeed[0].Title;
                                    TopNewsLink = newsFeed[0].Link;
                                    TopNewsThumbnail = newsFeed[0].Thumbnail;
                                    TopNewsAuthor = newsFeed[0].Author;
                                    TopNewsDescription = newsFeed[0].Description;
                                }
                            }

                            IsBusy = false;
                        });
                    });
                }));

        public void NavigateLandingPage(RssData data)
        {
            _navigationService.NavigateAsync(nameof(NewLandingPage), new NavigationParameters
            {
                {"url", data.Link }
            });
        }

        public ObservableCollection<RssData> NewsFeed
        {
            get => _newsFeed;
            set => SetProperty(ref _newsFeed, value);
        }

        public string TopNewsTitle
        {
            get => _topNewsTitle;
            set => SetProperty(ref _topNewsTitle, value);
        }

        public string TopNewsLink
        {
            get => _topNewsLink;
            set => SetProperty(ref _topNewsLink, value);
        }

        public string TopNewsGuid
        {
            get => _topNewsGuid;
            set => SetProperty(ref _topNewsGuid, value);
        }

        public string TopNewsAuthor
        {
            get => _topNewsAuthor;
            set => SetProperty(ref _topNewsAuthor, value);
        }

        public string TopNewsThumbnail
        {
            get => _topNewsThumbnail;
            set => SetProperty(ref _topNewsThumbnail, value);
        }

        public string TopNewsDescription
        {
            get => _topNewsDescription;
            set => SetProperty(ref _topNewsDescription, value);
        }
    }
}
