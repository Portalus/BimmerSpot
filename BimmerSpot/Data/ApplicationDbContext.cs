using BimmerSpot.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BimmerSpot.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
{
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Spot>()
            .HasMany(s => s.Attendants)
            .WithMany(u => u.AttendedSpots);

        builder.Entity<Spot>()
            .HasOne(s => s.CreatedBy)
            .WithMany(u => u.CreatedSpots)
            .OnDelete(DeleteBehavior.SetNull);
    }

    public DbSet<Spot> Spots { get; set; }
}
