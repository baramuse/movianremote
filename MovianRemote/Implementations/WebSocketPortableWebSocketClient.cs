using System;
using MovianRemote.Core.Interfaces;
using WebSocket.Portable;
using System.Diagnostics;
using MovianRemote.Core.Common;
using System.Threading.Tasks;

namespace MovianRemote.Core.Implementations
{
	public class WebSocketPortableWebSocketClient : IWebSocketClient
	{
		#region IWebSocketClient implementation

		public event EventHandler Opened;

		public event EventHandler Closed;

		public event EventHandler<MovianRemote.Core.Common.ErrorEventArgs> Error;

		public event EventHandler<MovianRemote.Core.Common.MessageEventArgs> MessageReceived;


		public async Task OpenAsync (string uri, int port)
		{
			return await _client.OpenAsync (uri, port, false);
		}

		public async Task SendAsync (string command)
		{
			return await _client.SendAsync (command);
		}

		public async Task CloseAsync ()
		{
			return await _client.CloseAsync ();
		}

		#endregion
		WebSocketClient _client;

		public WebSocketPortableWebSocketClient ()
		{
			_client = new WebSocketClient ();
			_client.Closed += _client_Closed;
			_client.Opened +=  _client_Opened;
			_client.MessageReceived += _client_MessageReceived;
			_client.Error +=_client_Error;
		}

		void _client_MessageReceived (WebSocket.Portable.Interfaces.IWebSocketMessage obj)
		{
			Debug.WriteLine($"_client_OnMessage : {obj.ToString()}");
			MessageReceived?.Invoke(this, new MessageEventArgs(obj.ToString()));
		}

		void _client_Closed ()
		{
			Debug.WriteLine($"_client_OnClose");
			Closed?.Invoke (this, null);
		}

		void _client_Error (Exception obj)
		{
			Debug.WriteLine($"_client_OnError : {obj.Message}");
			Error?.Invoke ( this, new ErrorEventArgs(obj));
		}

		void _client_Opened ()
		{
			Debug.WriteLine("_client_OnOpen");
			Opened?.Invoke (this, null);
		}
	}
}

