using System;
using LeetPhotos.Core.Models;
using System.Net.Sockets;
using System.Windows.Input;
using LeetPhotos.Core.IoC;
using LeetPhotos.Core.Utils;
using TinyNavigationHelper.Abstraction;
namespace LeetPhotos.Core.ViewModel
{
	public class PhotoViewModel : ViewModel
	{
		public Photo Item { get; set; }

		public ICommand Select => new LeetCommand(async () => await NavigationHelper.Current.NavigateToAsync("PhotoDetailView", Item));
    }
}
