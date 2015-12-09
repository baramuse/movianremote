using System;

namespace MovianRemote.Core.Common
{
	public class ErrorEventArgs : EventArgs
	{
		public Exception Exception { get; private set; }

		public ErrorEventArgs (Exception ex)
		{
			Exception = ex;
		}
	}
}

