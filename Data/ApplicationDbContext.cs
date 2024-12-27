using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using AracKiralamaPortal.Models;

namespace AracKiralamaPortal.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Tablolar (DbSet)
        public DbSet<Car> Cars { get; set; } // Araçlar Tablosu
        public DbSet<Reservation> Reservations { get; set; } // Rezervasyonlar Tablosu
        public DbSet<Payment> Payments { get; set; } // Ödemeler Tablosu
    }
}
