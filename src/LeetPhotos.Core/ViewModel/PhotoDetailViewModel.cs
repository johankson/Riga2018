using System;
using System.Threading.Tasks;
using LeetPhotos.Core.Models;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace LeetPhotos.Core.ViewModel
{
	public class PhotoDetailViewModel : ViewModel
    {
		private string title;
        public string Title
		{
			get => title;
			set => Set(ref title, value);
		}

		private string url;
        public string Url
        {
            get => url;
            set => Set(ref url, value);
        }

		private string tags;
        public string Tags
        {
            get => tags;
			set => Set(ref tags, value);
        }

		private string author;
        public string Author
        {
			get => author;
			set => Set(ref author, value);
        }

		private DateTime dateTaken;
		public DateTime DateTaken
        {
			get => dateTaken;
			set => Set(ref dateTaken, value);
        }

		public async Task Initialize(Photo photo)
		{
			BeginInvokeOnMainThread(() =>
			{
				Title = photo.Title;
				Url = photo.PhotoUrl;
				Tags = FormatTags(photo.Tags);
				DateTaken = photo.DateTaken;
				Author = photo.Author;
			});
		}

		private string FormatTags(List<string> tags)
        {
            var builder = new StringBuilder();

			foreach (var tag in tags)
            {

                builder.Append("#");
                builder.Append(tag);
                builder.Append(" ");
            }

            return builder.ToString();
        }

    }
}
