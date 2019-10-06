using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using flight_planner.Attributes;
using flight_planner.Models;
using flight_planner.services;

namespace flight_planner.Controllers
{
    [BasicAuthentication]
    public class AdminApiController : BasicApiController

    {
        private readonly FlightService _flightService;
        public AdminApiController()
        {
            _flightService = new FlightService();
        }

        [HttpGet]
        [Route("admin-api/flights/{id}")]
        public async Task<IHttpActionResult> Get(int id)
        {
            var flight = await _flightService.GetFlightById(id);
            if (flight == null)
            {
                return NotFound();
            }
            return Ok(ConvertToFlightRequest(flight));
        }

        [HttpGet]
        [Route("admin-api/get/flights")]
        public async Task<IHttpActionResult> GetFlights()
        {
            var flights = await _flightService.GetAllFlights();
            return Ok(flights.Select((ConvertToFlightRequest)).ToList());
        }

        [HttpPut]
        [Route("admin-api/flights")]
        public async Task<IHttpActionResult> AddFlight(FlightRequest flight)
        {
            if (!IsValid(flight))
            {
                return BadRequest("Flight Request Not Correct");
            }
            var result = await _flightService.AddFlight(ConvertToFlight(flight));
            if (!result.Succeeded)
            {
                return Conflict();
            }
            flight.id = result.Id;
            return Created("",flight);
        }

        [HttpDelete]
        [Route("admin-api/flights/{id}")]
        public async Task<IHttpActionResult> Delete(int id)
        {
            await _flightService.DeleteFlightById(id);
            return Ok();
        }
        private bool IsValid(FlightRequest flight)
        {
            if (!string.IsNullOrEmpty(flight.ArrivalTime) &&
                !string.IsNullOrEmpty(flight.DepartureTime) &&
                !string.IsNullOrEmpty(flight.Carrier) &&
                !string.IsNullOrEmpty(flight.DepartureTime) &&
                IsValidAirport(flight.From) && IsValidAirport(flight.To) &&
                !flight.From.Equals(flight.To) &&
                IsValidDateTime(flight.DepartureTime, flight.ArrivalTime))
            {
                return true;
            }
            return false;
        }

        private static bool IsValidAirport(AirportRequest airport)
        {
            return airport != null && !string.IsNullOrEmpty(airport.Airport) &&
                   !string.IsNullOrEmpty(airport.City) &&
                   !string.IsNullOrEmpty(airport.Country);
        }

        private static bool IsValidDateTime(string departTime, string arrTime)
        {
            DateTime dTime = DateTime.Parse(departTime);
            DateTime aTime = DateTime.Parse(arrTime);

            if (aTime == dTime || aTime < dTime)
            {
                return false;
            }
            return true;
        }
    }
}
