using PcRental.Domain.Entities;

public interface IBookingRepository
{
    Task<Booking> AddAsync(Booking booking);
    Task<Booking> GetByIdAsync(int id);
    Task UpdateAsync(Booking booking);
    Task<List<Booking>> GetActiveBookingAsync();
}