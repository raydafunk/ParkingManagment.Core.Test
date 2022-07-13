using Microsoft.EntityFrameworkCore;
using ParkingManagment.Core.Domain.Enities;
using ParkingManagment.Core.Enities;

namespace RoomBookingApp.Persistence
{
    public class ParkingManagamentAppDbContext : DbContext
    {
        public ParkingManagamentAppDbContext(DbContextOptions<ParkingManagamentAppDbContext> options) : base(options)
        {

        }
        public DbSet<CarBooking> CarBookings { get; set; }

        public DbSet<ParkingSpace> RoomBookings { get; set; }
        protected  override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CarBooking>().HasData(
                new CarBooking { Id = 1, Name = "Conferance Room A" },
                new CarBooking { Id = 2, Name = "Conferance Room B" },
                new CarBooking { Id = 3, Name = "Conferance Room C" },
                new CarBooking { Id = 4, Name = "Conferance Room A" },
                new CarBooking { Id = 5, Name = "Conferance Room B" },
                new CarBooking { Id = 6, Name = "Conferance Room C" },
                new CarBooking { Id = 7, Name = "Conferance Room C" },
                new CarBooking { Id = 8, Name = "Conferance Room C" },
                new CarBooking { Id = 9, Name = "Conferance Room C" },
                new CarBooking { Id = 10, Name = "Conferance Room C" }

                );
        }
    }
}
