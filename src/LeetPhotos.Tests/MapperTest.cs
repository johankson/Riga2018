using System;
using Xunit;
using System.Threading.Tasks;
using LeetPhotos.Core.Net;
using LeetPhotos.Core.Models;
using LeetPhotos.Core.Utils;

namespace LeetPhotos.Tests
{
    public class MapperTest
    {

        [Fact]
        public async Task MapItemsTest()
        {
            // Arrange (TODO Get data from mock?)
            var restClient = new RestClient();
            var data = await restClient.Get<Feed>("https://api.flickr.com/services/feeds/photos_public.gne?tags=tretton37&format=json");

            // Act
            var photos = Mapper.Map(data.items);

            // Assert
            Assert.NotNull(photos);
        }
    }
}
