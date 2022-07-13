using ParkingManagment.Core.Domain.Enities;
using ParkingManagment.Core.Enities;

namespace ParkingManagment.Core.Services
{
    public interface ICarParkingBookingService
    {
        void Save(CarBooking carbooking);

        IEnumerable<ParkingSpace> GetAvailabeParkingSpaces(DateTime date);
        
    }
}
