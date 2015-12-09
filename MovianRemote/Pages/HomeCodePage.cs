using System;

using Xamarin.Forms;

namespace MovianRemote
{
	public class HomeCodePage : ContentPage
	{
		public HomeCodePage ()
		{
			Content = new StackLayout { 
				Children = {
					new Label { Text = "Hello ContentPage" }
				}
			};
		}
	}
}


