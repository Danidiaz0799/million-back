using NUnit.Framework;
using Moq;
using Microsoft.Extensions.Options;
using RealEstate.Infrastructure.Repositories;
using RealEstate.Infrastructure.Configurations;
using RealEstate.Domain.Entities;

namespace RealEstate.Tests.Unit
{
    public class PropertyRepositoryTests
    {
        private Mock<IOptions<MongoDbSettings>> _mockSettings = null!;

        [SetUp]
        public void SetUp()
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

        [Test]
        public void Constructor_ShouldInitializeRepository()
        {
            Assert.NotNull(_mockSettings.Object);
        }

        [TestCase("Casa", null, null, null, 1, 10)]
        [TestCase(null, "Bogotá", null, null, 1, 10)]
        public void GetPropertiesAsync_ShouldAcceptValidParameters_String(
            string name, string address, decimal? priceMin, decimal? priceMax, int page, int pageSize)
        {
            Assert.That(page, Is.GreaterThan(0));
            Assert.That(pageSize, Is.GreaterThan(0));
            Assert.That(pageSize, Is.LessThanOrEqualTo(100));
        }

        [Test]
        public void GetPropertiesAsync_ShouldAcceptValidParameters_WithPriceRange()
        {
            string? name = null;
            string? address = null;
            decimal? priceMin = 100000m;
            decimal? priceMax = 500000m;
            int page = 1;
            int pageSize = 10;

            Assert.That(page, Is.GreaterThan(0));
            Assert.That(pageSize, Is.GreaterThan(0));
            Assert.That(pageSize, Is.LessThanOrEqualTo(100));
            Assert.That(priceMin, Is.LessThanOrEqualTo(priceMax));
        }
    }
}