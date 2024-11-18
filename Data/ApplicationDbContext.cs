using Airline.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
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
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Flight>()
            .HasOne(f => f.ArrivalDestination)
            .WithMany(d => d.ArrivalFlights)
            .HasForeignKey(f => f.ArrivalDestinationId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Passenger>()
            .HasOne(p => p.ApplicationUser)
            .WithOne(u => u.Passenger)
            .HasForeignKey<Passenger>(p => p.ApplicationUserId);
    }
}
