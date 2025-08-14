using Xunit;
using Microsoft.AspNetCore.Mvc;
using Moq;
using RealEstate.Api.Controllers;
using RealEstate.Application.Interfaces;
using RealEstate.Application.DTOs;
using RealEstate.Domain.Entities;

namespace RealEstate.Tests.Unit
{
    public class OwnersControllerTests
    {
        private readonly OwnersController _controller;
        private readonly Mock<IOwnerRepository> _mockRepository;
        private readonly Mock<IMapperService> _mockMapper;

        public OwnersControllerTests()
        {
            _mockRepository = new Mock<IOwnerRepository>();
            _mockMapper = new Mock<IMapperService>();
            _controller = new OwnersController(_mockRepository.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task Get_ShouldReturnOkResult_WithOwnersList()
        {
            // Arrange
            var owners = new List<Owner>
            {
                new Owner { IdOwner = 1, Name = "Test Owner", Address = "Test Address", Photo = "test.jpg", Birthday = DateTime.Now.AddYears(-30) }
            };
            var ownerDtos = new List<OwnerDto>
            {
                new OwnerDto { Name = "Test Owner", Address = "Test Address", Photo = "test.jpg", Birthday = DateTime.Now.AddYears(-30) }
            };

            _mockRepository.Setup(r => r.GetOwnersAsync()).ReturnsAsync(owners);
            _mockMapper.Setup(m => m.MapToDto(It.IsAny<Owner>())).Returns(ownerDtos.First());

            // Act
            var result = await _controller.Get();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.NotNull(okResult.Value);
        }

        [Fact]
        public async Task GetById_WithValidId_ShouldReturnOwner()
        {
            // Arrange
            var owner = new Owner { IdOwner = 1, Name = "Test Owner", Address = "Test Address", Photo = "test.jpg", Birthday = DateTime.Now.AddYears(-30) };
            var ownerDto = new OwnerDto { Name = "Test Owner", Address = "Test Address", Photo = "test.jpg", Birthday = DateTime.Now.AddYears(-30) };

            _mockRepository.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(owner);
            _mockMapper.Setup(m => m.MapToDto(owner)).Returns(ownerDto);

            // Act
            var result = await _controller.GetById(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(ownerDto, okResult.Value);
        }

        [Fact]
        public async Task GetById_WithInvalidId_ShouldReturnNotFound()
        {
            // Arrange
            _mockRepository.Setup(r => r.GetByIdAsync(999)).ReturnsAsync((Owner?)null);

            // Act
            var result = await _controller.GetById(999);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }
    }
}