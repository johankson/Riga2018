using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LeetPhotos.Core.Models;
using LeetPhotos.Core.Net;
using System.Linq;
using System.Xml.Linq;
using Newtonsoft.Json.Bson;
using System.Text;

namespace LeetPhotos.Core.Services
{
	public class PhotoService : IPhotoService
    {
		private IRestClient restClient;

		public PhotoService(IRestClient restClient)
        {
			this.restClient = restClient;
        }
        
		public async Task<List<Photo>> GetPhotosAsync()
		{
			var feed = await restClient.Get<Feed>("https://api.flickr.com/services/feeds/photos_public.gne?tags=tretton37&format=json");
   
			var photos = feed.items.Select(x => new Photo()
			{
				PhotoUrl = x.media.m,
				Title = x.title,
				Tags = x.tags.Split(' ').ToList(),
				Url = x.link,
				DateTaken = x.published,
				Author = x.author.Split(new string[2] { "(\"", "\")" }, StringSplitOptions.None)[1]
			}).OrderByDescending(x => x.DateTaken).ToList();

			return photos;
		}

        
	}
}
