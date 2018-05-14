using System;
using System.ComponentModel;
using LeetPhotos.Core.Services;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.ObjectModel;
using System.Windows.Input;
using LeetPhotos.Core.Utils;
using System.Xml.Linq;
namespace LeetPhotos.Core.ViewModel
{
	public class PhotosViewModel : ViewModel
    {
		private IPhotoService photoService;

		public PhotosViewModel(IPhotoService photoService)
        {
			this.photoService = photoService;

			Task.Run(Initialize);
        }
        
		public async Task Initialize()
		{
			BeginInvokeOnMainThread(() =>
			{
				IsBusy = true;
			});

			var allPhotos = await photoService.GetPhotosAsync();
            var items = allPhotos.Select(x => new PhotoViewModel() { Item = x }).ToList();

			BeginInvokeOnMainThread(() =>
            {
				Photos = new ObservableCollection<PhotoViewModel>(items);

				IsBusy = false;                 
			});
		}

		private ObservableCollection<PhotoViewModel> photos;
		public ObservableCollection<PhotoViewModel> Photos
		{
			get => photos;
			set => Set(ref photos, value);
		}

		public ICommand Refresh => new LeetCommand(async () =>
		{
			IsBusy = true;

			var allPhotos = await photoService.GetPhotosAsync();
			var items = allPhotos.Where(x => x.DateTaken > Photos.First().Item.DateTaken).Select(x => new PhotoViewModel() { Item = x }).ToList();

			foreach(var item in items)
			{
				Photos.Insert(0, item);
			}

			IsBusy = false;
		});

		public PhotoViewModel SelectedPhoto
		{
			set => value?.Select.Execute(null);
		}
    }
}
