using System;
using System.Collections.Generic;
using Newtonsoft.Json;
namespace LeetPhotos.Core.Models
{
    public class Photo
    {
        public string Url { get; set; }
		public string PhotoUrl { get; set; }
        public DateTime DateTaken { get; set; }
		public string Author { get; set; }
		public string Title { get; set; }
		public List<string> Tags { get; set; }
    }
}
