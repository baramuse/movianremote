using System;
using System.Collections.Generic;

using Xamarin.Forms;
using ModernHttpClient;
using System.Net.Http;
using System.Diagnostics;

namespace MovianRemote
{
	public partial class RemotePage : ContentPage
	{
		HttpClient _client;

		public RemotePage ()
		{
			InitializeComponent ();
			_client = new HttpClient ();
		
		}

		void sendCommand(string command)
		{
			var endpoint = $"http://{hostEntry.Text}:42000/{command}";
			_client.GetAsync (endpoint);
		}

		void onCommandButtonClicked(object sender, EventArgs e)
		{
			var b = sender as Button;
			sendCommand ("showtime/input/action/" + b.Text.ToLower());
		}

		void onOkClicked(object sender, EventArgs e)
		{
			sendCommand ("showtime/input/action/activate");
		}
	}
}

