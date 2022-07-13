using ParkingManagment.Core.Domain.Enities;
using ParkingManagment.Core.Models;

namespace ParkingManagment.Core.Enities
{
    public class ParkingSpace : CarBookingBase
    {
        public int Id { get; set; }
        public CarBooking? CarBooking { get; set; }

        public int SpaceId { get; set; }
    }
}