using Microsoft.AspNetCore.Mvc;
using ParkingManagment.Core.Models;
using ParkingManagment.Core.Processors;

namespace ParkingManagment.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CarBookingController : ControllerBase
    {
        private ICarParkingRequestProcessor _parkingRequestProcessor;

        public CarBookingController(ICarParkingRequestProcessor parkingRequestProcessor)
        {
            this._parkingRequestProcessor = parkingRequestProcessor;
        }
        [HttpPost]
        public async Task<IActionResult> BookCarSpace(CarParkingBookingRequest request)
        {
            if (ModelState.IsValid)
            {
                var result = _parkingRequestProcessor.ProcessRequest(request);
                if (result.Flag == Core.Enums.BookingResultFlag.Success)
                {
                    return Ok(result);
                }

                ModelState.AddModelError(nameof(CarParkingBookingRequest.BookingDateFrom), "No rooms available for given date");
            }

            return BadRequest(ModelState);
        }


    }
}
