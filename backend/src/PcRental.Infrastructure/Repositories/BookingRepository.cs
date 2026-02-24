using Microsoft.EntityFrameworkCore;
using PcRental.Application.Interfaces;
using PcRental.Domain.Entities;

namespace PcRental.Infrastructure.Repositories
{
    public class BookingRepository : IBookingRepository
    {
        private readonly PcRentalDbContext _context;

        public BookingRepository(PcRentalDbContext context)
        {
            _context = context;
        }

        public async Task<Booking> AddAsync(Booking booking)
        {
            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();
            return booking;
        }

        public async Task<Booking?> GetByIdAsync(int id)
        {
            return await _context.Bookings
                .Include(b => b.Computer)
                .Include(b => b.User)
                .FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task UpdateAsync(Booking booking)
        {
            _context.Bookings.Update(booking);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Booking>> GetActiveBookingAsync()
        {
            return await _context.Bookings
                .Include(b => b.Computer)
                .Include(b => b.User)
                .Where(b => b.EndTime > DateTime.Now)
                .ToListAsync();
        }
        
    }
}