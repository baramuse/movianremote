using System;
using System.ComponentModel;
using MovianRemote.Core.Common;
using MovianRemote.Core.Interfaces;

namespace MovianRemote.Core.ViewModels
{
	public class MediaControllerViewModel : INotifyPropertyChanged
	{
		public IMovianWebSocketService WSService { get; set; }
		private MovianPropertySubscription _durationSubscription;
		private MovianPropertySubscription _currentTimeSubscription;


		public MediaControllerViewModel (IMovianWebSocketService service)
		{
			WSService = service;
			_currentTimeSubscription = WSService.Subscribe ("media.current.currenttime");
			_currentTimeSubscription.ValueChanged+= _currentTimeSubscription_ValueChanged;
			_durationSubscription = WSService.Subscribe ("media.current.metadata.duration");
			_durationSubscription.ValueChanged+= _durationSubscription_ValueChanged;
		}

		void _durationSubscription_ValueChanged (object sender, string e)
		{
			if (!string.IsNullOrEmpty (e))
				Duration = double.Parse (e);
		}

		void _currentTimeSubscription_ValueChanged (object sender, string e)
		{
			if (!string.IsNullOrEmpty (e)) {
				_seekPosition = double.Parse (e);
				OnPropertyChanged (nameof (SeekPosition));
			}
		}

		double _duration = 1;
		public double Duration {
			get { return _duration; }
			set {
				if (value != _duration) {
					_duration = value;
					OnPropertyChanged (nameof (Duration));
				}
			}
		}

		double _seekPosition;
		public double SeekPosition {
			get { return _seekPosition; }
			set {
				if (value != _seekPosition) {
					_seekPosition = value;
					OnPropertyChanged (nameof (SeekPosition));
				}
			}
		}

		~MediaControllerViewModel ()
		{
			WSService.Unsubscribe (_durationSubscription);
			WSService.Unsubscribe (_currentTimeSubscription);
		}


		public event PropertyChangedEventHandler PropertyChanged;
		protected void OnPropertyChanged(string propertyName) => 
		PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	}
}

