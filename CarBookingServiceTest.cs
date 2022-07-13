using Microsoft.EntityFrameworkCore;
using ParkingManagment.Core.Domain.Enities;
using ParkingManagment.Persistence.Repositories;
using RoomBookingApp.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ParkingManagment.Persistance.test
{
    public class CarBookingServiceTest
    {
        [Fact]
        public void should_Return_Available_CarSpacing()
        {
            var date = new DateTime(2022, 07, 05);

            var dbContextopitons = new DbContextOptionsBuilder<ParkingManagamentAppDbContext>()
                .UseInMemoryDatabase("AvailableRoomTest")
                .Options;

            using var context = new ParkingManagamentAppDbContext(dbContextopitons);
            context.Add(new CarBooking { Id = 1, Name = "street1" });
            context.Add(new CarBooking { Id = 2, Name = "street2" });
            context.Add(new CarBooking { Id = 3, Name = "street3" });
            context.Add(new CarBooking { Id = 4, Name = "street4" });
            context.Add(new CarBooking { Id = 5, Name = "street5" });
            context.Add(new CarBooking { Id = 6, Name = "street6" });
            context.Add(new CarBooking { Id = 7, Name = "street7" });
            context.Add(new CarBooking { Id = 8, Name = "street8" });
            context.Add(new CarBooking { Id = 3, Name = "street9" });
            context.Add(new CarBooking { Id = 9, Name = "street10" });

            context.Add(new CarBooking { Id = 1, BookingDateFrom = date });
            context.Add(new CarBooking { Id = 2, BookingDateFrom = date.AddDays(-1) });

            context.SaveChanges();

            var roomBookingService = new CarParkingBookingService(context);

           
            var avaialbleRooms = roomBookingService.GetAvailabeParkingSpaces(date);

            //assert
            Assert.Equal(2, avaialbleRooms.Count());
            Assert.Contains(avaialbleRooms, q => q.Id == 2);
            Assert.Contains(avaialbleRooms, q => q.Id == 3);
            Assert.DoesNotContain(avaialbleRooms, q => q.Id == 4);
        }

        [Fact]
        public void should_Save_Room_Booking()
        {
            var dbContextopitons = new DbContextOptionsBuilder<ParkingManagamentAppDbContext>()
                .UseInMemoryDatabase("ShouldSaveTest")
                .Options;

            var carBooking = new CarBooking   { Id = 1, BookingDateFrom = new DateTime(2021, 06, 09) };

            using var context = new ParkingManagamentAppDbContext(dbContextopitons);
            var roomBookingService = new CarParkingBookingService(context);
            roomBookingService.Save(carBooking);

            var bookings = context.RoomBookings.ToList();
            var booking = Assert.Single(bookings);

            Assert.Equal(carBooking.BookingDateFrom, booking.BookingDateFrom);
            Assert.Equal(carBooking.Id, booking.Id);

        }
    }
}
