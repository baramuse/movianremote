using System;
using System.ComponentModel;

namespace MovianRemote.Core.ViewModels
{
	public class ViewModelBase  : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;
		protected void OnPropertyChanged(string propertyName) => 
		PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	}
}

