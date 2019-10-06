using flight_planner.Models;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using flight_planner.services;

namespace flight_planner.Controllers
{
    public class ClientApiController : BasicApiController
    {
        private readonly FlightService _flightService;

        public ClientApiController()
        {
            _flightService = new FlightService();
        }

        [HttpGet]
        [Route("api/flights/{id}")]
        public async Task<IHttpActionResult> GetFlightById(int id)
        {
            var flight = await _flightService.GetFlightById(id);
            if (flight == null)
            {
                return NotFound();
            }
            return Ok(ConvertToFlightRequest(flight));
        }

        [HttpGet]
        [Route("api/airports")]
        public async Task<IHttpActionResult> GetAirport(string search)
        {
            var airports = await _flightService.GetAirport(search.Trim().ToLowerInvariant());
            return Ok(airports.Select((ConvertToAirportRequest)).ToList());
        }

        [HttpPost]
        [Route("api/flights/search")]
        public async Task<IHttpActionResult> PostFlightSearch(FlightSearch flight)
        {
            if (!IsValid(flight))
            {
                return BadRequest();
            }
            var flights = await _flightService.GetFlights(flight.From, flight.To, flight.DepartureDate);
            var response = new FlightSearchResponse(flights.Select((ConvertToFlightRequest)).Distinct().ToList());

            return Ok(response);
        }

        private static bool IsValid(FlightSearch flightSearch)
        {
            if (flightSearch != null && 
                flightSearch.From != null && 
                flightSearch.To != null && 
                flightSearch.DepartureDate.GetHashCode() != 0 &&
                flightSearch.From != flightSearch.To)
            {
                return true;
            }
            return false;
        }
    }
}
