using PcRental.Application.Interfaces.Repositories;
using PcRental.Domain.Entities;

namespace PcRental.Application.Services
{
    public class ComputerRentalService
    {
        private readonly IComputerRepository _computerRepository;

        public ComputerRentalService(IComputerRepository computerRepository)
        {
            _computerRepository = computerRepository
                ?? throw new ArgumentNullException(nameof(computerRepository));
        }

        // Rent a computer by Id
        public async Task RentComputerAsync(Guid computerId)
        {
            var computer = await _computerRepository.GetByIdAsync(computerId);
            if (computer == null)
                throw new InvalidOperationException("Computer not found.");

            // Domain rule: cannot rent twice
            computer.Rent();

            await _computerRepository.UpdateAsync(computer); // persist change
        }

        // Return a computer by Id
        public async Task ReturnComputerAsync(Guid computerId)
        {
            var computer = await _computerRepository.GetByIdAsync(computerId);
            if (computer == null)
                throw new InvalidOperationException("Computer not found.");
            
            // Domain rule: cannot return non-rented
            computer.Return();

            await _computerRepository.UpdateAsync(computer);
        }

        // Get all computers
        public async Task<IEnumerable<Computer>> GetAllComputerAsync()
        {
            return await _computerRepository.GetAllAsync();
        }

        // Get all available computers for renting
        public async Task<IEnumerable<Computer>> GetAvailableComputerAsync()
        {
            return await _computerRepository.GetAvailableAsync();
        }

        // Rename a computer
        public async Task RenameComputerAsync(Guid computerId, string newName)
        {
            var computer = await _computerRepository.GetByIdAsync(computerId);
            if (computer == null)
                throw new InvalidOperationException("Computer not found.");

            computer.Rename(newName);

            await _computerRepository.UpdateAsync(computer);
        }
    }
}