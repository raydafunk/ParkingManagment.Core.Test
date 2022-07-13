using ParkingManagment.Core.Enities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingManagment.Core.Processors
{
     public interface ICarParkingAmendBookingProcessor
    {
        Task CreateProduct(Appointment appointment);
        Task<bool> UpdateProduct(Appointment Appointment);
        Task<bool> DeleteProduct(string id);
    }
}
