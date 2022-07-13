using ParkingManagment.Core.Enums;
using System;

namespace ParkingManagment.Core.Models
{
    public class CarParkingBookingResult : CarBookingBase
    {
        public BookingResultFlag Flag { get; set; }

        public int? CarBookingId { get; set; }
    }
}