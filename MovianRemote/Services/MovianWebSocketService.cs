using System;
using System.Collections.Generic;
using MovianRemote.Core.Common;
using MovianRemote.Core.Interfaces;

namespace MovianRemote.Core.Services
{
	public class MovianWebSocketService : IMovianWebSocketService
	{
		public event EventHandler ConnectionStateChanged;

		Dictionary<int, MovianPropertySubscription> _subscriptionIds = new Dictionary<int, MovianPropertySubscription>();
		IWebSocketClient _client;
		int _subscriptionId = 1;

		public MovianWebSocketService (IWebSocketClient client)
		{
			_client = client;
			_client.MessageReceived+= _client_MessageReceived;
		}

		void _client_MessageReceived (object sender, MessageEventArgs e)
		{
			parseServerMessage (e.Message);
		}

		private void parseServerMessage(string message)
		{
			var a = Newtonsoft.Json.JsonConvert.DeserializeObject<string[]> (message);

			var subId = int.Parse (a [1]);
			if (_subscriptionIds.ContainsKey (subId))
				_subscriptionIds [subId].Value = a [2];
		}

		public void Unsubscribe(MovianPropertySubscription subscription)
		{
			_client.SendAsync ($"[2, {subscription.SubscriptionId}]");
			_subscriptionIds.Remove (subscription.SubscriptionId);
		}


		public MovianPropertySubscription Subscribe(string propPath)
		{
			var s = new MovianPropertySubscription (propPath,_subscriptionId++);
			_subscriptionIds.Add (s.SubscriptionId, s);

			_client.SendAsync ($"[1, {s.SubscriptionId}, 0, \"{s.PropPath}\"]");
			return s;
		}

		enum MovianMessageType
		{
			Subscribe = 1,
			Unsubscribe = 2,
			SetValue = 3,
			NotifiyValue = 4
		}
	}
}

