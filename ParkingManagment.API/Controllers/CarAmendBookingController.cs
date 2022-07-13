using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ParkingManagment.Core.Enities;
using ParkingManagment.Core.Processors;
using System.Net;

namespace ParkingManagment.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CarAmendBookingController : ControllerBase
    {
        private readonly ICarParkingAmendBookingProcessor _processor;
        private readonly ILogger<CarAmendBookingController> _looger;
        public CarAmendBookingController(ICarParkingAmendBookingProcessor processor, ILogger<CarAmendBookingController> looger)
        {
            _processor = processor;
            _looger = looger;
        }

        [HttpPost]
        [ProducesResponseType(typeof(Appointment), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Appointment>> CreateProduct([FromBody] Appointment appointment)
        {
            await _processor.CreateProduct(appointment);

            return CreatedAtRoute("GetProduct", new { id = appointment.ParkingPlaceId }, appointment);
        }

        [HttpPut]
        [ProducesResponseType(typeof(Appointment), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Appointment>> UpdateProduct([FromBody] Appointment appointment)
        {
            return Ok(await _processor.UpdateProduct(appointment));
        }

        [HttpDelete("{id:length(24)}", Name = "DeleteProduct")]
        [ProducesResponseType(typeof(Appointment), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Appointment>> DeleteProductById(string id)
        {
            return Ok(await _processor.DeleteProduct(id));
        }
    }
}
