using flight_planner.Models;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using AutoMapper;
using flight_planner.core.Models;
using flight_planner.core.Services;
using flight_planner.services;
using MoreLinq;

namespace flight_planner.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ClientApiController : BasicApiController
    {
        //private readonly FlightService _flightService;
        protected readonly IAirportService _airportService;

        public ClientApiController(IAirportService airportService, IFlightService flightService, IMapper mapper) : base(flightService, mapper)
        {
            _airportService = airportService;
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
            //return Ok(ConvertToFlightRequest(flight));
            return Ok(_mapper.Map<FlightRequest>(flight));
        }

        [HttpGet]
        [Route("api/airports")]
        public async Task<IHttpActionResult> GetAirport(string search)
        {
            //var airports = await _flightService.GetAirport(search.Trim().ToLowerInvariant());
            var airports = await _airportService.SearchAirports(search.Trim().ToLowerInvariant());
            return Ok(airports
                .Select((ConvertToAirportRequest))
                .DistinctBy(f => new { f.City, f.Country, f.Airport })
                .ToList());
        }

        [HttpPost]
        [Route("api/flights/search")]
        public async Task<IHttpActionResult> PostFlightSearch(FlightSearch flight)
        {
            if (!IsValid(flight))
            {
                return BadRequest();

            }
            //var flights = await _flightService.GetFlights(flight.From, flight.To, flight.DepartureDate);
            var flights = await _flightService.GetFlights();
            var response = new FlightSearchResponse(flights.Select(f => _mapper.Map<FlightRequest>(f))
                .Where(f => f.To.Airport == flight.To && 
                            f.From.Airport == flight.From && 
                            f.DepartureTime.Substring(0, 9) == flight.DepartureDate.Substring(0, 9))
                .DistinctBy(f => new {f.ArrivalTime, f.DepartureTime, f.Carrier})
                .ToList());

            //var response = new FlightSearchResponse(flights.Select(ConvertToFlightRequest)
            //    .GroupBy(f => new {f.ArrivalTime, f.DepartureTime, f.Carrier}).ToList());

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
