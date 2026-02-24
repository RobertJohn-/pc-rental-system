using Xunit;
using PcRental.Infrastructure;
using PcRental.Infrastructure.Repositories;
using PcRental.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using System.Linq;

namespace PcRental.Tests.Infrastructure
{
    public class ComputerRepositoryTests
    {
        private PcRentalDbContext GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<PcRentalDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

            return new PcRentalDbContext(options);
        }

        [Fact]
        public async Task AddAsync_ShouldAddComputer()
        {
            // Given
            var context = GetInMemoryDbContext();
            var repo = new ComputerRepository(context);
        
            // When
            var computer = new Computer("PC1");
            await repo.AddAsync(computer);
        
            // Then
            var dbComputer = await context.Computers.FindAsync(computer.Id);
            Assert.NotNull(dbComputer);
            Assert.Equal("PC1", dbComputer!.Name);
        }

        [Fact]
        public async Task UpdateAsync_ShouldUpdateComputer()
        {
            // Given
            var context = GetInMemoryDbContext();
            var repo = new ComputerRepository(context);
        
            // When
            var computer = new Computer("PC1");
            await repo.AddAsync(computer);

            computer.Rent();
            await repo.UpdateAsync(computer);
        
            // Then
            var updated = await context.Computers.FindAsync(computer.Id);
            Assert.False(updated!.IsAvailable);
        }

        [Fact]
        public async Task DeleteAsync_ShouldRemoveComputer()
        {
            // Given
            var context = GetInMemoryDbContext();
            var repo = new ComputerRepository(context);

            var computer = new Computer("PC1");
            await repo.AddAsync(computer);
        
            // When
            await repo.DeleteAsync(computer.Id);
        
            // Then
            var deleted = await context.Computers.FindAsync(computer.Id);
            Assert.Null(deleted);
        }

        [Fact]
        public async Task GetAvailableAsync_ShouldReturnOnlyAvailableComputers()
        {
            // Given
            var context = GetInMemoryDbContext();
            var repo = new ComputerRepository(context);

            var c1 = new Computer("PC1");
            var c2 = new Computer("PC2");
            c2.Rent();
        
            // When
            await repo.AddAsync(c1);
            await repo.AddAsync(c2);
        
            // Then
            var available = await repo.GetAvailableAsync();
            Assert.Single(available);
            Assert.Equal("PC1", available.First().Name);
        }
    }
}