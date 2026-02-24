using Xunit;
using Moq;
using PcRental.Application.Services;
using PcRental.Application.Interfaces.Repositories;
using PcRental.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PcRental.Tests.Application
{
    public class ComputerRentalServiceTests
    {
        private readonly Mock<IComputerRepository> _repoMock;
        private readonly ComputerRentalService _service;

        public ComputerRentalServiceTests()
        {
            _repoMock = new Mock<IComputerRepository>();
            _service = new ComputerRentalService(_repoMock.Object);
        }

        [Fact]
        public async Task RentComputerAsync_ShouldSetIsAvailableFalse()
        {
            // Given
            var computer = new Computer("PC1");
            _repoMock.Setup(r => r.GetByIdAsync(computer.Id)).ReturnsAsync(computer);

            // When
            await _service.RentComputerAsync(computer.Id);
        
            // Then
            Assert.False(computer.IsAvailable);
            _repoMock.Verify(r => r.UpdateAsync(computer), Times.Once);
        }

        [Fact]
        public async Task RentComputerAsync_WhenAlreadyRented_ShouldThrow()
        {
            // Given
            var computer = new Computer("PC1");
            computer.Rent(); // already rented
            _repoMock.Setup(r => r.GetByIdAsync(computer.Id)).ReturnsAsync(computer);
        
            // Then
            await Assert.ThrowsAsync<InvalidOperationException>(() => _service.RentComputerAsync(computer.Id));
        }

        [Fact]
        public async Task ReturnComputerAsync_ShouldSetIsAvailableTrue()
        {
            // Given
            var computer = new Computer("PC1");
            computer.Rent();
            _repoMock.Setup(r => r.GetByIdAsync(computer.Id)).ReturnsAsync(computer);
        
            // When
            await _service.ReturnComputerAsync(computer.Id);
        
            // Then
            Assert.True(computer.IsAvailable);
            _repoMock.Verify(r => r.UpdateAsync(computer), Times.Once);
        }

        [Fact]
        public async Task RenameComputerAsync_ShouldUpdateName()
        {
            // Given
            var computer = new Computer("OldName");
            _repoMock.Setup(r => r.GetByIdAsync(computer.Id)).ReturnsAsync(computer);
        
            // When
            await _service.RenameComputerAsync(computer.Id, "NewName");
        
            // Then
            Assert.Equal("NewName", computer.Name);
            _repoMock.Verify(r => r.UpdateAsync(computer), Times.Once);
        }

        [Fact]
        public async Task GetAvailableComputerAsync_ShouldReturnOnlyAvailable()
        {
            // Given
            var c1 = new Computer("PC1");
            var c2 = new Computer("PC2");
            c2.Rent();
        
            // When
            _repoMock.Setup(r => r.GetAvailableAsync()).ReturnsAsync(new List<Computer> { c1 });
        
            // Then
            var result = await _service.GetAvailableComputerAsync();
            Assert.Single(result);
            Assert.Equal("PC1", result.First().Name);
        }

        [Fact]
        public void Should_Fail()
        {
            Assert.True(true); // This will pass
            // Assert.True(false); // This will fail
        }
    }
}