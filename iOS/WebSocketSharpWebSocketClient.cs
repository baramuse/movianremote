using System;
using System.Threading.Tasks;
using MovianRemote.iOS;
using MovianRemote.Core.Interfaces;
using MovianRemote.Core.Common;

namespace MovianRemote.iOS
{
	
	public class WebSocketSharpWebSocketClient : IWebSocketClient
	{
		public event EventHandler<MessageEventArgs> MessageReceived;
		public event EventHandler Opened;
		public event EventHandler Closed;
		public event EventHandler<ErrorEventArgs> Error;

		WebSocketSharp.WebSocket _client;

		public void OpenAsync (string uri, int port)
		{
			throw new NotImplementedException ();
		}

		public void OpenAsync (string uri)
		{
			_client = new WebSocketSharp.WebSocket (uri);
			_client.EmitOnPing = true;
			_client.ConnectAsync ();
			_client.OnOpen += _client_OnOpen;
			_client.OnError+= _client_OnError;
			_client.OnClose+= _client_OnClose;
			_client.OnMessage+= _client_OnMessage;
		}

		public void CloseAsync()
		{
			_client?.CloseAsync (WebSocketSharp.CloseStatusCode.Normal);
		}

		void _client_OnMessage (object sender, WebSocketSharp.MessageEventArgs e)
		{
			Console.WriteLine($"_client_OnMessage : {e.Data}");
			MessageReceived?.Invoke(sender, new MessageEventArgs(e.Data));
		}

		void _client_OnClose (object sender, WebSocketSharp.CloseEventArgs e)
		{
			Console.WriteLine($"_client_OnClose (wasClean={e.WasClean}): [{e.Code}] {e.Reason}");
			Closed?.Invoke (sender, null);
		}

		void _client_OnError (object sender, WebSocketSharp.ErrorEventArgs e)
		{
			Console.WriteLine($"_client_OnError : {e.Message}");
			Error?.Invoke (sender, new ErrorEventArgs(e.Exception));
		}

		void _client_OnOpen (object sender, EventArgs e)
		{
			Console.WriteLine("_client_OnOpen");
			//_client.Ping ("connect_test");
			Opened?.Invoke (sender, e);
		}

		public void SendAsync (string command)
		{
			_client.SendAsync (command, null);
		}
	}
}

