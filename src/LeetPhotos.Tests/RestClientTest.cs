using System;
using Xunit;
using LeetPhotos.Core.Net;
using System.Threading.Tasks;
using System.Collections.Generic;
using LeetPhotos.Core.Models;

namespace LeetPhotos.Tests
{
    public class RestClientTest
    {
        [Fact]
        public async Task GetFeedTest()
        {
            // Arrange
            var restClient = new RestClient();

            // Act
            var data = await restClient.Get<Feed>("https://api.flickr.com/services/feeds/photos_public.gne?tags=tretton37&format=json");

            // Assert
            Assert.NotNull(data);
        }
    }
}
