using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Matcha.BackgroundService;
using SampleBackground.Models;
using SampleBackground.Services;
using SampleBackground.ViewModels;
using Xamarin.Forms;

namespace SampleBackground.Views
{
	public partial class MainPage : ContentPage
	{
		public MainPage ()
		{
			InitializeComponent ();
        }

	    private void ListView_OnItemTapped(object sender, ItemTappedEventArgs e)
	    {
            if(e.Item == null) return;
	        NewsFeedList.SelectedItem = null;

            var selectedItem = (RssData)e.Item;
	        var viewModel = (MainPageViewModel) BindingContext;
	        viewModel.NavigateLandingPage(selectedItem);
        }

	    protected override void OnAppearing()
	    {
	        base.OnAppearing();
	        var viewModel = (MainPageViewModel)BindingContext;
	        viewModel.RefreshNewsFeedCommand.Execute(null);
        }
	}
}