using Xunit;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using System.Text;
using System.Text.Json;
using RealEstate.Application.DTOs;

namespace RealEstate.Tests.Integration
{
    public class PropertiesIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;
        private readonly HttpClient _client;

        public PropertiesIntegrationTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
            _client = _factory.CreateClient();
        }

        [Fact]
        public async Task Get_Properties_ShouldReturnSuccessStatusCode()
        {
            // Act
            var response = await _client.GetAsync("/api/properties");

            // Assert
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task Get_Properties_WithFilters_ShouldReturnFilteredResults()
        {
            // Act
            var response = await _client.GetAsync("/api/properties?name=Casa&page=1&pageSize=5");

            // Assert
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            Assert.Contains("\"data\"", content);
            Assert.Contains("\"totalCount\"", content);
        }

        [Theory]
        [InlineData("/api/properties")]
        [InlineData("/api/owners")]
        [InlineData("/api/propertyimages")]
        [InlineData("/api/propertytraces")]
        public async Task Get_Endpoints_ShouldReturnSuccessStatusCode(string url)
        {
            // Act
            var response = await _client.GetAsync(url);

            // Assert
            response.EnsureSuccessStatusCode();
        }
    }
}