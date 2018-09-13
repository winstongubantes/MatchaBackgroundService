using Matcha.BackgroundService;
using MonkeyCache.SQLite;
using Prism;
using Prism.Ioc;
using SampleBackground.ViewModels;
using SampleBackground.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Prism.DryIoc;
using Prism.Navigation;
using SampleBackground.Services;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace SampleBackground
{
    public partial class App : PrismApplication
    {
        public App() : this(null) { }

        public App(IPlatformInitializer initializer) : base(initializer) { }

        protected override async void OnInitialized()
        {
            InitializeComponent();

            //Starting Navigation
            await NavigationService.NavigateAsync($"{nameof(NavigationPage)}/{nameof(MainPage)}");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            //Set application id  for our storage
            Barrel.ApplicationId = "NewsfeedAppId";
            containerRegistry.RegisterInstance(Barrel.Current);

            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MainPage>();
            containerRegistry.RegisterForNavigation<NewLandingPage>();
        }

        //Called when the application starts.
        protected override void OnStart()
        {
            StartBackgroundService();
        }

        // Called each time the application goes to the background.
        protected override void OnSleep()
        {
            //BackgroundAggregatorService.StopBackgroundService();
        }

        protected override void OnResume()
        {
            if (Device.RuntimePlatform == Device.iOS) StartBackgroundService();
        }

        private static void StartBackgroundService()
        {
            //Rss gets updated every 3 minutes
            BackgroundAggregatorService.Add(() => new PeriodicBBCNewsRssFeed(3));
            //BackgroundAggregatorService.Add(() => new PeriodicCNNRssFeed(4));
            BackgroundAggregatorService.Add(() => new PeriodicWashingtonPostRssFeed(5));

            //you now running the periodic task in the background
            BackgroundAggregatorService.StartBackgroundService();
        }
    }
}
