namespace PcRental.Domain.Entities;

public class Computer
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = default!;
    public bool IsAvailable { get; private set; }
    public DateTime CreatedAt { get; private set; }
    private Computer() { } // For ORM

    public Computer(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Computer name is required.");

        Id = Guid.NewGuid();
        Name = name;
        IsAvailable = true;
        CreatedAt = DateTime.UtcNow;
    }

    public void Rent()
    {
        if (!IsAvailable)
            throw new InvalidOperationException("Computer is already rented.");

        IsAvailable = false;
    }

    public void Return()
    {
        if (IsAvailable)
            throw new InvalidOperationException("Computer is not rented.");

        IsAvailable = true;
    }

    public void Rename(string newName)
    {
        if (string.IsNullOrWhiteSpace(newName))
            throw new ArgumentException("Computer name cannot be empty.");

        Name = newName;
    }
}