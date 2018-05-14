using System;
using System.Collections.Generic;

using Xamarin.Forms;
using LeetPhotos.Core.IoC;
using LeetPhotos.Core.ViewModel;

namespace LeetPhotos.Views
{
	public partial class PhotosView : ContentPage
	{
		public PhotosView()
		{
			InitializeComponent();

			NavigationPage.SetBackButtonTitle(this, string.Empty);

			BindingContext = Resolver.Resolve<PhotosViewModel>();
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();

			PhotoList.ItemSelected += Photo_Selected;
		}

		protected override void OnDisappearing()
		{
			base.OnDisappearing();

			PhotoList.ItemSelected -= Photo_Selected;
		}

		private void Photo_Selected(object sender, EventArgs e)
		{
			if(PhotoList.SelectedItem != null)
			{
				PhotoList.SelectedItem = null;
			}
		}
	}
}
