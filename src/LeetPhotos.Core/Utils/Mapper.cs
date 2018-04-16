using System;
using LeetPhotos.Core.Models;
using System.Collections.Generic;
using System.Linq;

namespace LeetPhotos.Core.Utils
{
    public static class Mapper
    {
        public static Photo Map(Item item) => new Photo() { Url = item.media.m, DateTaken = item.date_taken };
        public static List<Photo> Map(IEnumerable<Item> items) => items.Select(x => Map(x)).ToList();
    }
}
