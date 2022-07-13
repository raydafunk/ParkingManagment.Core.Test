using ParkingManagment.Core.Domain.Enities;
using ParkingManagment.Core.Enities;
using ParkingManagment.Core.Services;
using RoomBookingApp.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingManagment.Persistence.Repositories
{
    public class CarParkingBookingService : ICarParkingBookingService
    {
        private ParkingManagamentAppDbContext _context;

        public CarParkingBookingService(ParkingManagamentAppDbContext context)
        {
            this._context = context;
        }

        public IEnumerable<ParkingSpace> GetAvailabeParkingSpaces(DateTime date)
        {
            return (IEnumerable<ParkingSpace>)_context.CarBookings.Where(q => !q.ParkingSpaces!.Any(x => x.BookingDateFrom == date)).ToList();
        }

        public void Save(CarBooking carbooking)
        {
            _context.Add(carbooking);
            _context.SaveChanges();
        }
    }
}
