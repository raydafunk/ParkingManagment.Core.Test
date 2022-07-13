using Moq;
using ParkingManagment.Core.Domain.Enities;
using ParkingManagment.Core.Enities;
using ParkingManagment.Core.Enums;
using ParkingManagment.Core.Models;
using ParkingManagment.Core.Processors;
using ParkingManagment.Core.Services;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace ParkingManagment.Core.Test
{
    public class CaParkingRequestProcessorTest
    {
        private readonly CarParkingRequestProcessor _processor;
        private readonly CarParkingBookingRequest _parkingRequest;
        private readonly Mock<ICarParkingBookingService> _carBookingServiceMock;
        private List<ParkingSpace> _availableparkingSpaces;

        public CaParkingRequestProcessorTest()
        {
           _parkingRequest = new CarParkingBookingRequest
            {
                ParkingPlaceId = 1,
                FulNamae = "TestName",
                Email = "test@request.com",
                Car = "Nession",
                BookingDateFrom = new DateTime(2022, 07, 07),
                BookingDateTo = new DateTime()

            };

            _availableparkingSpaces = new List<ParkingSpace> { new ParkingSpace() { Id = 1 } };

            _carBookingServiceMock = new Mock<ICarParkingBookingService>();
            _carBookingServiceMock.Setup(q => q.GetAvailabeParkingSpaces(_parkingRequest.BookingDateFrom))
                 .Returns(_availableparkingSpaces);
            _processor = new CarParkingRequestProcessor(_carBookingServiceMock.Object);

        }

        [Fact]
        public void should_Return_Car_Booking_Response_With_Request_Values()
        {

            CarParkingBookingResult result = _processor.ProcessRequest(_parkingRequest);

            result.ShouldNotBeNull();
            result.ParkingPlaceId.ShouldBe(_parkingRequest.ParkingPlaceId);
            result.FulNamae.ShouldBe(_parkingRequest.FulNamae);
            result.Car.ShouldBe(_parkingRequest.Car); 
            result.BookingDateFrom.ShouldBe(_parkingRequest.BookingDateFrom);
            result.BookingDateTo.ShouldBe(_parkingRequest.BookingDateTo);
            result.Email.ShouldBe(_parkingRequest.Email);
        }
        
        [Fact]
        public void Should_Thrown_Exeception_For_Null_Request_When_request_is_called()
        {

            var excepetion = Should.Throw<ArgumentNullException>(() => _processor.ProcessRequest(null!));
            excepetion.ParamName.ShouldBe("parkingRequest");
        }
        
        [Fact]
        public void Should_Save_Car_BooKing_Request()
        {
            CarBooking savedCarBooking = null!;
            _carBookingServiceMock.Setup(q => q.Save(It.IsAny<CarBooking>()))
                .Callback<CarBooking>(booking =>
                {
                    savedCarBooking = booking;
                });

            _processor.ProcessRequest(_parkingRequest);

            _carBookingServiceMock.Verify(q => q.Save(It.IsAny<CarBooking>()), Times.Once);

            savedCarBooking.ShouldNotBeNull();
            savedCarBooking.ParkingPlaceId.ShouldBe(_parkingRequest.ParkingPlaceId);
            savedCarBooking.FulNamae.ShouldBe(_parkingRequest.FulNamae);
            savedCarBooking.Car.ShouldBe(_parkingRequest.Car);
            savedCarBooking.Email.ShouldBe(_parkingRequest.Email);
            savedCarBooking.BookingDateFrom.ShouldBe(_parkingRequest.BookingDateFrom);
            savedCarBooking.BookingDateTo.ShouldBe(_parkingRequest.BookingDateTo);
            savedCarBooking.Id.ShouldBe(_availableparkingSpaces.First().Id);
        }

        [Fact]
        public void Should_Not_Save_Car_Booking_Request_If_No_car_Avaiable()
        {
            _availableparkingSpaces.Clear();
            _processor.ProcessRequest(_parkingRequest);
            _carBookingServiceMock.Verify(q => q.Save(It.IsAny<CarBooking>()), Times.Never);
        }

        [Theory]
        [InlineData(BookingResultFlag.Failure, false)]
        [InlineData(BookingResultFlag.Success, true)]
        public void Should_ReturnSuccessFailure_Flag_In_Result(BookingResultFlag bookingSucesssFlag, bool isAvailable)
        {
            if (!isAvailable)
            {
                _availableparkingSpaces.Clear();

            }

            var result = _processor.ProcessRequest(_parkingRequest);
            bookingSucesssFlag.ShouldBe(result.Flag);
        }

        [Theory]
        [InlineData(1, true)]
        [InlineData(null, false)]
        public void Should_Return_RoomBookingId_In_Result(int? carParkBookingId, bool isAvailable)
        {
            if (!isAvailable)
            {
                _availableparkingSpaces.Clear();

            }
            else
            {
                _carBookingServiceMock.Setup(q => q.Save(It.IsAny<CarBooking>()))
               .Callback<CarBooking>(booking =>
               {
                   booking.Id = carParkBookingId!.Value;
               });

                var result = _processor.ProcessRequest(_parkingRequest);
                result.CarBookingId.ShouldBe(carParkBookingId);
            }
        }
    }
}
