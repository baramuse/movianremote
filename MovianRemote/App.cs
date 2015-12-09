using System;
using Xamarin.Forms;
using Autofac;
using MovianRemote.Core.Pages;
using System.Diagnostics;
using MovianRemote.Core.Interfaces;

namespace MovianRemote.Core
{
//	public class App
//	{
//		public static IContainer Container { get; set; }
//
//
//		public static void Init(AppSetup setup)
//		{
//			Container = setup.CreateContainer ();
//		}
//
//		public static Page GetMainPage ()
//		{	
//			return new ConnectPage ();
//		}
//	}

	public class App : Xamarin.Forms.Application, IAppNavigation
	{
		#region IAppNavigation implementation

		public void GoToHomePage ()
		{
			MainPage = new HomeTabbedPage ();
		}

		public void GoToConnectPage ()
		{
			MainPage = new ConnectPage ();
		}

		#endregion

		public static IContainer Container { get; set; }

		public App (AppSetup setup)
		{
			var builder = setup.CreateContainerBuilder ();
			builder.RegisterInstance<IAppNavigation> (this);
			Container = builder.Build ();
			MainPage = new ConnectPage ();
		}

		protected override void OnStart()
		{
			// Handle when your app starts
			Debug.WriteLine ("OnStart");
		}

		protected override void OnSleep()
		{
			// Handle when your app sleeps
			Debug.WriteLine ("OnSleep");
		}

		protected override void OnResume()
		{
			// Handle when your app resumes
			Debug.WriteLine ("OnResume");
		}
	}
}

