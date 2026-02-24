using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PcRental.Domain.Entities
{
    public class Booking
    {
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; } = null!; // Required for EF Core

        [Required]
        public int ComputerId { get; set; }

        [ForeignKey("ComputerId")]
        public Computer Computer { get; set; } = null!;

        [Required]
        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; } // Can be null if ongoing

        // Optional: duration calculated property
        [NotMapped]
        public TimeSpan Duration => EndTime != default ? EndTime - StartTime : TimeSpan.Zero;
    }
}