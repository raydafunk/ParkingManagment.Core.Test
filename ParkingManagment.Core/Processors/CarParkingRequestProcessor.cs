using ParkingManagment.Core.Domain.Enities;
using ParkingManagment.Core.Enities;
using ParkingManagment.Core.Enums;
using ParkingManagment.Core.Models;
using ParkingManagment.Core.Services;

namespace ParkingManagment.Core.Processors
{
    public class CarParkingRequestProcessor : ICarParkingRequestProcessor
    {
        private readonly ICarParkingBookingService _carParkingBookingService;

        public CarParkingRequestProcessor(Services.ICarParkingBookingService carParkingBookingService)
        {
            this._carParkingBookingService = carParkingBookingService;
        }

        public CarParkingBookingResult ProcessRequest(CarParkingBookingRequest parkingRequest)
        {
            if (parkingRequest is null)
            {
                throw new ArgumentNullException(nameof(parkingRequest));
            }

            var availableparkingSpaces = _carParkingBookingService.GetAvailabeParkingSpaces(parkingRequest.BookingDateFrom);
            var result = CreateCarBookingObject<CarParkingBookingResult>(parkingRequest);


            if (availableparkingSpaces.Any())
            {
                var carParkingSpace = availableparkingSpaces.First();
                var carparking = CreateCarBookingObject<CarBooking>(parkingRequest);
                carparking.Id = carParkingSpace.SpaceId;
                _carParkingBookingService.Save(carparking);

                result.CarBookingId = carparking.Id;
                result.Flag = BookingResultFlag.Success;
            }
            else
            {
                result.Flag = BookingResultFlag.Failure;
            }

            return result;

        }

        private static TCarBooking CreateCarBookingObject<TCarBooking>(CarParkingBookingRequest parkingRequest) where TCarBooking
           : CarBookingBase, new()
        {
            return new TCarBooking
            {
                ParkingPlaceId = parkingRequest.ParkingPlaceId,
                FulNamae = parkingRequest.FulNamae,
                Car = parkingRequest.Car,
                Email = parkingRequest.Email,
                BookingDateFrom = parkingRequest.BookingDateFrom,
                BookingDateTo = parkingRequest.BookingDateTo,

            };
        }

 
    }
}