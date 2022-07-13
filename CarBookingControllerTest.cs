using Microsoft.AspNetCore.Mvc;
using Moq;
using ParkingManagment.API.Controllers;
using ParkingManagment.Core.Enities;
using ParkingManagment.Core.Enums;
using ParkingManagment.Core.Models;
using ParkingManagment.Core.Processors;
using Shouldly;
using System;
using System.Threading.Tasks;
using Xunit;

namespace ParkingManagment.API.Test
{
    public class CarBookingControllerTest
    {
        private readonly Mock<ICarParkingRequestProcessor> _carParkingProcessor;
        private readonly CarBookingController _contorller;
        private readonly CarParkingBookingRequest _request;
        private readonly CarParkingBookingResult _result;


        public CarBookingControllerTest()
        {
            _carParkingProcessor = new Mock<ICarParkingRequestProcessor>();
            _contorller = new CarBookingController(_carParkingProcessor.Object);
            _request = new CarParkingBookingRequest();
            _result = new CarParkingBookingResult();

            _carParkingProcessor.Setup(x => x.ProcessRequest(_request)).Returns(_result);

        }
        [Theory]
        [InlineData(1, true, typeof(OkObjectResult), BookingResultFlag.Success)]
        [InlineData(0, false, typeof(BadRequestObjectResult), BookingResultFlag.Failure)]

        public async Task SHould_Call_Booking_Method_When_Vaild(int execpetedMethodCalls, bool isModelVaild, Type execpetedActionResultType, BookingResultFlag bookingResultFlag)
        {
            if (!isModelVaild)
            {
                _contorller.ModelState.AddModelError("key", "ErrorMessage");
            }
            _result.Flag = bookingResultFlag;


            var result = await _contorller.BookCarSpace(_request);

            result.ShouldBeOfType(execpetedActionResultType);
            _carParkingProcessor.Verify(x => x.ProcessRequest(_request), Times.Exactly(execpetedMethodCalls));
        }
    }

}
