using System;

namespace MovianRemote.Core.Common
{
	public class MovianPropertySubscription
	{
		public event EventHandler<string> ValueChanged;

		public string PropPath { get; private set; }
		public int SubscriptionId { get; private set; }

		private string _value;
		public string Value
		{
			get
			{
				return this._value;
			}
			set
			{
				if (value != this._value)
				{
					this._value = value;
					OnValueChanged(this._value);
				}
			}
		}

		protected void OnValueChanged(string value) => 
		ValueChanged?.Invoke(this, value);

		public MovianPropertySubscription (string propPath, int subscriptionId)
		{
			PropPath = propPath;
			SubscriptionId = subscriptionId;
		}
	}
}

