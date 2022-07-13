using ParkingManagment.Core.Enities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingManagment.Core.Processors
{
    public class CarParkingAmend : ICarParkingAmendBookingProcessor
    {
        private readonly ICarParkingAmendBookingProcessor carParkingAmendBookingProcessor;
        public Task CreateProduct(Appointment appointment)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteProduct(string id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateProduct(Appointment Appointment)
        {
            throw new NotImplementedException();
        }
    }
}
