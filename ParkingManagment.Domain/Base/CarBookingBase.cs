using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingManagment.Core.Models
{
    public  abstract class CarBookingBase: IValidatableObject
    {
        public int ParkingPlaceId { get; set; }
        public string? FulNamae { get; set; }
        public string? Car { get; set; }
        public DateTime BookingDateFrom { get; set; }
        public DateTime BookingDateTo { get; set; }
        public string? Email { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (BookingDateFrom < DateTime.Now)
            {
                yield return new ValidationResult("Date Must be in the Future", new[] { nameof(BookingDateFrom) });
            }
        }
    }
}
