using Microsoft.Extensions.Logging;
using NSubstitute;
using ParkingManagment.API.Controllers;
using ParkingManagment.Core.Enities;
using ParkingManagment.Core.Processors;
using Shouldly;
using System;
using System.Threading.Tasks;
using Xunit;

namespace ParkingManagment.API.Test
{
    public class CarAmendBookingTest
    {
        private readonly ICarParkingAmendBookingProcessor _processor;
        private readonly ILogger<CarAmendBookingController> _looger;

        private readonly CarAmendBookingController _caramendBookingController;

        public CarAmendBookingTest()
        {
            _processor = Substitute.For<ICarParkingAmendBookingProcessor>();
            _looger = Substitute.For<ILogger<CarAmendBookingController>>();

            _caramendBookingController = new CarAmendBookingController(_processor, _looger);
        }
        [Fact]
        public async Task CreateProduct_WhenCalled_ShouldNotBeEqualToTheSameProductObject()
        {
            var products = MockResponse;

            var createdProducts = CreatedProductMockResponse;

            await _processor.CreateProduct(createdProducts);

            products.ShouldNotBe(createdProducts);
        }

        private static Appointment MockResponse
        {
            get
            {
                return new Appointment
                {
                    ParkingPlaceId = 1,
                    FulNamae = "TestName",
                    Email = "test email",
                    Car = "Nession",
                    BookingDateFrom = DateTime.Now,
                    BookingDateTo = DateTime.Now,


                };
            }
        }
        private static Appointment CreatedProductMockResponse
        {
            get
            {
                return new Appointment
                {
                    ParkingPlaceId = 2,
                    FulNamae = "TestName2",
                    Email = "test2 email",
                    Car = "Nession2",
                    BookingDateFrom = DateTime.Now,
                    BookingDateTo = DateTime.Now,

                };
            }

        }
    }
}
