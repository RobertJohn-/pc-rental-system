using System.ComponentModel.DataAnnotations;

namespace PcRental.Domain.Entities;

public class User
{
    public int Id { get; set; }

    [Required, MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [Required, EmailAddress, MaxLength(200)]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string PasswordHash { get; set; } = string.Empty; // Never store plain password

    [Required]
    public string Role { get; set; } = "Customer"; // Roles: Admin, Staff, Customer

    // Navigation property - a user can have multiple bookings
    public List<Booking> Bookings { get; set; } = new();
    
}