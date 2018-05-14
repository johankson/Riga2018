using System;
using System.Collections.Generic;

using Xamarin.Forms;
using LeetPhotos.Core.IoC;
using LeetPhotos.Core.ViewModel;
using System.Threading.Tasks;
using LeetPhotos.Core.Models;

namespace LeetPhotos.Views
{
    public partial class PhotoDetailView : ContentPage
    {
        public PhotoDetailView(object parameter)
        {
            InitializeComponent();

			var viewModel = Resolver.Resolve<PhotoDetailViewModel>();

			BindingContext = viewModel;

			Task.Run(async () => await viewModel.Initialize(parameter as Photo));
        }
    }
}
