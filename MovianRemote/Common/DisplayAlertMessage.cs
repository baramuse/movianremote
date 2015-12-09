using System;

namespace MovianRemote.Core.Common
{
	public class DisplayAlertMessage
	{
		public string Title { get; set; }
		public string Message { get; set; }
		public string Cancel { get; set; }
		public string Accept { get; set; }

		public Action<bool> OnCompleted { get; set; }
	}
}

