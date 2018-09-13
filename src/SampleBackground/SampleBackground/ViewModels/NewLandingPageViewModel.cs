using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using Prism.Navigation;

namespace SampleBackground.ViewModels
{
	public class NewLandingPageViewModel : BindableBase, INavigationAware
	{
        public NewLandingPageViewModel()
        {

        }



	    private string _url;

	    public string Url
	    {
	        get => _url;
	        set => SetProperty(ref _url, value);
	    }

	    public void OnNavigatedFrom(NavigationParameters parameters)
	    {      
	    }

	    public void OnNavigatedTo(NavigationParameters parameters)
	    {
	        var url = parameters.GetValue<string>("url");
	        Url = url;
	    }

	    public void OnNavigatingTo(NavigationParameters parameters)
	    {
	    }
	}
}
