using System;
using Xunit;
using LeetPhotos.Core.Net;
using System.Threading.Tasks;

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
            var data = await service.Get<object>("https://api.flickr.com/services/feeds/photos_public.gne?tags=tretton37&format=json");

            // Assert
            Assert.NotNull(data);
        }
    }
}
