using ParkingManagment.Core.Enities;
using ParkingManagment.Core.Models;

namespace ParkingManagment.Core.Processors
{
    public interface ICarParkingRequestProcessor
    {
        CarParkingBookingResult ProcessRequest(CarParkingBookingRequest parkingRequest);

    }
}