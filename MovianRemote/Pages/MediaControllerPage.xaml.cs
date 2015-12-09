using System;
using System.Collections.Generic;

using Xamarin.Forms;
using MovianRemote.Core;
using Autofac;
using MovianRemote.Core.ViewModels;

namespace MovianRemote
{
	public partial class MediaControllerPage : ContentPage
	{
		public MediaControllerPage ()
		{
			InitializeComponent ();

			using (var scope = App.Container.BeginLifetimeScope()) 
			{
				BindingContext = App.Container.Resolve<MediaControllerViewModel>();
			}
		}
	}
}

