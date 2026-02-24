using Xunit;
using PcRental.Domain.Entities;
using System;
using System.Reflection;
using System.Reflection.Metadata;

namespace PcRental.Tests.Domain
{
    public class ComputerTests
    {
        [Fact]
        public void Constructor_ShouldInitializeProperly()
        {
            var computer = new Computer("PC1");

            Assert.Equal("PC1", computer.Name);
            Assert.True(computer.IsAvailable);
            Assert.NotEqual(default, computer.Id);
            Assert.NotEqual(default, computer.CreatedAt);
        }

        [Fact]
        public void Rent_ShouldMarkAsNotAvailable()
        {
            // Given
            var computer = new Computer("PC1");
            computer.Rent();
            
            // Then
            Assert.False(computer.IsAvailable);
        }

        [Fact]
        public void Rent_WhenAlreadyRented_ShouldThrow()
        {
            // Given
            var computer = new Computer("PC1");
            computer.Rent();
        
            // Then
            Assert.Throws<InvalidOperationException>(() => computer.Rent());
        }

        [Fact]
        public void Return_ShouldMarkAsAvailable()
        {
            // Given
            var computer = new Computer("PC1");
            computer.Rent();
            computer.Return();
        
            // Then
            Assert.True(computer.IsAvailable);
        }

        [Fact]
        public void Return_WhenNotRented_ShouldThrow()
        {
            // Given
            var computer = new Computer("PC1");
        
            // Then
            Assert.Throws<InvalidOperationException>(() => computer.Return());
        }

        [Fact]
        public void Rename_ShouldUpdateName()
        {
            // Given
            var computer = new Computer("OldName");
            computer.Rename("NewName");
        
            // Then
            Assert.Equal("NewName", computer.Name);
        }

        [Fact]
        public void Rename_EmptyName_ShouldThrow()
        {
            // Given
            var computer = new Computer("OldName");
        
            // Then
            Assert.Throws<ArgumentException>(() => computer.Rename(""));
        }

    }
}