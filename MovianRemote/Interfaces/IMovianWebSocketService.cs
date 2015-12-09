using System;
using MovianRemote.Core.Common;

namespace MovianRemote.Core.Interfaces
{
	public interface IMovianWebSocketService 
	{
		MovianPropertySubscription Subscribe (string propertyPath);
		void Unsubscribe (MovianPropertySubscription subscription);
		event EventHandler ConnectionStateChanged;

	}
}

