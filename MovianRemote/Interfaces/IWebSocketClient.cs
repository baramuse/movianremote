using System;
using System.Threading.Tasks;
using MovianRemote.Core.Common;

namespace MovianRemote.Core.Interfaces
{
	public interface IWebSocketClient
	{
		Task OpenAsync(string uri, int port);
		Task SendAsync(string command);
		Task CloseAsync ();

		event EventHandler Opened;
		event EventHandler Closed;
		event EventHandler<ErrorEventArgs> Error;
		event EventHandler<MessageEventArgs> MessageReceived;
	}
}

