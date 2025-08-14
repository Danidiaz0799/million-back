using NUnit.Framework;
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
        private OwnersController _controller = null!;
        private Mock<IOwnerRepository> _mockRepository = null!;
        private Mock<IMapperService> _mockMapper = null!;

        [SetUp]
        public void SetUp()
        {
            _mockRepository = new Mock<IOwnerRepository>();
            _mockMapper = new Mock<IMapperService>();
            _controller = new OwnersController(_mockRepository.Object, _mockMapper.Object);
        }

        [Test]
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
            Assert.IsInstanceOf<OkObjectResult>(result.Result);
            var ok = result.Result as OkObjectResult;
            Assert.That(ok, Is.Not.Null);
            Assert.That(ok!.Value, Is.Not.Null);
        }

        [Test]
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
            Assert.IsInstanceOf<OkObjectResult>(result.Result);
            var ok = result.Result as OkObjectResult;
            Assert.That(ok!.Value, Is.EqualTo(ownerDto));
        }

        [Test]
        public async Task GetById_WithInvalidId_ShouldReturnNotFound()
        {
            // Arrange
            _mockRepository.Setup(r => r.GetByIdAsync(999)).ReturnsAsync((Owner?)null);

            // Act
            var result = await _controller.GetById(999);

            // Assert
            Assert.IsInstanceOf<NotFoundResult>(result.Result);
        }
    }
}