using System;
using Xunit;
using LeetPhotos.Core.Net;
using System.Threading.Tasks;
using System.Collections.Generic;
using LeetPhotos.Core.Models;

namespace LeetPhotos.Tests
{
    public class UnitTest1
    {
        [Fact]
        public async Task Test1()
        {
            // Arrange
            var service = new RestClient();

            // Act
            var data = await service.Get<Feed>("https://api.flickr.com/services/feeds/photos_public.gne?tags=tretton37&format=json");

            // Assert
            Assert.NotNull(data);
        }
    }
}
