using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace LeetPhotos.Core.ViewModel
{
	public abstract class ViewModel : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		protected void Set<T>(ref T field,T newValue, [CallerMemberName] string propertyName = null)
		{
			if (!EqualityComparer<T>.Default.Equals(field, newValue))
            {
				field = newValue;
				RaisePropertyChanged(propertyName);
            }
		}

		protected void RaisePropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		public static Action<Action> BeginInvokeOnMainThread { get; set; }

		private bool isBusy;
        public bool IsBusy
		{
			get => isBusy;
			set => Set(ref isBusy, value);
		}
	}
}
