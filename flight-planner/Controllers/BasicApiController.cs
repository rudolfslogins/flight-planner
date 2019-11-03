using System.Web.Http;
using AutoMapper;
using flight_planner.core.Models;
using flight_planner.core.Services;
using flight_planner.Models;
using flight_planner.services;

namespace flight_planner.Controllers
{
    public class BasicApiController : ApiController
    {
        protected readonly IFlightService _flightService;
        protected readonly IMapper _mapper;
        public BasicApiController(IFlightService flightService, IMapper mapper)
        {
            _flightService = flightService;
            _mapper = mapper;
        }
        public static Flight ConvertToFlight(FlightRequest flightRequest)
        {
            //return new Flight
            //{
            //    From = ConvertToAirport(flightRequest.From),
            //    To = ConvertToAirport(flightRequest.To),
            //    ArrivalTime = flightRequest.ArrivalTime,
            //    id = flightRequest.id,
            //    DepartureTime = flightRequest.DepartureTime,
            //    Carrier = flightRequest.Carrier
            //};
            return new Flight(
                ConvertToAirport(flightRequest.From),
                ConvertToAirport(flightRequest.To),
                flightRequest.Carrier,
                flightRequest.DepartureTime,
                flightRequest.ArrivalTime);
        }

        public static FlightRequest ConvertToFlightRequest(Flight flight)
        {
            //return new FlightRequest
            //{
            //    From = ConvertToAirportRequest(flight.From),
            //    To = ConvertToAirportRequest(flight.To),
            //    ArrivalTime = flight.ArrivalTime,
            //    id = flight.id,
            //    DepartureTime = flight.DepartureTime,
            //    Carrier = flight.Carrier
            //};
            return new FlightRequest(ConvertToAirportRequest(flight.From),
                ConvertToAirportRequest(flight.To),
                flight.Carrier,
                flight.DepartureTime,
                flight.ArrivalTime);
        }

        public static AirportRequest ConvertToAirportRequest(Airport airport)
        {
            //return new AirportRequest
            //{
            //    Airport = airport.AirportCode,
            //    City = airport.City,
            //    Country = airport.Country
            //};
            return new AirportRequest(airport.Country, airport.City, airport.AirportCode);
        }
        public static Airport ConvertToAirport(AirportRequest airportRequest)
        {
            //return new Airport
            //{
            //    AirportCode = airportRequest.Airport,
            //    City = airportRequest.City,
            //    Country = airportRequest.Country
            //};
            return new Airport(airportRequest.Country, airportRequest.City, airportRequest.Airport);
        }
    }
}
