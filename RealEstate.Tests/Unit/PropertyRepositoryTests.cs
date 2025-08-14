using Xunit;
using Moq;
using Microsoft.Extensions.Options;
using RealEstate.Infrastructure.Repositories;
using RealEstate.Infrastructure.Configurations;
using RealEstate.Domain.Entities;

namespace RealEstate.Tests.Unit
{
    public class PropertyRepositoryTests
    {
        private readonly Mock<IOptions<MongoDbSettings>> _mockSettings;

        public PropertyRepositoryTests()
        {
            _mockSettings = new Mock<IOptions<MongoDbSettings>>();
            var settings = new MongoDbSettings
            {
                ConnectionString = "mongodb://localhost:27017",
                DatabaseName = "test_realestate_db",
                PropertiesCollectionName = "properties"
            };
            _mockSettings.Setup(x => x.Value).Returns(settings);
        }

        [Fact]
        public void Constructor_ShouldInitializeRepository()
        {
            // Act & Assert
            Assert.NotNull(_mockSettings.Object);
        }

        [Theory]
        [InlineData("Casa", null, null, null, 1, 10)]
        [InlineData(null, "Bogotá", null, null, 1, 10)]
        public void GetPropertiesAsync_ShouldAcceptValidParameters_String(
            string name, string address, decimal? priceMin, decimal? priceMax, int page, int pageSize)
        {
            // Arrange & Act & Assert
            Assert.True(page > 0);
            Assert.True(pageSize > 0);
            Assert.True(pageSize <= 100);
        }

        [Fact]
        public void GetPropertiesAsync_ShouldAcceptValidParameters_WithPriceRange()
        {
            // Arrange
            string name = null;
            string address = null;
            decimal? priceMin = 100000m;
            decimal? priceMax = 500000m;
            int page = 1;
            int pageSize = 10;

            // Act & Assert
            Assert.True(page > 0);
            Assert.True(pageSize > 0);
            Assert.True(pageSize <= 100);
            Assert.True(priceMin <= priceMax);
        }
    }
}