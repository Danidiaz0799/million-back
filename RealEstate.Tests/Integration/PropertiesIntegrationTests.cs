using NUnit.Framework;
using Microsoft.AspNetCore.Mvc.Testing;

namespace RealEstate.Tests.Integration
{
    [TestFixture]
    public class PropertiesIntegrationTests
    {
        private WebApplicationFactory<Program> _factory = null!;
        private HttpClient _client = null!;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _factory = new WebApplicationFactory<Program>();
        }

        [SetUp]
        public void SetUp()
        {
            _client = _factory.CreateClient();
        }

        [TearDown]
        public void TearDown()
        {
            _client.Dispose();
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            _factory.Dispose();
        }

        [Test]
        public async Task Get_Properties_ShouldReturnSuccessStatusCode()
        {
            // Act
            var response = await _client.GetAsync("/api/properties");

            // Assert
            response.EnsureSuccessStatusCode();
        }

        [Test]
        public async Task Get_Properties_WithFilters_ShouldReturnFilteredResults()
        {
            // Act
            var response = await _client.GetAsync("/api/properties?name=Casa&page=1&pageSize=5");

            // Assert
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            StringAssert.Contains("\"data\"", content);
            StringAssert.Contains("\"totalCount\"", content);
        }

        [TestCase("/api/properties")]
        [TestCase("/api/owners")]
        [TestCase("/api/propertyimages")]
        [TestCase("/api/propertytraces")]
        public async Task Get_Endpoints_ShouldReturnSuccessStatusCode(string url)
        {
            // Act
            var response = await _client.GetAsync(url);

            // Assert
            response.EnsureSuccessStatusCode();
        }
    }
}