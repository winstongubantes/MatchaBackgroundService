# Matcha Background Service Plugin for Xamarin.Forms

A plugin library to simplify Backgrounding in Xamarin.Forms. 
 

 ## Get Started
 
Ever wonder how facebook and twitter process there background to fetch a new content? It is not hard but you have to deal with each platform functionality, It is not generic and simple. 

And so the Matcha background plugin was born, it was made to get you started fast with backgrounding without having to deal with platform specific functions.
 
 ## Setup
 
* NuGet: [Matcha.Sync.Mobile](http://www.nuget.org/packages/Matcha.Sync.Mobile) [![NuGet](https://img.shields.io/nuget/v/Matcha.Sync.Mobile.svg?label=NuGet)](https://www.nuget.org/packages/Matcha.Sync.Mobile/)
* `PM> Install-Package Matcha.Sync.Mobile`
* Install into ALL of your projects, include client projects.
 
 ## For Android
You call the "Init" method before all libraries initialization in MainActivity class.

```csharp
public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
 {
     protected override void OnCreate(Bundle bundle)
     {
	     BackgroundAggregator.Init(this);
	     
	     base.OnCreate(bundle);
         ....// Code for init was here
     }
 }
 ```
 
## For iOS
 
You call the "Init" method before all libraries initialization in FinishedLaunching method in FormsApplicationDelegate class.
 
 ```csharp
 
public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
 {
     public override bool FinishedLaunching(UIApplication app, NSDictionary options)
     {
         BackgroundAggregator.Init(this);
         
           ....// Code for init was here
         return base.FinishedLaunching(app, options);
     }
 }
 
 ```

## Create Periodic Task
 
You will have to inherit IPeriodicTask interface in which you will supply and implement the interval and StartJob, Periodic Task will be execute every interval once it is registered.
 
 ```csharp
public class PeriodicWebCall : IPeriodicTask
 {
     public PeriodicWebCallTest(int seconds)
     {
         Interval = TimeSpan.FromSeconds(seconds);
     }

     public TimeSpan Interval { get; set; }
     
     public Task StartJob()
     {
	     // YOUR CODE HERE
         // THIS CODE WILL BE EXECUTE EVERY INTERVAL
     }
 }
 ```

## Register Periodic Task
 
After you have implemented the Periodic Task you will need to register it to Background Aggregator Service,  We define it on OnStart() method under App.cs.
 
 ```csharp
 protected override void OnStart()
{
	//Register Periodic Tasks
    BackgroundAggregatorService.AddSchedule(() => new PeriodicWebCall(3));
    BackgroundAggregatorService.AddSchedule(() => new PeriodicCall2(4));

	//Start the background service
	BackgroundAggregatorService.StartBackgroundService();
}
 ```

## Quirks and Limitation
 
Keep in mind that the plugin was not design to communicate with UI thread, one way of dealing the transfer of information is through storage (e.g. Sqlite or Settings plugin). Our sample project is using Monkey-Cache storage.

Starting with Android Oreo it has already introduced the background execution limits similar to iOS background time limits assuming the app is in background mode or app is closed or minimized, as discuss on this [article](https://blog.xamarin.com/replacing-services-jobs-android-oreo-8-0/). 

For more info about Backgrounding in Android in iOS please check the link [HERE](https://docs.microsoft.com/en-us/xamarin/android/app-fundamentals/services/). 

## That's it
 
You can now run your app that runs a Periodic Task every interval in the Background Service.  We have provided a few good samples to for you to dig in.

## See it in Action
 


## Platform Supported

|Platform|Version|
| ------------------- | :-----------: |
|Xamarin.iOS|iOS 7+|
|Xamarin.Android|API 15+|
|.NET Standard|2.0+|