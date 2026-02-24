namespace PcRental.Application.Interfaces.Repositories;

using PcRental.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IComputerRepository
{
    // <summary>
    // Get a computer by its unique Id.
    // </summary>
    Task<Computer?> GetByIdAsync(Guid id);

    // Get all computers.
    Task<IEnumerable<Computer>> GetAllAsync();

    // Add a new computer to the system.
    Task AddAsync(Computer computer);

    // Update an existing computer.
    // Idempotent: can be called multiple times with the same entity.
    Task UpdateAsync(Computer computer);

    // Remove a computer by Id.
    Task DeleteAsync(Guid id);

    // Optional: Get all available computers.
    // Useful for renting.
    Task<IEnumerable<Computer>> GetAvailableAsync();
    
}