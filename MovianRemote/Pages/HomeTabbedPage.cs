using System;

using Xamarin.Forms;
using MovianRemote.Core.Interfaces;
using Autofac;
using MovianRemote.Core.Common;

namespace MovianRemote.Core.Pages
{
	public class HomeTabbedPage : TabbedPage
	{
		IMovianWebSocketService _ws;
		MovianPropertySubscription _playStatusSubscription;

		public HomeTabbedPage ()
		{
			_ws = App.Container.Resolve<IMovianWebSocketService> ();

			_playStatusSubscription = _ws.Subscribe ("media.current.playstatus");
			_playStatusSubscription.ValueChanged+= _playStatusSubscription_ValueChanged;
			Children.Add (new RemotePage { Title = "Remote" });
			Children.Add (new MediaControllerPage { Title = "Media" });
		}

		void _playStatusSubscription_ValueChanged (object sender, string e)
		{
			Device.BeginInvokeOnMainThread (() => {
				if (string.IsNullOrEmpty (e)) {
					this.CurrentPage = Children [0];
				} else {
					this.CurrentPage = Children [1];
				}
			});
		}
	}
}


