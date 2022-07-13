using ParkingManagment.Core.Enities;
using ParkingManagment.Core.Models;

namespace ParkingManagment.Core.Domain.Enities
{
    public class CarBooking : CarBookingBase
    {
        

        public int Id { get; set; }

        public string? Name;

        public List<ParkingSpace> ParkingSpaces { get; set; }   
    }
}