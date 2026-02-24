using PcRental.Application.Interfaces.Repositories;
using PcRental.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PcRental.Infrastructure.Repositories
{
    public class ComputerRepository : IComputerRepository
    {
        private readonly PcRentalDbContext _dbContext;
        public ComputerRepository(PcRentalDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task AddAsync(Computer computer)
        {
            if (computer == null)
                throw new ArgumentNullException(nameof(computer));

            // Idempotent: check if already exists
            var exists = await _dbContext.Computers.AnyAsync(c => c.Id == computer.Id);
            if (exists)
                return;

            await _dbContext.Computers.AddAsync(computer);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var computer = await _dbContext.Computers.FindAsync(id);
            if (computer == null) return;

            _dbContext.Computers.Remove(computer);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Computer>> GetAllAsync()
        {
            return await _dbContext.Computers.ToListAsync();
        }

        public async Task<Computer?> GetByIdAsync(Guid id)
        {
            return await _dbContext.Computers.FindAsync(id);
        }

        public async Task<IEnumerable<Computer>> GetAvailableAsync()
        {
            return await _dbContext.Computers.AsNoTracking().Where(c => c.IsAvailable).ToListAsync();
        }

        public async Task UpdateAsync(Computer computer)
        {
            if (computer == null)
                throw new ArgumentNullException(nameof(computer));
            
            // Idempotent: attach if not tracked
            var tracked = await _dbContext.Computers.FindAsync(computer.Id);
            if (tracked == null)
            {
                _dbContext.Computers.Attach(computer);
            }

            _dbContext.Entry(computer).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }
    }
}