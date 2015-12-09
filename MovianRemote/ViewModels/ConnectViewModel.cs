using System;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;
using MovianRemote.Core.Interfaces;
using MovianRemote.Core.Common;

namespace MovianRemote.Core.ViewModels
{
	public class ConnectViewModel : ViewModelBase
	{
		IWebSocketClient _wsClient;

		public ConnectViewModel (IWebSocketClient wsClient)
		{
			_wsClient = wsClient;
			this.ConnectCommand = new Command (onConnectCommand);
			_wsClient.Opened+= WsClient_Opened;
			_wsClient.Error+= WsClient_Error;
		}

		void WsClient_Error (object sender, MovianRemote.Core.Common.ErrorEventArgs e)
		{
			ActivityIndicatorIsRunning = false;
			var message = new DisplayAlertMessage ();

			message.Message = "Connection error";
			MessagingCenter.Send<Application, DisplayAlertMessage> (Application.Current, "ShowAlert", message);
		}

		void WsClient_Opened (object sender, EventArgs e)
		{
			ActivityIndicatorIsRunning = false;

			MessagingCenter.Send<Application, NavigateMessage> (Application.Current, "Navigate", new NavigateMessage());
		}

		private void onConnectCommand()
		{
			ActivityIndicatorIsRunning = true;
			_wsClient.OpenAsync ($"ws://{IpEntryText}:42000/showtime/stpp");
		}

		string _ipEntryText = "127.0.0.1";
		public string IpEntryText {
			get { return _ipEntryText; }
			set {
				if (value != _ipEntryText) {
					_ipEntryText = value;
					OnPropertyChanged (nameof (IpEntryText));
				}
			}
		}

		bool _activityIndicatorIsRunning = false;
		public bool ActivityIndicatorIsRunning {
			get { return _activityIndicatorIsRunning; }
			set {
				if (value != _activityIndicatorIsRunning) {
					_activityIndicatorIsRunning = value;
					OnPropertyChanged (nameof (ActivityIndicatorIsRunning));
				}
			}
		}

		public ICommand ConnectCommand { protected set; get; }
	}
}

