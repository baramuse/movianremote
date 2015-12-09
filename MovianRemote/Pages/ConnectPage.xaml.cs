using System;
using System.Collections.Generic;

using Xamarin.Forms;
using MovianRemote.Core.Common;
using Autofac;
using MovianRemote.Core.ViewModels;
using System.Threading.Tasks;
using MovianRemote.Core.Interfaces;

namespace MovianRemote.Core.Pages
{
	public partial class ConnectPage : ContentPage
	{
		public ConnectPage ()
		{
			InitializeComponent ();



			BindingContext = App.Container.Resolve<ConnectViewModel> ();

			MessagingCenter.Subscribe<Application, DisplayAlertMessage> (this, "ShowAlert", (sender, message) => {
				const string title = "Message";
				const string cancel = "Ok";
				Device.BeginInvokeOnMainThread(() => this.DisplayAlert (title, message.Message, cancel));

				message.OnCompleted?.Invoke (true);

			}, Application.Current);

			MessagingCenter.Subscribe<Application, NavigateMessage> (this, "Navigate", (sender, message) => {
				
				Device.BeginInvokeOnMainThread(() => App.Container.Resolve<IAppNavigation>().GoToHomePage());
			}, Application.Current);
		}

	}
}

