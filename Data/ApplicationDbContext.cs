using Airline.Models;
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext
{
    public DbSet<Flight> Flights { get; set; }
    public DbSet<Destination> Destinations { get; set; }
    public DbSet<Ticket> Tickets { get; set; }
    public DbSet<Passenger> Passengers { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Aircraft> Aircrafts { get; set; }
    public DbSet<Crew> Crews { get; set; }
    public DbSet<AircraftType> AircraftTypes { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Flight>()
            .HasOne(f => f.DepartureDestination)
            .WithMany(d => d.DepartureFlights)
            .HasForeignKey(f => f.DepartureDestinationId)
            .OnDelete(DeleteBehavior.Restrict); // Если хотите запретить каскадное удаление

        modelBuilder.Entity<Flight>()
            .HasOne(f => f.ArrivalDestination)
            .WithMany(d => d.ArrivalFlights)
            .HasForeignKey(f => f.ArrivalDestinationId)
            .OnDelete(DeleteBehavior.Restrict); // Если хотите запретить каскадное удаление
    }
}
